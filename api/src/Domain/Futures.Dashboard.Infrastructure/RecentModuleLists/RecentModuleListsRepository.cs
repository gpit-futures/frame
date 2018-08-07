using System;
using System.Threading.Tasks;
using Futures.Dashboard.Domain.Services.RecentModules.Entities;
using Futures.Dashboard.Domain.Services.RecentModules.Repositories;
using Futures.Infrastructure.Mongo;
using MongoDB.Driver;

namespace Futures.Dashboard.Infrastructure.RecentModuleLists
{
    public class RecentModuleListsRepository : IRecentModuleListsRepository
    {
        public RecentModuleListsRepository(IMongoDatabaseFactory factory)
        {
            this.Collection = factory.GetDatabase()
                .GetCollection<RecentModulesList>("recentModulesLists");
        }

        private IMongoCollection<RecentModulesList> Collection { get; }

        public Task<RecentModulesList> GetOneByUser(string user)
        {
            return this.Collection.GetOneAsync(user);
        }

        public Task<RecentModulesList> GetOne(Guid id)
        {
            return this.Collection.GetOneAsync(id);
        }

        public Task AddOrUpdate(RecentModulesList item)
        {
            return this.Collection.AddOrUpdate(item);
        }
    }

    internal static class RecentPatientListExtensions
    {
        public static async Task<T> GetOneAsync<T>(this IMongoCollection<T> collection, string user) where T : RecentModulesList
        {
            return await collection
                .Find(Builders<T>.Filter.Eq(x => x.User, user))
                .SingleOrDefaultAsync();
        }
    }
}