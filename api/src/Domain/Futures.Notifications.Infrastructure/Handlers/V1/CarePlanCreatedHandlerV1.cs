using System;
using System.Linq;
using Futures.Infrastructure.Hubs;
using Futures.Infrastructure.MessageQueue;
using Futures.Notifications.Domain.Messages.V1;
using Futures.Notifications.Domain.Services.Notifications.Entities;
using Futures.Notifications.Domain.Services.Notifications.Repositories;
using Hl7.Fhir.Model;
using Microsoft.AspNetCore.SignalR;
using Task = System.Threading.Tasks.Task;

namespace Futures.Notifications.Infrastructure.Handlers.V1
{
    public class CarePlanCreatedHandlerV1 : MessageHandlerBaseV1<CarePlan>, IMessageHandlerV1<CarePlanCreatedV1>
    {
        public CarePlanCreatedHandlerV1(IHubContext<NotificationsHub> hub, 
            INotificationsRepository notifications)
        {
            this.Hub = hub;
            this.Notifications = notifications;
        }

        private IHubContext<NotificationsHub> Hub { get; }

        private INotificationsRepository Notifications { get; }

        public async Task Handle(CarePlanCreatedV1 message)
        {
            var careplan = this.ParseMessage(message);

            var activity = careplan.Activity.Select(x =>
                    $"{((CodeableConcept)x.Detail.Product).Coding[0].Display} ({((CodeableConcept)x.Detail.Product).Coding[0].Code}) - {x.Detail.Status}")
                .ToArray();

            var notification = new Notification
            {
                NhsNumber = careplan.Subject.Identifier.Value,
                DateCreated = careplan.Meta.LastUpdated?.DateTime ?? DateTime.UtcNow,
                Type = careplan.TypeName,
                Summary = $"{careplan.Title}. {careplan.Period.Start} to {careplan.Period.End}.",
                Details = $"{careplan.Title}. {careplan.Period.Start} to {careplan.Period.End}. {string.Join(", ", activity)}"
            };

            await this.Notifications.AddOrUpdate(notification);
            await this.Hub.Clients.All.SendAsync("notification", notification);
        }
    }
}