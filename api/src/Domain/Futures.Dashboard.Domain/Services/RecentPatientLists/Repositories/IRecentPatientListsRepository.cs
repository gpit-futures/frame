using System;
using System.Threading.Tasks;
using Futures.Dashboard.Domain.Services.RecentPatientLists.Entities;

namespace Futures.Dashboard.Domain.Services.RecentPatientLists.Repositories
{
    public interface IRecentPatientListsRepository
    {
        Task<RecentPatientList> GetOneByUser(string user);

        Task<RecentPatientList> GetOne(Guid id);

        Task AddOrUpdate(RecentPatientList item);
    }
}