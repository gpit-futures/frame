using System.Threading.Tasks;

namespace Futures.Infrastructure.MessageQueue
{
    public interface IMessageHandlerV1<in T> where T : class, IMessageV1
    {
        Task Handle(T message);
    }
}