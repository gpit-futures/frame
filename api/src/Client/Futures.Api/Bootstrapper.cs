using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using Futures.Infrastructure.Hubs;
using Futures.Infrastructure.MessageQueue;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.DependencyInjection;
using RawRabbit;
using RawRabbit.vNext;

namespace Futures.Api
{
    public static class Bootstrapper
    {
        public static IContainer SetupContainer(IServiceCollection services)
        {
            var containerBuilder = new ContainerBuilder();

            Bootstrapper.RegisterModules(containerBuilder);

            containerBuilder.Populate(services);
            var container = containerBuilder.Build();

            // Run all startup tasks
            var tasks = container.Resolve<IEnumerable<Futures.Infrastructure.DependencyInjection.IStartupTask>>();
            Task.WaitAll(tasks.Select(t => t.Start()).ToArray());

            return container;
        }

        public static IBusClient SetupMessageSubscriptions(IServiceCollection services, IContainer container)
        {
            var bus = BusClientFactory.CreateDefault(services);

            var hub = container.Resolve<IHubContext<NotificationsHub>>();

            var task = container.Resolve<IMessageSubscription>();
            task.Start(bus, container, hub);

            return bus;
        }

        private static void RegisterModules(ContainerBuilder builder)
        {
            builder.RegisterModule<Futures.Infrastructure.Module>();
            builder.RegisterModule<Futures.Notifications.Infrastructure.Module>();
        }
    }
}