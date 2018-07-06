using Autofac;
using Futures.Infrastructure.MessageQueue;
using Futures.Notifications.Domain.Messages;
using RawRabbit;

namespace Futures.Notifications.Infrastructure
{
    public class MessageQueueBootstrap : IMessageSubscription
    {
        public void Start(IBusClient bus, IContainer container)
        {
            bus.SubscribeAsync<ObservationCreated>((message, context) =>
            {
                var handler = container.Resolve<IMessageHandler<ObservationCreated>>();
                return handler.Handle(message);
            }, config => config.WithSubscriberId(string.Empty));

            bus.SubscribeAsync<EncounterCreated>((message, context) =>
            {
                var handler = container.Resolve<IMessageHandler<EncounterCreated>>();
                return handler.Handle(message);
            }, config => config.WithSubscriberId(string.Empty));
        }
    }
}