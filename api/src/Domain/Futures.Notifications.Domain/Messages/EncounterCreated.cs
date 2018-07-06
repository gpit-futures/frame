using System.Collections.Generic;
using Futures.Infrastructure.MessageQueue;
using RawRabbit.Attributes;
using RawRabbit.Configuration.Exchange;

namespace Futures.Notifications.Domain.Messages
{
    [Exchange(Type = ExchangeType.Direct, Name = "encounter.exchange")]
    [Routing(RoutingKey = "encounter.created", NoAck = true)]
    [Queue(Name = "created-encounter-queue", Durable = true)]
    public class EncounterCreated : Dictionary<string, dynamic>, IMessage
    {
    }
}