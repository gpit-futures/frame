using System;
using System.Collections.Generic;
using Futures.Domain.Interfaces;
using Futures.Lists.Domain.Services.ClientLists.Entities;

namespace Futures.Dashboard.Domain.Services.RecentModules.Entities
{
    public class RecentModulesList : IEntity
    {
        public Guid Id { get; set; }

        public string User { get; set; }

        public IEnumerable<Client> Modules { get; set; } = new List<Client>();
    }
}