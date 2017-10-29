using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

namespace Infrastructure.Hubs
{
    public interface IHubNotifier<T>
    {
        Task SendAsync(T t);
    }

    public class HubNotifier<T> : Hub, IHubNotifier<T>
    {
        private readonly IHubSender<T> hubSender;

        public HubNotifier(IHubSender<T> hubSender)
        {
            this.hubSender = hubSender;
        }

        public async Task SendAsync(T t)
        {
            await hubSender.SendAsync(t);
        }        
    }
}
