using Futures.Infrastructure.MessageQueue;
using Futures.Notifications.Domain.Messages.V1;
using Hl7.Fhir.Model;
using Task = System.Threading.Tasks.Task;

namespace Futures.Notifications.Infrastructure.Handlers.V1
{
    public class EncounterCreatedHandlerV1 : MessageHandlerBaseV1<Encounter>, IMessageHandlerV1<EncounterCreatedV1>
    {
        public async Task Handle(EncounterCreatedV1 message)
        {
            var encounter = this.ParseMessage(message);

            System.Console.WriteLine(encounter.Meta.LastUpdated);
        }
    }
}