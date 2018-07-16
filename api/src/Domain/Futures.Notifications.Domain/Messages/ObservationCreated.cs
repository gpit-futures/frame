﻿using System.Collections.Generic;
using Futures.Infrastructure.MessageQueue;
using RawRabbit.Attributes;
using RawRabbit.Configuration.Exchange;

namespace Futures.Notifications.Domain.Messages
{
    [Exchange(Type = ExchangeType.Direct, Name = "observation.exchange")]
    [Routing(RoutingKey = "observation.created", NoAck = true)]
    [Queue(Name = "created-observation-queue", Durable = true)]
    public class ObservationCreated : IMessage
    {
        public string From { get; set; }

        public string To { get; set; }

        public Dictionary<string, dynamic> Body { get; set; }
    }
}