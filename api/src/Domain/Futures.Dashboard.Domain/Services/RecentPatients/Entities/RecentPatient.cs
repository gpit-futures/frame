using System;
using System.Collections.Generic;
using Futures.Domain.Interfaces;

namespace Futures.Dashboard.Domain.Services.RecentPatients.Entities
{
    public class RecentPatient : IEntity
    {
        public Guid Id { get; set; }

        public string User { get; set; }

        public DateTime DateAdded { get; set; }

        public Dictionary<string, dynamic> Patient { get; set; }
    }
}