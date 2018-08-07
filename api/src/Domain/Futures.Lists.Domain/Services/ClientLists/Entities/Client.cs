using System;
using System.Collections.Generic;
using Futures.Domain.Interfaces;

namespace Futures.Lists.Domain.Services.ClientLists.Entities
{
    public class Client : IEntity
    {
        public Guid Id { get; set; }

        public string ApplicationName { get; set; }

        public string Publisher { get; set; }

        public string SourceUrl { get; set; }

        public IEnumerable<string> EventsOfInterest { get; set; } = new List<string>();
    }
}