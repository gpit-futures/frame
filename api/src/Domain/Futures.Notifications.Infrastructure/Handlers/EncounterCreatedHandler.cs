using Futures.Infrastructure.MessageQueue;
using Futures.Notifications.Domain.Messages;
using Hl7.Fhir.Model;
using Task = System.Threading.Tasks.Task;

namespace Futures.Notifications.Infrastructure.Handlers
{
    public class EncounterCreatedHandler : MessageHandlerBase<Encounter>, IMessageHandler<EncounterCreated>
    {
        public async Task Handle(EncounterCreated message)
        {
            var encounter = this.ParseMessage(message);

            System.Console.WriteLine(encounter.Meta.LastUpdated);
        }
    }
}