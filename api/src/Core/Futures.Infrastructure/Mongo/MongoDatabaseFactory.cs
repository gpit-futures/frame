using Microsoft.Extensions.Configuration;
using MongoDB.Driver;

namespace Futures.Infrastructure.Mongo
{
    public class MongoDatabaseFactory : IMongoDatabaseFactory
    {
        public MongoDatabaseFactory(IMongoConnectionFactory factory, IConfiguration config)
        {
            this.Client = factory.GetClient();

            var mongoConfig = config.GetSection("MongoDbSettings");
            this.DatabaseName = mongoConfig["DatabaseName"];
        }

        private IMongoClient Client { get; }

        private string DatabaseName { get; }

        public IMongoDatabase GetDatabase()
        {
            return this.Client.GetDatabase(this.DatabaseName);
        }
    }
}