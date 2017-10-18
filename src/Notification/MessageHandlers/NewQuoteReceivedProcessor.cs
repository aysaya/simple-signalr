using Contracts;
using Infrastructure.ServiceBus;
using Newtonsoft.Json;
using Notification.Hubs;
using Notification.ResourceAccessors;
using System.Threading.Tasks;

namespace Notification.MessageHandlers
{
    public class NewQuoteReceivedProcessor<T> : IProcessMessage<T>
    {
        private readonly ICommandRA commandRA;
        private readonly INotifyRateFeedClient notifyRateFeedClient;

        public NewQuoteReceivedProcessor(ICommandRA commandRA, INotifyRateFeedClient notifyRateFeedClient)
        {
            this.commandRA = commandRA;
            this.notifyRateFeedClient = notifyRateFeedClient;
        }

        public async Task ProcessAsync(T message)
        {
            var payload = JsonConvert.SerializeObject(message as NewQuoteReceived);
            var notifyRateFeedClients = notifyRateFeedClient.Notify(payload);

            await commandRA.SaveAsync(new Models.Notification
                {
                    Payload = payload
                });
        }
    }
}
