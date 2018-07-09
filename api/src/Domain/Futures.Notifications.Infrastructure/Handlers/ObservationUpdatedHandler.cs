using Futures.Infrastructure.MessageQueue;
using Futures.Notifications.Domain.Messages;
using Hl7.Fhir.Model;
using Task = System.Threading.Tasks.Task;

namespace Futures.Notifications.Infrastructure.Handlers
{
    public class ObservationUpdatedHandler : MessageHandlerBase<Observation>, IMessageHandler<ObservationUpdated>
    {
        public async Task Handle(ObservationUpdated message)
        {
            var observation = this.ParseMessage(message);
        }
    }
}