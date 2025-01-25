using Microsoft.AspNetCore.SignalR;
using TargetMasters.Models.GameModels;

namespace TargetMasters.Hubs
{
    public class UserHub : Hub
    {

        public static int TotalViews { get; set; } = 0;
        public static int TotalUsers { get; set; } = 0;
        public static MainGame MainGame { get; set; } = new MainGame();

        public override Task OnConnectedAsync()
        {
            // var connectionId = Context.ConnectionId;
            TotalViews++;
            this.Clients.All.SendAsync("getTotalUsers", MainGame.Players.Count()).GetAwaiter().GetResult();
            return base.OnConnectedAsync();
        }

        public override Task OnDisconnectedAsync(Exception? exception)
        {
            var connectionId = Context.ConnectionId;

            MainGame.Players.RemoveAll(x => x.ConnectionId == connectionId);

            TotalViews--;
            this.Clients.All.SendAsync("mainGameHandlerUpdate", MainGame).GetAwaiter().GetResult();
            this.Clients.All.SendAsync("getTotalUsers", MainGame.Players.Count()).GetAwaiter().GetResult();
            return base.OnDisconnectedAsync(exception);
        }

        public async Task NewWindowLoaded()
        {
            TotalViews++;
            await this.Clients.All.SendAsync("updateTotalViews", TotalViews);
        }

        public async Task MainGameHandler(string playerName)
        {
            var connectionId = Context.ConnectionId;
            var joinedPlayer = new Player(playerName, connectionId);

            //var lastStackPositionNumber = MainGame.Players.Select(x => x.StackPosition).LastOrDefault();
            //joinedPlayer.StackPosition = lastStackPositionNumber;

            MainGame.Players.Add(joinedPlayer);
            await this.Clients.All.SendAsync("mainGameHandlerUpdate", MainGame);
            await this.Clients.All.SendAsync("getTotalUsers", MainGame.Players.Count());
        }
    }
}
