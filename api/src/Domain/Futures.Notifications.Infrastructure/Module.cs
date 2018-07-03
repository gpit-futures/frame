using System;
using Autofac;
using Futures.Infrastructure.DependencyInjection;

namespace Futures.Notifications.Infrastructure
{
    public class Module : ModuleBase
    {
        protected override void Register(ContainerBuilder builder)
        {

            builder
                .RegisterType<MongoIndexBuilder>()
                .As<IStartupTask>();
        }
    }
}
