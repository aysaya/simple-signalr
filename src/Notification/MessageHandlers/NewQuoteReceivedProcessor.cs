using Contracts;
using Infrastructure.ServiceBus;
using Notification.Hubs;
using Notification.Models;
using Notification.ResourceAccessors;
using System.Threading.Tasks;

namespace Notification.MessageHandlers
{
    public class NewQuoteReceivedProcessor : IProcessMessage<NewQuoteReceived>
    {
        private readonly ICommandRA<RateFeed> commandRA;
        private readonly INotifyRateFeedClient notifyRateFeedClient;

        public NewQuoteReceivedProcessor(ICommandRA<RateFeed> commandRA, INotifyRateFeedClient notifyRateFeedClient)
        {
            this.commandRA = commandRA;
            this.notifyRateFeedClient = notifyRateFeedClient;
        }

        public async Task ProcessAsync(NewQuoteReceived message)
        {
            var saveTask = commandRA.SaveAsync(new RateFeed
                {
                    BaseCurrency = message.BaseCurrency,
                    TradeCurrency = message.TradeCurrency,
                    Rate = message.Rate
                });

            var notifyTask = notifyRateFeedClient.Notify
                (
                    new RateFeedData
                    {
                        BaseCurrency = message.BaseCurrency,
                        TargetCurrency = message.TradeCurrency,
                        RateValue = message.Rate
                    }
                );

            await Task.WhenAll(saveTask, notifyTask);
        }
    }
}
