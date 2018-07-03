using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Autofac;
using Futures.Infrastructure.MessageQueue;
using RawRabbit;

namespace Futures.Infrastructure
{
    public class MessageSubscription : IMessageSubscription
    {
        public async Task Start(IBusClient bus, IContainer container)
        {
            var handlers = container.Resolve<IEnumerable<IMessageHandler<IMessage>>>();

            await Task.Run(() =>
            {
                handlers
                    .Select(h => bus
                        .SubscribeAsync<IMessage>((message, context) => h.Handle(message), config => config.WithSubscriberId(string.Empty))
                    );
            });
        }
    }
}