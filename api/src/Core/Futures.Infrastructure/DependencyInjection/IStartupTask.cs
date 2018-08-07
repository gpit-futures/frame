using System.Threading.Tasks;

namespace Futures.Infrastructure.DependencyInjection
{
    public interface IStartupTask
    {
        Task Start();
    }
}