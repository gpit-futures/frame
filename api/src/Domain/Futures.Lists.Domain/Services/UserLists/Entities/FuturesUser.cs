using System;
using Futures.Domain.Interfaces;

namespace Futures.Lists.Domain.Services.UserLists.Entities
{
    public class FuturesUser : IEntity
    {
        public Guid Id { get; set; }

        public string Username { get; set; }

        public string Title { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string ClientId { get; set; }

        public string ClientSecret { get; set; }
    }
}