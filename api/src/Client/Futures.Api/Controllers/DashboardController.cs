using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Futures.Dashboard.Domain.Services.RecentModules.Entities;
using Futures.Dashboard.Domain.Services.RecentModules.Repositories;
using Futures.Dashboard.Domain.Services.RecentPatientLists;
using Futures.Lists.Domain.Services.ClientLists.Entities;
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
        public DashboardController(IRecentPatientListsService recentPatientListsService, 
            IRecentModuleListsRepository recentModules)
        {
            this.RecentPatientListsService = recentPatientListsService;
            this.RecentModules = recentModules;
        }

        private IRecentPatientListsService RecentPatientListsService { get; }

        private IRecentModuleListsRepository RecentModules { get; }

        [HttpGet("recent-patients")]
        public async Task<IActionResult> GetRecentPatients()
        {
            var user = this.GetUser();

            if (string.IsNullOrWhiteSpace(user.Ods) || string.IsNullOrWhiteSpace(user.Username))
            {
                return this.BadRequest();
            }

            var recentPatients = await this.RecentPatientListsService.GetOneByUser($"{user.Ods}:{user.Username}");

            if (!string.IsNullOrWhiteSpace(recentPatients?.Patients))
            {
                var patients =
                    JsonConvert.DeserializeObject<IEnumerable<Dictionary<string, dynamic>>>(recentPatients.Patients);

                return this.Ok(patients);
            }

            return this.Ok(new List<object>());
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

        [HttpGet("recent-modules")]
        public async Task<IActionResult> GetRecentModules()
        {
            var user = this.GetUser();

            if (string.IsNullOrWhiteSpace(user.Ods) || string.IsNullOrWhiteSpace(user.Username))
            {
                return this.BadRequest();
            }

            var modules = await this.RecentModules.GetOneByUser($"{user.Ods}:{user.Username}");

            if (modules?.Modules != null)
            {
                return this.Ok(modules.Modules);
            }

            return this.Ok(new List<object>());
        }

        [HttpPost("recent-modules")]
        public async Task<IActionResult> SaveRecentModules([FromBody] IEnumerable<Client> modules)
        {
            var user = this.GetUser();

            if (string.IsNullOrWhiteSpace(user.Ods) || string.IsNullOrWhiteSpace(user.Username))
            {
                return this.BadRequest();
            }

            var recentModules = await this.RecentModules.GetOneByUser($"{user.Ods}:{user.Username}")
                                ?? new RecentModulesList
                                {
                                    User = $"{user.Ods}:{user.Username}"
                                };

            recentModules.Modules = modules;

            await this.RecentModules.AddOrUpdate(recentModules);

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