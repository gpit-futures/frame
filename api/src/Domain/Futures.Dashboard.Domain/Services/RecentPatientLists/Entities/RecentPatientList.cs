using System;
using System.Collections.Generic;
using Futures.Domain.Interfaces;

namespace Futures.Dashboard.Domain.Services.RecentPatientLists.Entities
{
    public class RecentPatientList : IEntity
    {
        public Guid Id { get; set; }

        public string User { get; set; }

        public IEnumerable<Dictionary<string, dynamic>> Patients { get; set; } = new List<Dictionary<string, dynamic>>();
    }
}