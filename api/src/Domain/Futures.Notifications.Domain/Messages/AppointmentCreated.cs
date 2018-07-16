﻿using System.Collections.Generic;
using Futures.Infrastructure.MessageQueue;
using RawRabbit.Attributes;
using RawRabbit.Configuration.Exchange;

namespace Futures.Notifications.Domain.Messages
{
    [Exchange(Type = ExchangeType.Direct, Name = "appointment.exchange")]
    [Routing(RoutingKey = "appointment.created", NoAck = true)]
    [Queue(Name = "created-appointment-queue", Durable = true)]
    public class AppointmentCreated : IMessage
    {
        public string From { get; set; }

        public string To { get; set; }

        public Dictionary<string, dynamic> Body { get; set; }
    }
}