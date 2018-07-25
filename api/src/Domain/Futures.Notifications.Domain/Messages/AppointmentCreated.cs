using System.Collections.Generic;
using Futures.Infrastructure.MessageQueue;
using RawRabbit.Attributes;

namespace Futures.Notifications.Domain.Messages
{
    [Exchange(Name = "appointment.exchange")]
    [Routing(RoutingKey = "appointment.created", NoAck = true)]
    [Queue(Name = "created-appointment-queue", Durable = true)]
    public class AppointmentCreated : IMessage
    {
        public string System { get; set; }

        public string Source { get; set; }

        public string Destination { get; set; }

        public Dictionary<string, dynamic> Body { get; set; }
    }
}