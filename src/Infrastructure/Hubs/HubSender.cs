using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

namespace Infrastructure.Hubs
{
    public interface IHubSender<T>
    {
        Task SendAsync(T t);
    }

    public class HubSender<T> : Hub, IHubSender<T>
    {
        public async Task SendAsync(T t)
        {
            await Clients.All.InvokeAsync("Send", t);
        }
    }
}
