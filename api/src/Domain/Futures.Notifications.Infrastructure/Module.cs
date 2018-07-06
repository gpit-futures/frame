using Autofac;
using Futures.Infrastructure.DependencyInjection;
using Futures.Infrastructure.MessageQueue;
using Futures.Notifications.Domain.Messages;
using Futures.Notifications.Infrastructure.Handlers;

namespace Futures.Notifications.Infrastructure
{
    public class Module : ModuleBase
    {
        protected override void Register(ContainerBuilder builder)
        {
            builder.RegisterType<MessageQueueBootstrap>().As<IMessageSubscription>();

            builder.RegisterType<ObservationCreatedHandler>().As<IMessageHandler<ObservationCreated>>();
            builder.RegisterType<EncounterCreatedHandler>().As<IMessageHandler<EncounterCreated>>();

            builder
                .RegisterType<MongoIndexBuilder>()
                .As<IStartupTask>();
        }
    }
}
