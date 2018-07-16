using System.Collections.Generic;
using Futures.Infrastructure.MessageQueue;
using RawRabbit.Attributes;
using RawRabbit.Configuration.Exchange;

namespace Futures.Notifications.Domain.Messages
{
    [Exchange(Type = ExchangeType.Direct, Name = "appointment.exchange")]
    [Routing(RoutingKey = "appointment.created", NoAck = true)]
    [Queue(Name = "created-appointment-queue", Durable = true)]
    public class AppointmentCreated : Dictionary<string, dynamic>, IMessage
    {
    }
}