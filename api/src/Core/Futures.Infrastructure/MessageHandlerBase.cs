using Hl7.Fhir.Model;
using Hl7.Fhir.Serialization;
using Newtonsoft.Json;

namespace Futures.Infrastructure
{
    public abstract class MessageHandlerBase<T> where T : Base
    {
        protected MessageHandlerBase()
        {
            this.Parser = new FhirJsonParser();
        }

        private FhirJsonParser Parser { get; }

        protected T ParseMessage(object message)
        {
            return this.Parser.Parse<T>(JsonConvert.SerializeObject(message));
        }
    }
}