using System.Collections.Generic;
using System.Threading.Tasks;
using Futures.Dashboard.Domain.Services.RecentPatientLists.Entities;

namespace Futures.Dashboard.Domain.Services.RecentPatientLists
{
    public interface IRecentPatientListsService
    {
        Task<RecentPatientList> GetOneByUser(string user);

        Task AddOrUpdateUserRecentPatients(string user, string patients);
    }
}