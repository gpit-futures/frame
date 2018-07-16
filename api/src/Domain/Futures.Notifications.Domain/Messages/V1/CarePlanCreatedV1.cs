using System.Collections.Generic;
using Futures.Infrastructure.MessageQueue;
using RawRabbit.Attributes;
using RawRabbit.Configuration.Exchange;

namespace Futures.Notifications.Domain.Messages.V1
{
    [Exchange(Type = ExchangeType.Direct, Name = "care.plan.exchange")]
    [Routing(RoutingKey = "care.plan.created", NoAck = true)]
    [Queue(Name = "created-care-plan-queue", Durable = true)]
    public class CarePlanCreatedV1 : Dictionary<string, dynamic>, IMessageV1
    {
    }
}