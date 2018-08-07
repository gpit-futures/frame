using Autofac;
using Futures.Infrastructure.DependencyInjection;
using Futures.Infrastructure.Mongo;

namespace Futures.Infrastructure
{
    public class Module : ModuleBase
    {
        protected override void Register(ContainerBuilder builder)
        {
            builder.RegisterType<MongoConnectionFactory>().As<IMongoConnectionFactory>();
            builder.RegisterType<MongoDatabaseFactory>().As<IMongoDatabaseFactory>();
        }
    }
}