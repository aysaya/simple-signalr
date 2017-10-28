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

        public HubNotifier(IProvideHubContext<T> hubContext)
        {
            this.hubContext = hubContext;
        }

        public async Task NotifyAsync(T t)
        {
            var sender = new HubSender<T>
            {
                Clients = hubContext.HubContext.Clients
            };
            
            await sender.SendAsync(t);
        }        
    }
}
