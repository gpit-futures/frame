using System.Threading.Tasks;
using Autofac;
using Microsoft.Extensions.DependencyInjection;
using RawRabbit;

namespace Futures.Infrastructure.MessageQueue
{
    public interface IMessageSubscription
    {
        Task Start(IBusClient bus, IContainer container);
    }
}