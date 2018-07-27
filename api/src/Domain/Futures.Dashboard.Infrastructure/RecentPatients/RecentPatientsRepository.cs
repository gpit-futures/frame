using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Futures.Dashboard.Domain.Services.RecentPatients.Entities;
using Futures.Dashboard.Domain.Services.RecentPatients.Repositories;
using Futures.Infrastructure.Mongo;
using MongoDB.Driver;

namespace Futures.Dashboard.Infrastructure.RecentPatients
{
    public class RecentPatientsRepository : IRecentPatientsRepository
    {
        public RecentPatientsRepository(IMongoDatabaseFactory factory)
        {
            this.Collection = factory.GetDatabase()
                .GetCollection<RecentPatient>("recentPatients");
        }

        private IMongoCollection<RecentPatient> Collection { get; }

        public async Task<IEnumerable<RecentPatient>> GetAllByUser(string user)
        {
            var sort = Builders<RecentPatient>.Sort.Descending(x => x.DateAdded);

            return await this.Collection
                .Where(x => string.Equals(x.User, user))
                .Sort(sort)
                .ToListAsync();
        }

        public async Task<IEnumerable<RecentPatient>> GetAllByUser(string user, int limit)
        {
            var sort = Builders<RecentPatient>.Sort.Descending(x => x.DateAdded);

            return await this.Collection
                .Where(x => string.Equals(x.User, user))
                .Sort(sort)
                .Limit(limit)
                .ToListAsync();
        }

        public Task<RecentPatient> GetOne(Guid id)
        {
            return this.Collection.GetOneAsync(id);
        }

        public async Task Delete(Guid id)
        {
            await this.Collection.DeleteOneAsync(x => x.Id == id);
        }

        public Task DeleteMany(IEnumerable<RecentPatient> patients)
        {
            var filter = Builders<RecentPatient>.Filter.Where(rp => patients.Contains(rp));

            return this.Collection.DeleteManyAsync(filter);
        }

        public Task AddOrUpdate(RecentPatient item)
        {
            return this.Collection.AddOrUpdate(item);
        }
    }
}