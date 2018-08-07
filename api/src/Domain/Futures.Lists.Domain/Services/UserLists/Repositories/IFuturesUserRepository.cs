using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Futures.Lists.Domain.Services.UserLists.Entities;

namespace Futures.Lists.Domain.Services.UserLists.Repositories
{
    public interface IFuturesUserRepository
    {
        Task<IEnumerable<FuturesUser>> GetAll();

        Task<FuturesUser> GetOne(Guid id);
    }
}