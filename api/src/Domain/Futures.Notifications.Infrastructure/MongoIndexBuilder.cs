using System.Threading.Tasks;
using Futures.Infrastructure.DependencyInjection;
using Futures.Infrastructure.Mongo;
using Futures.Notifications.Domain.Services.Notifications.Entities;
using MongoDB.Driver;

namespace Futures.Notifications.Infrastructure
{
    public class MongoIndexBuilder : IStartupTask
    {
        public MongoIndexBuilder(IMongoDatabaseFactory factory)
        {
            this.Database = factory.GetDatabase();
        }

        private IMongoDatabase Database { get; }

        public async Task Start()
        {
            var indexModels =
                new CreateIndexModel<Notification>(
                    new IndexKeysDefinitionBuilder<Notification>().Ascending(x => x.Ods));

            await this.Database
                .GetCollection<Notification>("notifications")
                .Indexes
                .CreateOneAsync(indexModels);
        }
    }
}