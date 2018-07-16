using System;
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
    public class ObservationCreatedHandlerV1 : MessageHandlerBaseV1<Observation>, IMessageHandlerV1<ObservationCreatedV1>
    {
        public ObservationCreatedHandlerV1(IHubContext<NotificationsHub> hub, 
            INotificationsRepository notifications)
        {
            this.Hub = hub;
            this.Notifications = notifications;
        }

        private IHubContext<NotificationsHub> Hub { get; }

        private INotificationsRepository Notifications { get; }

        public async Task Handle(ObservationCreatedV1 message)
        {
            var observation = this.ParseMessage(message);

            var value = (SimpleQuantity) observation.Value;

            var notification = new Notification
            {
                NhsNumber = observation.Subject.Identifier.Value,
                DateCreated = observation.Meta.LastUpdated?.DateTime ?? DateTime.UtcNow,
                Type = observation.TypeName,
                Summary = $"{observation.Code.Coding[0].Display}.",
                Details = $"{observation.Code.Coding[0].Display}. Value: {value.Value}. Unit: {value.Unit}."
            };

            await this.Notifications.AddOrUpdate(notification);
            await this.Hub.Clients.All.SendAsync("notification", notification);
        }
    }
}