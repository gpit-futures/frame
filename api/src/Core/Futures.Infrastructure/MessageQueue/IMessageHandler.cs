using System.Threading.Tasks;

namespace Futures.Infrastructure.MessageQueue
{
    public interface IMessageHandler<in T> where T : IMessage
    {
        Task Handle(T message);
    }
}