using Futures.Infrastructure;
using Futures.Infrastructure.MessageQueue;
using Futures.Notifications.Domain.Messages;
using Hl7.Fhir.Model;
using Task = System.Threading.Tasks.Task;

namespace Futures.Notifications.Infrastructure.Handlers
{
    public class ObservationCreatedHandler : MessageHandlerBase<Observation>, IMessageHandler<ObservationCreated>
    {
        public async Task Handle(ObservationCreated message)
        {
            var observation = this.ParseMessage(message);
        }
    }
}