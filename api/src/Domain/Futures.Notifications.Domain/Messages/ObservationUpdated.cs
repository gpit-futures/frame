﻿using System.Collections.Generic;
using Futures.Infrastructure.MessageQueue;
using RawRabbit.Attributes;
using RawRabbit.Configuration.Exchange;

namespace Futures.Notifications.Domain.Messages
{
    [Exchange(Type = ExchangeType.Direct, Name = "observation.exchange")]
    [Routing(RoutingKey = "observation.updated", NoAck = true)]
    [Queue(Name = "updated-observation-queue", Durable = true)]
    public class ObservationUpdated : IMessage
    {
        public string From { get; set; }

        public string To { get; set; }

        public Dictionary<string, dynamic> Body { get; set; }
    }
}