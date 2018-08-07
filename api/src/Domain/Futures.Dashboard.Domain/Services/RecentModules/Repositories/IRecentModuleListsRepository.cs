using System;
using System.Threading.Tasks;
using Futures.Dashboard.Domain.Services.RecentModules.Entities;

namespace Futures.Dashboard.Domain.Services.RecentModules.Repositories
{
    public interface IRecentModuleListsRepository
    {
        Task<RecentModulesList> GetOneByUser(string user);

        Task<RecentModulesList> GetOne(Guid id);

        Task AddOrUpdate(RecentModulesList item);
    }
}