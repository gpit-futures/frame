using System.Collections.Generic;
using Futures.Infrastructure.MessageQueue;
using RawRabbit.Attributes;

namespace Futures.Notifications.Domain.Messages
{
    [Exchange(Name = "careplan.exchange")]
    [Routing(RoutingKey = "careplan.created", NoAck = true)]
    [Queue(Name = "created-care-plan-queue", Durable = true)]
    public class CarePlanCreated : IMessage
    {
        public string Source { get; set; }

        public string Destination { get; set; }

        public Dictionary<string, dynamic> Body { get; set; }
    }
}