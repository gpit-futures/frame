using Futures.Infrastructure.MessageQueue;
using Futures.Notifications.Domain.Messages;
using Hl7.Fhir.Model;
using Task = System.Threading.Tasks.Task;

namespace Futures.Notifications.Infrastructure.Handlers
{
    public class CarePlanCreatedHandler : MessageHandlerBase<CarePlan>, IMessageHandler<CarePlanCreated>
    {
        public async Task Handle(CarePlanCreated message)
        {
            var careplan = this.ParseMessage(message);
        }
    }
}