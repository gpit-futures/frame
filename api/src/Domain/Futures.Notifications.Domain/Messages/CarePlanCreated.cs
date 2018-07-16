﻿using System.Collections.Generic;
using Futures.Infrastructure.MessageQueue;
using RawRabbit.Attributes;
using RawRabbit.Configuration.Exchange;

namespace Futures.Notifications.Domain.Messages
{
    [Exchange(Type = ExchangeType.Direct, Name = "care.plan.exchange")]
    [Routing(RoutingKey = "care.plan.created", NoAck = true)]
    [Queue(Name = "created-care-plan-queue", Durable = true)]
    public class CarePlanCreated : IMessage
    {
        public string From { get; set; }

        public string To { get; set; }

        public Dictionary<string, dynamic> Body { get; set; }
    }
}