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
    public class AppointmentCreatedHandler: MessageHandlerBase<Appointment>, IMessageHandler<AppointmentCreated>
    {
        public AppointmentCreatedHandler(IHubContext<NotificationsHub> hub, 
            INotificationsRepository notifications)
        {
            this.Hub = hub;
            this.Notifications = notifications;
        }

        private IHubContext<NotificationsHub> Hub { get; }

        private INotificationsRepository Notifications { get; }

        public async Task Handle(AppointmentCreated message)
        {
            var obj = this.ParseMessage(message);

            var notification = new Notification
            {
                Ods = message.Destination,
                System = message.System,
                // need the patient NHS
                NhsNumber = obj.Participant.SingleOrDefault(x => x.Actor.Reference.Contains("Patient"))?.Actor?.Identifier?.Value,
                DateCreated = DateTime.UtcNow,
                Type = obj.TypeName,
                Summary = $"Appointment {obj.Status}.",
                Details = $"{obj.Description}",
                Json = this.ToString()
            };

            await this.Notifications.AddOrUpdate(notification);
            await this.Hub.Clients.Group(message.Destination?.ToLowerInvariant()).SendAsync("notification", notification);
        }
    }
}