using System;
using Futures.Domain.Interfaces;

namespace Futures.Dashboard.Domain.Services.RecentPatientLists.Entities
{
    public class RecentPatientList : IEntity
    {
        public Guid Id { get; set; }

        public string User { get; set; }

        public string Patients { get; set; }
    }
}