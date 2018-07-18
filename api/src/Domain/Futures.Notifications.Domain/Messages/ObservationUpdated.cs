using System.Collections.Generic;
using Futures.Infrastructure.MessageQueue;
using RawRabbit.Attributes;

namespace Futures.Notifications.Domain.Messages
{
    [Exchange(Name = "observation.exchange")]
    [Routing(RoutingKey = "observation.updated", NoAck = true)]
    [Queue(Name = "updated-observation-queue", Durable = true)]
    public class ObservationUpdated : IMessage
    {
        public string Source { get; set; }

        public string Destination { get; set; }

        public Dictionary<string, dynamic> Body { get; set; }
    }
}