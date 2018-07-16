using System.Collections.Generic;
using Futures.Infrastructure.MessageQueue;
using RawRabbit.Attributes;
using RawRabbit.Configuration.Exchange;

namespace Futures.Notifications.Domain.Messages.V1
{
    [Exchange(Type = ExchangeType.Direct, Name = "encounter.exchange")]
    [Routing(RoutingKey = "encounter.created", NoAck = true)]
    [Queue(Name = "created-encounter-queue", Durable = true)]
    public class EncounterCreatedV1 : Dictionary<string, dynamic>, IMessageV1
    {
    }
}