using Microsoft.AspNetCore.SignalR;

namespace Notification.Hubs
{
    public interface IProvideRateFeedClientContext
    {
        IHubContext<RateFeedHub> RateFeedClients { get; set; }
    }

    public class RateFeedClients : IProvideRateFeedClientContext
    {
        IHubContext<RateFeedHub> IProvideRateFeedClientContext.RateFeedClients { get; set; }
    }
}
