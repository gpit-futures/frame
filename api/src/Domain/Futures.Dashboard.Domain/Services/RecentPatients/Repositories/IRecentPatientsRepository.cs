using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Futures.Dashboard.Domain.Services.RecentPatients.Entities;

namespace Futures.Dashboard.Domain.Services.RecentPatients.Repositories
{
    public interface IRecentPatientsRepository
    {
        Task<IEnumerable<RecentPatient>> GetAllByUser(string user);

        Task<IEnumerable<RecentPatient>> GetAllByUser(string user, int limit);

        Task<RecentPatient> GetOne(Guid id);

        Task Delete(Guid id);

        Task DeleteMany(IEnumerable<RecentPatient> patients);

        Task AddOrUpdate(RecentPatient item);
    }
}