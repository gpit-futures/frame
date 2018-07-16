using System.Collections.Generic;
using Futures.Infrastructure.MessageQueue;
using RawRabbit.Attributes;
using RawRabbit.Configuration.Exchange;

namespace Futures.Notifications.Domain.Messages.V1
{
    [Exchange(Type = ExchangeType.Direct, Name = "appointment.exchange")]
    [Routing(RoutingKey = "appointment.created", NoAck = true)]
    [Queue(Name = "created-appointment-queue", Durable = true)]
    public class AppointmentCreatedV1 : Dictionary<string, dynamic>, IMessageV1
    {
    }
}