using System;
using System.Collections.Generic;

namespace Futures.Api.Models
{
    public class ClientList
    {
        public Guid Id { get; set; }

        public string ApplicationName { get; set; }

        public string Publisher { get; set; }

        public string SourceUrl { get; set; }

        public IEnumerable<string> EventsOfInterest { get; set; } = new List<string>();
    }
}