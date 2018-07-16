using Futures.Infrastructure.MessageQueue;
using Futures.Notifications.Domain.Messages;
using Hl7.Fhir.Model;
using Task = System.Threading.Tasks.Task;

namespace Futures.Notifications.Infrastructure.Handlers
{
    public class AppointmentCreatedHandler : MessageHandlerBase<Appointment>, IMessageHandler<AppointmentCreated>
    {
        public async Task Handle(AppointmentCreated message)
        {
            
        }
    }
}