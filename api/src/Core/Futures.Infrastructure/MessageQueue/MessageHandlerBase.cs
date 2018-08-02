using System.Collections.Generic;
using System.Linq;
using Hl7.Fhir.Model;
using Hl7.Fhir.Serialization;
using Newtonsoft.Json;

namespace Futures.Infrastructure.MessageQueue
{
    public abstract class MessageHandlerBase<T> where T : Base
    {
        protected MessageHandlerBase()
        {
            this.Parser = new FhirJsonParser(new ParserSettings
            {
                AcceptUnknownMembers = true
            });

            this.Serializer = new FhirJsonSerializer();
        }

        private FhirJsonParser Parser { get; }

        private FhirJsonSerializer Serializer { get; }

        private T Resource { get; set; }

        protected T ParseMessage(IMessage message)
        {
            var body = MessageHandlerBase<T>.IgnoreNulls(message.Body);
            this.Resource = this.Parser.Parse<T>(JsonConvert.SerializeObject(body));

            return this.Resource;
        }

        public override string ToString()
        {
            return this.Serializer.SerializeToString(this.Resource);
        }

        private static IDictionary<string, dynamic> IgnoreNulls(IDictionary<string, dynamic> body)
        {
            return body.Where(field => field.Value != null).ToDictionary(field => field.Key, field => field.Value);
        }
    }
}