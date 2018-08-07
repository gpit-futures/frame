using Microsoft.Extensions.Configuration;
using MongoDB.Driver;

namespace Futures.Infrastructure.Mongo
{
    public class MongoConnectionFactory : IMongoConnectionFactory
    {
        public MongoConnectionFactory(IConfiguration config)
        {
            var mongoConfig = config.GetSection("MongoDbSettings");
            this.Client = new MongoClient(mongoConfig["ConnectionString"]);
        }

        private IMongoClient Client { get; }

        public IMongoClient GetClient()
        {
            return this.Client;
        }
    }
}