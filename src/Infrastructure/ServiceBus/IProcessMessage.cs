using System.Threading.Tasks;

namespace Infrastructure.ServiceBus
{
    public interface IProcessMessage
    {
        Task ProcessAsync<T>(T message);
    }
}
