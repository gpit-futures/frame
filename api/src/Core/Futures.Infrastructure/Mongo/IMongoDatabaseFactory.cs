using MongoDB.Driver;

namespace Futures.Infrastructure.Mongo
{
    public interface IMongoDatabaseFactory
    {
        IMongoDatabase GetDatabase();
    }
}