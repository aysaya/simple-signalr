using Contracts;
using Infrastructure.ServiceBus;
using Notification.DomainModels;
using Notification.ResourceAccessors;
using System.Threading.Tasks;
using Infrastructure.Hubs;

namespace Notification.MessageHandlers
{
    public class NewQuoteReceivedProcessor : IProcessMessage<NewQuoteReceived>
    {
        private readonly ICommandRA<RateFeed> commandRA;
        private readonly IHubNotifier<RateFeed> notifyRateFeedClient;

        public NewQuoteReceivedProcessor(ICommandRA<RateFeed> commandRA, IHubNotifier<RateFeed> notifyRateFeedClient)
        {
            this.commandRA = commandRA;
            this.notifyRateFeedClient = notifyRateFeedClient;
        }

        public async Task ProcessAsync(NewQuoteReceived message)
        {
            var rateFeed = await commandRA.SaveAsync(new RateFeed
                {
                    BaseCurrency = message.BaseCurrency,
                    TradeCurrency = message.TradeCurrency,
                    Rate = message.Rate
                });

            await notifyRateFeedClient.SendAsync(rateFeed);
        }
    }
}
