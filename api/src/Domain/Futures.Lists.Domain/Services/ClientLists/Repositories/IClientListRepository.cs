using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Futures.Lists.Domain.Services.ClientLists.Entities;

namespace Futures.Lists.Domain.Services.ClientLists.Repositories
{
    public interface IClientListRepository
    {
        Task<IEnumerable<Client>> GetAllByUser(Guid id);

        Task AddOrUpdate(Guid id, IEnumerable<Client> clients);
    }
}