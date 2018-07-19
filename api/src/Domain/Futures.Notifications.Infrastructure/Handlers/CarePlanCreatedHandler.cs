using System;
using System.Linq;
using Futures.Application.Hubs;
using Futures.Infrastructure.MessageQueue;
using Futures.Notifications.Domain.Messages;
using Futures.Notifications.Domain.Services.Notifications.Entities;
using Futures.Notifications.Domain.Services.Notifications.Repositories;
using Hl7.Fhir.Model;
using Microsoft.AspNetCore.SignalR;
using Task = System.Threading.Tasks.Task;

namespace Futures.Notifications.Infrastructure.Handlers
{
    public class CarePlanCreatedHandler : MessageHandlerBase<CarePlan>, IMessageHandler<CarePlanCreated>
    {
        public CarePlanCreatedHandler(IHubContext<NotificationsHub> hub, 
            INotificationsRepository notifications)
        {
            this.Hub = hub;
            this.Notifications = notifications;
        }

        private IHubContext<NotificationsHub> Hub { get; }

        private INotificationsRepository Notifications { get; }

        public async Task Handle(CarePlanCreated message)
        {
            var obj = this.ParseMessage(message);

            var activity = obj.Activity.Select(x =>
                    $"{((CodeableConcept)x.Detail.Product).Coding[0].Display} ({((CodeableConcept)x.Detail.Product).Coding[0].Code}) - {x.Detail.Status}")
                .ToArray();

            var notification = new Notification
            {
                Ods = message.Destination,
                NhsNumber = obj.Subject.Identifier.Value,
                DateCreated = obj.Meta?.LastUpdated?.DateTime ?? DateTime.UtcNow,
                Type = obj.TypeName,
                Summary = $"{obj.Title}. {obj.Period.Start} to {obj.Period.End}.",
                Details = $"{obj.Title}. {obj.Period.Start} to {obj.Period.End}. {string.Join(", ", activity)}"
            };

            await this.Notifications.AddOrUpdate(notification);
            await this.Hub.Clients.All.SendAsync("notification", notification);
        }
    }
}