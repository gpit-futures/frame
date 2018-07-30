using System.Collections.Generic;
using System.Threading.Tasks;

namespace Futures.Dashboard.Domain.Services.RecentPatientLists
{
    public interface IRecentPatientListsService
    {
        Task AddOrUpdateUserRecentPatients(string user, string patients);
    }
}