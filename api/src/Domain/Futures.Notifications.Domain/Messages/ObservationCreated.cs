using Futures.Infrastructure.MessageQueue;
using Hl7.Fhir.Model;
using RawRabbit.Attributes;
using RawRabbit.Configuration.Exchange;

namespace Futures.Notifications.Domain.Messages
{
    [Exchange(Type = ExchangeType.Direct, Name = "observation.exchange")]
    [Routing(RoutingKey = "observation.created", NoAck = true)]
    [Queue(Name = "created-observation-queue", Durable = true)]
    public class ObservationCreated : Observation, IMessage
    {
    }
}