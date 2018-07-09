using System.Collections.Generic;
using Futures.Infrastructure.MessageQueue;
using RawRabbit.Attributes;
using RawRabbit.Configuration.Exchange;

namespace Futures.Notifications.Domain.Messages
{
    [Exchange(Type = ExchangeType.Direct, Name = "observation.exchange")]
    [Routing(RoutingKey = "observation.updated", NoAck = true)]
    [Queue(Name = "updated-observation-queue", Durable = true)]
    public class ObservationUpdated : Dictionary<string, dynamic>, IMessage
    {
    }
}