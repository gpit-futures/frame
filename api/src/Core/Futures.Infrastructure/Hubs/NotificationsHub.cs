using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;

namespace Futures.Infrastructure.Hubs
{
    public class NotificationsHub : Hub
    {
        public override async Task OnConnectedAsync()
        {
            var test = this.Context.ConnectionId;

            System.Console.WriteLine($"ClientId {test} conencted.");
            await base.OnConnectedAsync();
        }
    }
}