using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

namespace Infrastructure.Hubs
{
    public class HubSender<T> : Hub
    {
        public async Task SendAsync(T t)
        {
            await Clients.All.InvokeAsync("Send", t);
        }
    }
}
