using Microsoft.AspNetCore.SignalR;
using TargetMasters.Models.GameModels;

namespace TargetMasters.Hubs
{
    public class UserHub : Hub
    {

        public static int TotalViews { get; set; } = 0;
        public static MainGame MainGame { get; set; } = new MainGame();

        public override Task OnConnectedAsync()
        {
            var connectionId = Context.ConnectionId;
            TotalViews++;
            this.Clients.All.SendAsync("getTotalUsers", MainGame.Players.Count()).GetAwaiter().GetResult();
            this.Clients.Caller.SendAsync("getMyConnectionId", connectionId).GetAwaiter().GetResult();
            return base.OnConnectedAsync();
        }

        public class CustomTimer(double interval) : System.Timers.Timer(interval)
        {
            public HubCallerContext? CallerContext { get; set; }
            public IHubCallerClients? HubCallerClients { get; set; }
        }

        public override Task OnDisconnectedAsync(Exception? exception)
        {
            var connectionId = Context.ConnectionId;

            MainGame.Players.RemoveAll(x => x.ConnectionId == connectionId);

            MainGame.NumberOfPlayers = MainGame.Players.Where(x => x.IsReady).Count();

            if (MainGame.Players.Count() <= 1)
                ResetGameStatus();

            if(MainGame.HasStarted && MainGame.Players.FindIndex(x => x.PlayerStats.IsPlayerTurn) == -1)
            {
                var currentWinnerPlayer = MainGame.Players
                    .Where(x => x.IsReady && !x.IsWatching)
                    .OrderByDescending(x => x.PlayerStats.SecondsMs)
                    .FirstOrDefault();

                if(currentWinnerPlayer != null)
                    currentWinnerPlayer.PlayerStats.IsPlayerTurn = true;
                else ResetGameStatus();
            }

            TotalViews--;
            this.Clients.All.SendAsync("mainGameHandlerUpdate", MainGame.Players).GetAwaiter().GetResult();
            this.Clients.All.SendAsync("getTotalUsers", MainGame.Players.Count()).GetAwaiter().GetResult();
            this.Clients.All.SendAsync("updateTotalViews", TotalViews).GetAwaiter().GetResult();
            return base.OnDisconnectedAsync(exception);
        }

        public async Task NewWindowLoaded()
        {
            TotalViews++;
            await this.Clients.All.SendAsync("updateTotalViews", TotalViews);
        }

        // Player entrou na sala de jogo:
        public async Task MainGameHandler(string playerName)
        {
            var connectionId = Context.ConnectionId;
            var joinedPlayer = new Player(playerName, connectionId);

            // Se Player entrou na sala de jogo enquanto uma partida está em andamento, adquire status de observador até que jogo atual acabe:
            if(MainGame.HasStarted)
            {
                joinedPlayer.IsWatching = true;
                joinedPlayer.HasGameStarted = true;
            }

            MainGame.Players.Add(joinedPlayer);
            await this.Clients.All.SendAsync("mainGameHandlerUpdate", MainGame.Players);
            await this.Clients.All.SendAsync("getTotalUsers", MainGame.Players.Count());
        }

        // Player acionou o botão de "pronto":
        public async Task PreGameOrganizer()
        {
            var connectionId = Context.ConnectionId;
            var player = MainGame.Players.FirstOrDefault(x => x.ConnectionId == connectionId);
            if (player != null)
                player.IsReady = !player.IsReady;

            await this.Clients.All.SendAsync("mainGameHandlerUpdate", MainGame.Players);

            // Todos os players estão prontos:
            if(MainGame.Players.Count() > 1 && MainGame.Players.Count() == MainGame.Players.Where(x => x.IsReady).ToArray().Count())
            {
                await GameLogicHandle();
            }
        }

        // Se inicia o jogo:
        public async Task GameLogicHandle()
        {
            if(!MainGame.HasStarted)
            {
                MainGame.HasStarted = true;
                MainGame.NumberOfPlayers = MainGame.Players.Where(x => x.IsReady).Count();
                MainGame.Players.ForEach(x => x.HasGameStarted = true);

                await this.Clients.All.SendAsync("mainGameHandlerUpdate", MainGame.Players);
            }

            Random random = new Random();
            int numeroAleatorio = random.Next(1, 7);
            if (numeroAleatorio == 1)
            {
                MainGame.CurrentGameDifficultLevel--;
                if (MainGame.CurrentGameDifficultLevel < 0)
                    MainGame.CurrentGameDifficultLevel = 0;
            }
            if (numeroAleatorio == 6)
                MainGame.CurrentGameDifficultLevel++;


            // Cria lista para targets
            MainGame.TargetsQueue.Clear();
            var targets = TargetGenerator.GenerateRandomTargets(MainGame.CurrentGameDifficultLevel + 3);
            MainGame.TargetsQueue.AddRange(targets);

            // Selecionar proximo jogador
            MainGame.SetNextPlayerTurn();
            // _ = CountdownTimer();

            await this.Clients.All.SendAsync("mainGameHandlerUpdate", MainGame.Players);
            await this.Clients.All.SendAsync("targetPositionValues", MainGame.TargetsQueue[0]);
        }

        public async Task ClicksHander()
        {
            MainGame.TargetsQueue.RemoveAt(0);

            if(MainGame.TargetsQueue.Count() == 0)
            {
                await GameLogicHandle();
            }

            await this.Clients.All.SendAsync("targetPositionValues", MainGame.TargetsQueue[0]);
        }

        public void ResetGameStatus()
        {
            MainGame.HasStarted = false;
            MainGame.TargetsQueue.Clear();
            MainGame.CurrentGameDifficultLevel = 0;
            MainGame.CurrentGameTurn = 0;
            MainGame.CurrentGameRound = 0;
            MainGame.NumberOfPlayers = 0;

            MainGame.Players.ForEach(x =>
            {
                x.PlayerStats.SecondsMs = 30000;
                x.PlayerStats.IsPlayerTurn = false;
                x.IsReady = false;
                x.IsWatching = false;
                x.Place = null;
                x.HasGameStarted = false;
            });

            this.Clients.All.SendAsync("mainGameHandlerUpdate", MainGame.Players).GetAwaiter().GetResult();
        }

        public async Task SendCountdownTimer()
        {
            var currentPlayer = MainGame.Players.FirstOrDefault(x => x.PlayerStats.IsPlayerTurn);
            if(currentPlayer != null)
            {
                currentPlayer.PlayerStats.SecondsMs -= 100;
                await this.Clients.Others.SendAsync("mainGameHandlerUpdate", MainGame.Players);

                if (currentPlayer.PlayerStats.SecondsMs <= 0)
                {
                    currentPlayer.Place = MainGame.NumberOfPlayers;
                    currentPlayer.IsReady = false;
                    currentPlayer.IsWatching = true;
                    currentPlayer.PlayerStats.IsPlayerTurn = false;
                    MainGame.NumberOfPlayers--;

                    if(MainGame.Players.Where(x => x.IsReady && !x.IsWatching).Count() == 1)
                    {
                        ResetGameStatus();
                    } else
                    {
                        await GameLogicHandle();
                    }

                    
                }
            }
        }
    }
}
