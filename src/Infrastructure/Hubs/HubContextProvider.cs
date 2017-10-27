using Microsoft.AspNetCore.SignalR;

namespace Infrastructure.Hubs
{
    public interface IProvideHubContext<T>
    {
        IHubContext<HubSender<T>> HubContext { get; set; }
    }

    public class HubContextProvider<T> : IProvideHubContext<T>
    {
        IHubContext<HubSender<T>> IProvideHubContext<T>.HubContext { get; set; }
    }
}
