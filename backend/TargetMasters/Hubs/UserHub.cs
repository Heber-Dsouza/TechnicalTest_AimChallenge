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

        public override Task OnDisconnectedAsync(Exception? exception)
        {
            var connectionId = Context.ConnectionId;

            MainGame.Players.RemoveAll(x => x.ConnectionId == connectionId);

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

            //var lastStackPositionNumber = MainGame.Players.Select(x => x.StackPosition).LastOrDefault();
            //joinedPlayer.StackPosition = lastStackPositionNumber;

            // Se Player entrou na sala de jogo enquanto uma partida está em andamento, adquire status de observador até que jogo atual acabe:
            if(MainGame.HasStarted)
            {
                joinedPlayer.IsWatching = true;
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
                player.IsReady = true;

            await this.Clients.All.SendAsync("mainGameHandlerUpdate", MainGame.Players);

            // Todos os players estão prontos:
            if(MainGame.Players.Count() == MainGame.Players.Where(x => x.IsReady).ToArray().Count())
            {
                await GameLogicHandle();
            }
        }

        // Se inicia o jogo:
        public async Task GameLogicHandle()
        {

        }

        public async Task CountdownTimer()
        {

        }
    }
}
