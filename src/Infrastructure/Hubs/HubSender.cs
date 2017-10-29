using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

namespace Infrastructure.Hubs
{
    public interface IHubSender<T>
    {
        Task SendAsync(T t);
    }
    public class HubSender<T, THub> : IHubSender<T> where THub: Hub
    {
        private readonly IHubContext<THub> hubContext;

        public HubSender(IHubContext<THub> hubContext)
        {
            this.hubContext = hubContext;
        }
        public async Task SendAsync(T t)
        {
            await hubContext.Clients.All.InvokeAsync("Send", t);
        }
    }
}
