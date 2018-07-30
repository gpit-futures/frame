using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Futures.Dashboard.Domain.Services.RecentPatientLists;
using Futures.Dashboard.Domain.Services.RecentPatientLists.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

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

            if (!string.IsNullOrWhiteSpace(recentPatients?.Patients))
            {
                var patients =
                    JsonConvert.DeserializeObject<IEnumerable<Dictionary<string, dynamic>>>(recentPatients.Patients);

                return this.Ok(patients);
            }

            return this.Ok(new List<Dictionary<string, dynamic>>());
        }

        [HttpPost("recent-patients")]
        public async Task<IActionResult> SaveRecentPatient([FromBody] IEnumerable<Dictionary<string, dynamic>> patients)
        {
            var user = this.GetUser();

            if (string.IsNullOrWhiteSpace(user.Ods) || string.IsNullOrWhiteSpace(user.Username))
            {
                return this.BadRequest();
            }

            var json = JsonConvert.SerializeObject(patients);

            await this.RecentPatientListsService.AddOrUpdateUserRecentPatients($"{user.Ods}:{user.Username}", json);

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