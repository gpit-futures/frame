using System.Collections.Generic;
using Futures.Infrastructure.MessageQueue;
using RawRabbit.Attributes;

namespace Futures.Notifications.Domain.Messages
{
    [Exchange(Name = "patient.exchange")]
    [Routing(RoutingKey = "created-patients-queue", NoAck = true)]
    [Queue(Name = "created-patients-queue", Durable = true)]
    public class PatientCreated : IMessage
    {
        public string System { get; set; }

        public string Source { get; set; }

        public string Destination { get; set; }

        public Dictionary<string, dynamic> Body { get; set; }
    }
}