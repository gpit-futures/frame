using MongoDB.Driver;

namespace Futures.Infrastructure.Mongo
{
    public interface IMongoConnectionFactory
    {
        IMongoClient GetClient();
    }
}