using Autofac;
using RawRabbit;

namespace Futures.Infrastructure.MessageQueue
{
    public interface IMessageSubscription
    {
        void Start(IBusClient bus, IContainer container);
    }
}