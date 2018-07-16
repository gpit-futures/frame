using Futures.Infrastructure.Hubs;
using Futures.Infrastructure.MessageQueue;
using Futures.Notifications.Domain.Messages.V1;
using Hl7.Fhir.Model;
using Microsoft.AspNetCore.SignalR;
using Task = System.Threading.Tasks.Task;

namespace Futures.Notifications.Infrastructure.Handlers.V1
{
    public class AppointmentCreatedHandlerV1 : MessageHandlerBaseV1<Appointment>, IMessageHandlerV1<AppointmentCreatedV1>
    {
        public AppointmentCreatedHandlerV1(IHubContext<NotificationsHub> hub)
        {
            this.Hub = hub;
        }

        private IHubContext<NotificationsHub> Hub { get; }

        public async Task Handle(AppointmentCreatedV1 message)
        {
            
        }
    }
}