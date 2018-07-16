using System.Collections.Generic;
using Futures.Infrastructure.MessageQueue;
using RawRabbit.Attributes;
using RawRabbit.Configuration.Exchange;

namespace Futures.Notifications.Domain.Messages.V1
{
    [Exchange(Type = ExchangeType.Direct, Name = "observation.exchange")]
    [Routing(RoutingKey = "observation.created", NoAck = true)]
    [Queue(Name = "created-observation-queue", Durable = true)]
    public class ObservationCreatedV1 : Dictionary<string, dynamic>, IMessageV1
    {
    }
}