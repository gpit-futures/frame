using System;
using System.Threading.Tasks;
using Futures.Dashboard.Domain.Services.RecentPatientLists.Entities;
using Futures.Dashboard.Domain.Services.RecentPatientLists.Repositories;
using Futures.Infrastructure.Mongo;
using MongoDB.Driver;

namespace Futures.Dashboard.Infrastructure.RecentPatientLists
{
    public class RecentPatientListsRepository : IRecentPatientListsRepository
    {
        public RecentPatientListsRepository(IMongoDatabaseFactory factory)
        {
            this.Collection = factory.GetDatabase()
                .GetCollection<RecentPatientList>("recentPatientLists");
        }

        private IMongoCollection<RecentPatientList> Collection { get; }

        public Task<RecentPatientList> GetOneByUser(string user)
        {
            return this.Collection.GetOneAsync(user);
        }

        public Task<RecentPatientList> GetOne(Guid id)
        {
            return this.Collection.GetOneAsync(id);
        }

        public Task AddOrUpdate(RecentPatientList item)
        {
            return this.Collection.AddOrUpdate(item);
        }
    }

    internal static class RecentPatientListExtensions
    {
        public static async Task<T> GetOneAsync<T>(this IMongoCollection<T> collection, string user) where T : RecentPatientList
        {
            return await collection
                .Find(Builders<T>.Filter.Eq(x => x.User, user))
                .SingleOrDefaultAsync();
        }
    }
}