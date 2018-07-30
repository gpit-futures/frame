using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Futures.Lists.Domain.Services.UserLists.Entities;
using Futures.Lists.Domain.Services.UserLists.Repositories;
using Newtonsoft.Json;

namespace Futures.Lists.Infrastructure.UserLists
{
    public class FuturesUserFileRepository : IFuturesUserRepository
    {
        public async Task<IEnumerable<FuturesUser>> GetAll()
        {
            IEnumerable<FuturesUser> users;

            try
            {
                using (var reader = new StreamReader("user-list.json"))
                {
                    var json = await reader.ReadToEndAsync();
                    users = JsonConvert.DeserializeObject<IEnumerable<FuturesUser>>(json);
                }
            }
            catch (Exception e)
            {
                users = new List<FuturesUser>();
            }

            return users;
        }

        public async Task<FuturesUser> GetOne(Guid id)
        {
            IEnumerable<FuturesUser> users;

            try
            {
                using (var reader = new StreamReader("user-list.json"))
                {
                    var json = await reader.ReadToEndAsync();
                    users = JsonConvert.DeserializeObject<IEnumerable<FuturesUser>>(json);
                }
            }
            catch (Exception e)
            {
                users = new List<FuturesUser>();
            }

            return users.FirstOrDefault(x => x.Id == id);
        }
    }
}