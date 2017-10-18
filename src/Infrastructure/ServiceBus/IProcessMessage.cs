using System.Threading.Tasks;

namespace Infrastructure.ServiceBus
{
    public interface IProcessMessage<T>
    {
        Task ProcessAsync(T message);
    }
}
