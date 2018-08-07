using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Futures.Lists.Domain.Services.ClientLists.Entities;
using Futures.Lists.Domain.Services.ClientLists.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Futures.Api.Controllers
{
    [Produces("application/json")]
    [Route("api/client-lists")]
    public class ClientListController : Controller
    {
        public ClientListController(IClientListRepository clientLists)
        {
            this.ClientLists = clientLists;
        }

        private IClientListRepository ClientLists { get; }

        [AllowAnonymous]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserModules(Guid id)
        {

            var clientList = await this.ClientLists.GetAllByUser(id);

            if (clientList == null)
            {
                return this.NotFound();
            }

            return this.Ok(clientList);
        }

        [Authorize(Policy = "Read")]
        [HttpPost("{id}")]
        public async Task<IActionResult> SaveUserModules(Guid id, [FromBody] IEnumerable<Client> modules)
        {
            await this.ClientLists.AddOrUpdate(id, modules);

            return this.Ok();
        }
    }
}