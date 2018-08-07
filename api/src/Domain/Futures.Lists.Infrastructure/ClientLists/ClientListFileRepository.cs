using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Futures.Lists.Domain.Services.ClientLists.Entities;
using Futures.Lists.Domain.Services.ClientLists.Repositories;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Futures.Lists.Infrastructure.ClientLists
{
    public class ClientListFileRepository : IClientListRepository
    {
        public async Task<IEnumerable<Client>> GetAllByUser(Guid id)
        {
            Dictionary<Guid, IEnumerable<Client>> clientLists;

            try
            {
                using (var reader = new StreamReader("client-list.json"))
                {
                    var json = await reader.ReadToEndAsync();
                    clientLists = JsonConvert.DeserializeObject<Dictionary<Guid, IEnumerable<Client>>>(json);
                }
            }
            catch (Exception e)
            {
                clientLists = new Dictionary<Guid, IEnumerable<Client>>();
            }

            return !clientLists.ContainsKey(id) 
                ? null 
                : clientLists[id];
        }

        public async Task AddOrUpdate(Guid id, IEnumerable<Client> clients)
        {
            Dictionary<Guid, IEnumerable<Client>> clientLists;

            try
            {
                using (var reader = new StreamReader("client-list.json"))
                {
                    var json = await reader.ReadToEndAsync();
                    clientLists = JsonConvert.DeserializeObject<Dictionary<Guid, IEnumerable<Client>>>(json);
                }
            }
            catch (Exception e)
            {
                clientLists = new Dictionary<Guid, IEnumerable<Client>>();
            }

            if (clientLists.ContainsKey(id))
            {
                clientLists[id] = clients;
            }
            else
            {
                clientLists.Add(id, clients);
            }

            try
            {
                using (var writer = new StreamWriter("client-list.json"))
                {
                    var settings =
                        new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver() };
                    var json = JsonConvert.SerializeObject(clientLists, Formatting.Indented, settings);

                    await writer.WriteAsync(json);
                }
            }
            catch (Exception e)
            {
            }
        }
    }
}