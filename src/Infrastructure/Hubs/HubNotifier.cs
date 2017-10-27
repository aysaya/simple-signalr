using System.Threading.Tasks;

namespace Infrastructure.Hubs
{
    public interface IHubNotifier<T>
    {
        Task NotifyAsync(T t);
    }

    public class HubNotifier<T> : IHubNotifier<T>
    {
        private readonly IProvideHubContext<T> hubContext;
        private readonly IHubSender<T> hub;

        public HubNotifier(IProvideHubContext<T> hubContext, IHubSender<T> hub)
        {
            this.hubContext = hubContext;
            this.hub = hub;
        }

        public async Task NotifyAsync(T t)
        {
            await hub.SendAsync(t);
        }        
    }
}
