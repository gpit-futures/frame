using System;
using System.Collections.Generic;
using Futures.Domain.Interfaces;

namespace Futures.Notifications.Domain.Services.Notifications.Entities
{
    public class Notification : IEntity
    {
        public Guid Id { get; set; }

        public string Ods { get; set; }

        public IEnumerable<string> Read { get; set; } = new List<string>();

        public string Summary { get; set; }

        public DateTime DateCreated { get; set; }
    }
}