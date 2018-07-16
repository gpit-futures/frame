using System;
using Autofac;
using Futures.Infrastructure.Hubs;
using Futures.Infrastructure.MessageQueue;
using Futures.Notifications.Domain.Messages;
using Microsoft.AspNetCore.SignalR;
using RawRabbit;

namespace Futures.Notifications.Infrastructure
{
    public class MessageQueueBootstrap : IMessageSubscription
    {
        public void Start(IBusClient bus, IContainer container, IHubContext<NotificationsHub> hub)
        {
            bus.SubscribeAsync<CarePlanCreated>((message, context) =>
            {
                var handler = container.Resolve<IMessageHandler<CarePlanCreated>>();
                return handler.Handle(message);
            }, config => config.WithSubscriberId(string.Empty));

            bus.SubscribeAsync<ObservationCreated>((message, context) =>
            {
                var handler = container.Resolve<IMessageHandler<ObservationCreated>>();
                return handler.Handle(message);
            }, config => config.WithSubscriberId(string.Empty));

            bus.SubscribeAsync<ObservationUpdated>((message, context) =>
            {
                var handler = container.Resolve<IMessageHandler<ObservationUpdated>>();
                return handler.Handle(message);
            }, config => config.WithSubscriberId(string.Empty));

            bus.SubscribeAsync<AppointmentCreated>((message, context) =>
            {
                var handler = container.Resolve<IMessageHandler<AppointmentCreated>>();
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