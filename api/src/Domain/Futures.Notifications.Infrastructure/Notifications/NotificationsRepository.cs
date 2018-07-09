using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Futures.Infrastructure.Mongo;
using Futures.Notifications.Domain.Services.Notifications.Entities;
using Futures.Notifications.Domain.Services.Notifications.Repositories;
using MongoDB.Driver;

namespace Futures.Notifications.Infrastructure.Notifications
{
    public class NotificationsRepository : INotificationsRepository
    {
        public NotificationsRepository(IMongoDatabaseFactory factory)
        {
            this.Collection = factory.GetDatabase()
                .GetCollection<Notification>("notifications");
        }

        private IMongoCollection<Notification> Collection { get; }

        public async Task<IEnumerable<Notification>> GetAll()
        {
            return await this.Collection
                .FindAll()
                .ToListAsync();
        }

        public async Task<IEnumerable<Notification>> GetAllByOds(string ods)
        {
            return await this.Collection
                .Where(x => string.Equals(x.Ods, ods, StringComparison.InvariantCultureIgnoreCase))
                .ToListAsync();
        }

        public Task<Notification> GetOne(Guid id)
        {
            return this.Collection.GetOneAsync(id);
        }

        public Task AddOrUpdate(Notification item)
        {
            return this.Collection.AddOrUpdate(item);
        }
    }
}