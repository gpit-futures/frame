using Autofac;
using Futures.Infrastructure.MessageQueue;
using Futures.Notifications.Domain.Messages.V1;
using RawRabbit;

namespace Futures.Notifications.Infrastructure
{
    public class MessageQueueBootstrapV1 : IMessageSubscriptionV1
    {
        public void Start(IBusClient bus, IContainer container)
        {
            bus.SubscribeAsync<CarePlanCreatedV1>((message, context) =>
            {
                var handler = container.Resolve<IMessageHandlerV1<CarePlanCreatedV1>>();
                return handler.Handle(message);
            }, config => config.WithSubscriberId(string.Empty));

            bus.SubscribeAsync<ObservationCreatedV1>((message, context) =>
            {
                var handler = container.Resolve<IMessageHandlerV1<ObservationCreatedV1>>();
                return handler.Handle(message);
            }, config => config.WithSubscriberId(string.Empty));

            bus.SubscribeAsync<ObservationUpdatedV1>((message, context) =>
            {
                var handler = container.Resolve<IMessageHandlerV1<ObservationUpdatedV1>>();
                return handler.Handle(message);
            }, config => config.WithSubscriberId(string.Empty));

            bus.SubscribeAsync<AppointmentCreatedV1>((message, context) =>
            {
                var handler = container.Resolve<IMessageHandlerV1<AppointmentCreatedV1>>();
                return handler.Handle(message);
            }, config => config.WithSubscriberId(string.Empty));

            //bus.SubscribeAsync<EncounterCreated>((message, context) =>
            //{
            //    //return hub.Clients.All.SendAsync("notification", message);
            //    //var handler = container.Resolve<IMessageHandler<EncounterCreated>>();
            //    //return handler.Handle(message);
            //}, config => config.WithSubscriberId(string.Empty));
        }
    }
}