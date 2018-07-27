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
        public RecentPatientListsRepository(IMongoCollection<RecentPatientList> collection)
        {
            this.Collection = collection;
        }

        private IMongoCollection<RecentPatientList> Collection { get; }

        public async Task<RecentPatientList> GetOneByUser(string user)
        {
            var result = await this.Collection.FindAsync(x => string.Equals(x.User, user));

            return result.FirstOrDefault();
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
}