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
    public class PatientCreatedHandler : MessageHandlerBase<Patient>, IMessageHandler<PatientCreated>
    {
        public PatientCreatedHandler(IHubContext<NotificationsHub> hub, 
            INotificationsRepository notifications)
        {
            this.Hub = hub;
            this.Notifications = notifications;
        }

        private IHubContext<NotificationsHub> Hub { get; }

        private INotificationsRepository Notifications { get; }

        public async Task Handle(PatientCreated message)
        {
            var obj = this.ParseMessage(message);
            var usual = obj.Name.First(n => n.Use == HumanName.NameUse.Usual);

            var notification = new Notification
            {
                Ods = message.Destination,
                System = message.System,
                NhsNumber = obj.Identifier[0]?.Value,
                DateCreated = DateTime.UtcNow,
                Type = obj.TypeName,
                Summary = $"Patient Created: {obj.Identifier[0]?.Value}",
                Details = $"{usual?.Family}, {string.Join(" ", usual?.Given)} ({string.Join(" ", usual?.Prefix)})"
            };

            await this.Notifications.AddOrUpdate(notification);
            await this.Hub.Clients.Group(message.Destination?.ToLowerInvariant()).SendAsync("notification", notification);
        }
    }
}