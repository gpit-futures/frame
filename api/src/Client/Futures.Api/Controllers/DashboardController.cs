using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Futures.Dashboard.Domain.Services.RecentPatientLists;
using Futures.Dashboard.Domain.Services.RecentPatientLists.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Futures.Api.Controllers
{
    [Produces("application/json")]
    [Authorize(Policy = "Read")]
    [Route("api/dashboard")]
    public class DashboardController : Controller
    {
        public DashboardController(IRecentPatientListsRepository recentPatientLists, 
            IRecentPatientListsService recentPatientListsService)
        {
            this.RecentPatientLists = recentPatientLists;
            this.RecentPatientListsService = recentPatientListsService;
        }

        private IRecentPatientListsRepository RecentPatientLists { get; }

        private IRecentPatientListsService RecentPatientListsService { get; }

        [HttpGet("recent-patients")]
        public async Task<IActionResult> GetRecentPatients()
        {
            var user = this.GetUser();

            if (string.IsNullOrWhiteSpace(user.Ods) || string.IsNullOrWhiteSpace(user.Username))
            {
                return this.BadRequest();
            }

            var recentPatients = await this.RecentPatientLists.GetOneByUser($"{user.Ods}:{user.Username}");

            return this.Ok(recentPatients);
        }

        [HttpPost("recent-patients")]
        public async Task<IActionResult> SaveRecentPatient(IEnumerable<Dictionary<string, dynamic>> patients)
        {
            var user = this.GetUser();

            if (string.IsNullOrWhiteSpace(user.Ods) || string.IsNullOrWhiteSpace(user.Username))
            {
                return this.BadRequest();
            }

            await this.RecentPatientListsService.AddOrUpdateUserRecentPatients($"{user.Ods}:{user.Username}", patients);

            return this.Ok();
        }

        private FuturesUser GetUser()
        {
            var ods = this.User.Claims.FirstOrDefault(c => c.Type == "odsId");
            var username = this.User.Claims.FirstOrDefault(c => c.Type == "user_name");

            return new FuturesUser(ods?.Value, username?.Value);
        }

        private class FuturesUser
        {
            public FuturesUser(string ods, string username)
            {
                this.Ods = ods;
                this.Username = username;
            }

            public string Ods { get; }

            public string Username { get;  }
        }
    }
}