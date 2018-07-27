using System;
using System.Collections.Generic;
using System.IO;
using Futures.Api.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Futures.Api.Controllers
{
    [Produces("application/json")]
    [Authorize(Policy = "Read")]
    [Route("api/client-lists")]
    public class ClientListController : Controller
    {
        [HttpGet("{id}")]
        public IActionResult GetUserModules(Guid id)
        {

            Dictionary<Guid, IEnumerable<ClientList>> clientLists;

            using (var reader = new StreamReader("client-list.json"))
            {
                var json = reader.ReadToEnd();
                clientLists = JsonConvert.DeserializeObject<Dictionary<Guid, IEnumerable<ClientList>>>(json);
            }


            if (!clientLists.ContainsKey(id))
            {
                return this.NotFound();
            }

            return this.Ok(clientLists[id]);
        }

        [HttpPost("{id}")]
        public IActionResult SaveUserModules(Guid id, [FromBody] IEnumerable<ClientList> modules)
        {
            Dictionary<Guid, IEnumerable<ClientList>> clientLists;

            using (var reader = new StreamReader("client-list.json"))
            {
                var json = reader.ReadToEnd();
                clientLists = JsonConvert.DeserializeObject<Dictionary<Guid, IEnumerable<ClientList>>>(json);
            }

            if (clientLists.ContainsKey(id))
            {
                clientLists[id] = modules;
            }
            else
            {
                clientLists.Add(id, modules);
            }

            using (var writer = new StreamWriter("client-list.json"))
            {
                var settings =
                    new JsonSerializerSettings {ContractResolver = new CamelCasePropertyNamesContractResolver()};
                var json = JsonConvert.SerializeObject(clientLists, Formatting.Indented, settings);
                writer.Write(json);
            }

            return this.Ok();
        }
    }
}