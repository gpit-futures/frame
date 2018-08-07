using System.Collections.Generic;
using Futures.Infrastructure.MessageQueue;
using RawRabbit.Attributes;

namespace Futures.Notifications.Domain.Messages
{
    [Exchange(Name = "encounter.exchange")]
    [Routing(RoutingKey = "encounter.created", NoAck = true)]
    [Queue(Name = "created-encounter-queue", Durable = true)]
    public class EncounterCreated : IMessage
    {
        public string System { get; set; }

        public string Source { get; set; }

        public string Destination { get; set; }

        public Dictionary<string, dynamic> Body { get; set; }
    }
}