using Hl7.Fhir.Model;
using Hl7.Fhir.Serialization;
using Newtonsoft.Json;

namespace Futures.Infrastructure.MessageQueue
{
    public abstract class MessageHandlerBaseV1<T> where T : Base
    {
        protected MessageHandlerBaseV1()
        {
            this.Parser = new FhirJsonParser();
        }

        private FhirJsonParser Parser { get; }

        protected T ParseMessage(IMessageV1 message)
        {
            return this.Parser.Parse<T>(JsonConvert.SerializeObject(message));
        }
    }
}