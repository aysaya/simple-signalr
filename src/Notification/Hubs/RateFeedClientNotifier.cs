using System.Threading.Tasks;

namespace Notification.Hubs
{
    public interface INotifyRateFeedClient
    {
        Task Notify(RateFeedData rateFeedData);
    }
    public class RateFeedClientNotifier : INotifyRateFeedClient
    {
        private readonly IProvideRateFeedClientContext rateFeedClientContext;

        public RateFeedClientNotifier(IProvideRateFeedClientContext rateFeedClientContext)
        {
            this.rateFeedClientContext = rateFeedClientContext;
        }

        public async Task Notify(RateFeedData rateFeedData)
        {
            var hub = new RateFeedHub
            {
                Clients = rateFeedClientContext.RateFeedClients.Clients
            };
            await hub.Send(rateFeedData);
        }
    }
}
