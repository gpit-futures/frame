using Autofac;
using Futures.Infrastructure.DependencyInjection;
using Futures.Infrastructure.MessageQueue;
using Futures.Notifications.Domain.Messages.V1;
using Futures.Notifications.Domain.Services.Notifications.Repositories;
using Futures.Notifications.Infrastructure.Handlers;
using Futures.Notifications.Infrastructure.Handlers.V1;
using Futures.Notifications.Infrastructure.Notifications;

namespace Futures.Notifications.Infrastructure
{
    public class Module : ModuleBase
    {
        protected override void Register(ContainerBuilder builder)
        {
            builder.RegisterType<NotificationsRepository>().As<INotificationsRepository>();

            //builder.RegisterType<MessageQueueBootstrap>().As<IMessageSubscription>();
            builder.RegisterType<MessageQueueBootstrapV1>().As<IMessageSubscriptionV1>();

            builder.RegisterType<ObservationCreatedHandlerV1>().As<IMessageHandlerV1<ObservationCreatedV1>>();
            builder.RegisterType<EncounterCreatedHandlerV1>().As<IMessageHandlerV1<EncounterCreatedV1>>();
            builder.RegisterType<CarePlanCreatedHandlerV1>().As<IMessageHandlerV1<CarePlanCreatedV1>>();
            builder.RegisterType<ObservationUpdatedHandlerV1>().As<IMessageHandlerV1<ObservationUpdatedV1>>();

            builder
                .RegisterType<MongoIndexBuilder>()
                .As<IStartupTask>();
        }
    }
}
