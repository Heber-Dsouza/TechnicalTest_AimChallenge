using Microsoft.AspNetCore.SignalR;

namespace TargetMasters.Hubs
{
    public class UserHub : Hub
    {
        //public int TotalViews { get; set; }

        public static int TotalViews { get; set; } = 0;
        public static int TotalUsers { get; set; } = 0;

        public override Task OnConnectedAsync()
        {
            TotalUsers++;
            this.Clients.All.SendAsync("getTotalUsers", TotalUsers).GetAwaiter().GetResult();
            return base.OnConnectedAsync();
        }

        public override Task OnDisconnectedAsync(Exception? exception)
        {
            TotalUsers--;
            this.Clients.All.SendAsync("getTotalUsers", TotalUsers).GetAwaiter().GetResult();
            return base.OnDisconnectedAsync(exception);
        }

        public async Task NewWindowLoaded()
        {
            TotalViews++;
            await this.Clients.All.SendAsync("updateTotalViews", TotalViews);
        }
    }
}
