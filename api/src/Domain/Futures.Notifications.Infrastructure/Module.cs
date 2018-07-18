using Autofac;
using Futures.Infrastructure.DependencyInjection;
using Futures.Infrastructure.MessageQueue;
using Futures.Notifications.Domain.Messages;
using Futures.Notifications.Domain.Services.Notifications.Repositories;
using Futures.Notifications.Infrastructure.Handlers;
using Futures.Notifications.Infrastructure.Notifications;

namespace Futures.Notifications.Infrastructure
{
    public class Module : ModuleBase
    {
        protected override void Register(ContainerBuilder builder)
        {
            builder.RegisterType<NotificationsRepository>().As<INotificationsRepository>();

            builder.RegisterType<MessageQueueBootstrap>().As<IMessageSubscription>();

            builder.RegisterType<ObservationCreatedHandler>().As<IMessageHandler<ObservationCreated>>();
            builder.RegisterType<AppointmentCreatedHandler>().As<IMessageHandler<AppointmentCreated>>();
            builder.RegisterType<CarePlanCreatedHandler>().As<IMessageHandler<CarePlanCreated>>();
            builder.RegisterType<ObservationUpdatedHandler>().As<IMessageHandler<ObservationUpdated>>();

            builder
                .RegisterType<MongoIndexBuilder>()
                .As<IStartupTask>();
        }
    }
}
