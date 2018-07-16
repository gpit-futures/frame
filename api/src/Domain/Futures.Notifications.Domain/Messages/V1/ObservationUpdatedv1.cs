using System.Collections.Generic;
using Futures.Infrastructure.MessageQueue;
using RawRabbit.Attributes;
using RawRabbit.Configuration.Exchange;

namespace Futures.Notifications.Domain.Messages.V1
{
    [Exchange(Type = ExchangeType.Direct, Name = "observation.exchange")]
    [Routing(RoutingKey = "observation.updated", NoAck = true)]
    [Queue(Name = "updated-observation-queue", Durable = true)]
    public class ObservationUpdatedV1 : Dictionary<string, dynamic>, IMessageV1
    {
    }
}