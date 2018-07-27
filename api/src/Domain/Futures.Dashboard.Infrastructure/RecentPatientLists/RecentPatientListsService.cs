using System.Collections.Generic;
using System.Threading.Tasks;
using Futures.Dashboard.Domain.Services.RecentPatientLists;
using Futures.Dashboard.Domain.Services.RecentPatientLists.Entities;
using Futures.Dashboard.Domain.Services.RecentPatientLists.Repositories;

namespace Futures.Dashboard.Infrastructure.RecentPatientLists
{
    public class RecentPatientListsService : IRecentPatientListsService
    {
        public RecentPatientListsService(IRecentPatientListsRepository recentPatients)
        {
            this.RecentPatients = recentPatients;
        }

        private IRecentPatientListsRepository RecentPatients { get; }

        public async Task AddOrUpdateUserRecentPatients(string user, IEnumerable<Dictionary<string, dynamic>> patients)
        {
            var existing = await this.RecentPatients.GetOneByUser(user)
                           ?? new RecentPatientList
                           {
                               User = user
                           };

            existing.Patients = patients;

            await this.RecentPatients.AddOrUpdate(existing);
        }
    }
}