using Autofac;
using Futures.Infrastructure.DependencyInjection;
using Futures.Lists.Domain.Services.ClientLists.Repositories;
using Futures.Lists.Domain.Services.UserLists.Repositories;
using Futures.Lists.Infrastructure.ClientLists;
using Futures.Lists.Infrastructure.UserLists;

namespace Futures.Lists.Infrastructure
{
    public class Module : ModuleBase
    {
        protected override void Register(ContainerBuilder builder)
        {
            builder.RegisterType<ClientListFileRepository>().As<IClientListRepository>();
            builder.RegisterType<FuturesUserFileRepository>().As<IFuturesUserRepository>();
        }
    }
}