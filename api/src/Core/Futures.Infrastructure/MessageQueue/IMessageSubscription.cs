using Autofac;
using Futures.Infrastructure.Hubs;
using Microsoft.AspNetCore.SignalR;
using RawRabbit;

namespace Futures.Infrastructure.MessageQueue
{
    public interface IMessageSubscription
    {
        void Start(IBusClient bus, IContainer container, IHubContext<NotificationsHub> hub);
    }
}