using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

namespace Futures.Application.Hubs
{
    [Authorize]
    public class NotificationsHub : Hub
    {
        public override async Task OnConnectedAsync()
        {
            var ods = this.Context.User.FindFirst(x => x.Type == "odsId");
            var connectionId = this.Context.ConnectionId;

            await this.Groups.AddToGroupAsync(connectionId, ods?.Value?.ToLowerInvariant());
            await base.OnConnectedAsync();
        }

        public override async Task OnDisconnectedAsync(Exception exception)
        {
            var ods = this.Context.User.FindFirst(x => x.Type == "odsId");
            var connectionId = this.Context.ConnectionId;

            await this.Groups.RemoveFromGroupAsync(connectionId, ods?.Value?.ToLowerInvariant());
            await base.OnDisconnectedAsync(exception);
        }
    }
}