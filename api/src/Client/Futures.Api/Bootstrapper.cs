using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;

namespace Futures.Api
{
    public static class Bootstrapper
    {
        public static IContainer SetupContainer(IServiceCollection services)
        {
            var container = new ContainerBuilder();

            Bootstrapper.RegisterModules(container);

            container.Populate(services);
            return container.Build();
        }

        private static void RegisterModules(ContainerBuilder builder)
        {
            builder.RegisterModule<Futures.Infrastructure.Module>();
        }
    }
}