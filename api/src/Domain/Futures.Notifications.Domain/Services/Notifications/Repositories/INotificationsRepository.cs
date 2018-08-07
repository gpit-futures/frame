using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Futures.Notifications.Domain.Services.Notifications.Entities;

namespace Futures.Notifications.Domain.Services.Notifications.Repositories
{
    public interface INotificationsRepository
    {
        Task<IEnumerable<Notification>> GetAll();

        Task<IEnumerable<Notification>> GetAllByOds(string ods);

        Task<IEnumerable<Notification>> GetAllByOds(string ods, string username);

        Task<Notification> GetOne(Guid id);

        Task AddOrUpdate(Notification item);
    }
}