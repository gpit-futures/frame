using System;
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
    public class ObservationCreatedHandler : MessageHandlerBase<Observation>, IMessageHandler<ObservationCreated>
    {
        public ObservationCreatedHandler(IHubContext<NotificationsHub> hub, 
            INotificationsRepository notifications)
        {
            this.Hub = hub;
            this.Notifications = notifications;
        }

        private IHubContext<NotificationsHub> Hub { get; }

        private INotificationsRepository Notifications { get; }

        public async Task Handle(ObservationCreated message)
        {
            var obj = this.ParseMessage(message);

            var value = (SimpleQuantity)obj.Value;

            var notification = new Notification
            {
                Ods = message.Destination,
                System = message.System,
                NhsNumber = obj.Subject.Identifier.Value,
                DateCreated = DateTime.UtcNow,
                Type = obj.TypeName,
                Summary = $"{obj.Code.Coding[0].Display}.",
                Details = $"Value: {value.Value}. Unit: {value.Unit}.",
                Json = this.ToString()
            };

            await this.Notifications.AddOrUpdate(notification);
            await this.Hub.Clients.Group(message.Destination?.ToLowerInvariant()).SendAsync("notification", notification);
        }
    }
}