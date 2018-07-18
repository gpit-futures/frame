using System;
using System.Linq;
using Futures.Infrastructure.Hubs;
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
                // need the patient NHS
                NhsNumber = obj.Participant.SingleOrDefault(x => x.Actor.Reference.Contains("Patient"))?.Actor.Reference,
                DateCreated = obj.Meta.LastUpdated?.DateTime ?? DateTime.UtcNow,
                Type = obj.TypeName,
                Summary = $"{obj.Status}. {obj.Start?.DateTime:g} to {obj.End?.DateTime:g}.",
                Details = $"{obj.Status}. {obj.Start?.DateTime:g} to {obj.End?.DateTime:g}. {obj.Description}"
            };

            await this.Notifications.AddOrUpdate(notification);
            await this.Hub.Clients.All.SendAsync("notification", notification);
        }
    }
}