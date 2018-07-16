using Autofac;
using RawRabbit;

namespace Futures.Infrastructure.MessageQueue
{
    public interface IMessageSubscriptionV1
    {
        void Start(IBusClient bus, IContainer container);
    }
}