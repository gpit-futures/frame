using Autofac;
using Futures.Dashboard.Domain.Services.RecentPatientLists;
using Futures.Dashboard.Domain.Services.RecentPatientLists.Repositories;
using Futures.Dashboard.Domain.Services.RecentPatients.Repositories;
using Futures.Dashboard.Infrastructure.RecentPatientLists;
using Futures.Dashboard.Infrastructure.RecentPatients;
using Futures.Infrastructure.DependencyInjection;

namespace Futures.Dashboard.Infrastructure
{
    public class Module : ModuleBase
    {
        protected override void Register(ContainerBuilder builder)
        {
            builder.RegisterType<RecentPatientsRepository>().As<IRecentPatientsRepository>();
            builder.RegisterType<RecentPatientListsRepository>().As<IRecentPatientListsRepository>();

            builder.RegisterType<RecentPatientListsService>().As<IRecentPatientListsService>();
        }
    }
}