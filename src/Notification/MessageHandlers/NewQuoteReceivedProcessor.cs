using Infrastructure.ServiceBus;
using Newtonsoft.Json;
using Notification.Hubs;
using Notification.ResourceAccessors;
using System.Threading.Tasks;

namespace Notification.MessageHandlers
{
    public class NewQuoteReceivedProcessor : IProcessMessage
    {
        private readonly ICommandRA commandRA;
        private readonly INotifyRateFeedClient notifyRateFeedClient;

        public NewQuoteReceivedProcessor(ICommandRA commandRA, INotifyRateFeedClient notifyRateFeedClient)
        {
            this.commandRA = commandRA;
            this.notifyRateFeedClient = notifyRateFeedClient;
        }

        public async Task ProcessAsync<NewQuoteReceived>(NewQuoteReceived message)
        {
            var payload = JsonConvert.SerializeObject(message);
            var notifyRateFeedClients = notifyRateFeedClient.Notify(payload);

            await commandRA.SaveAsync(new Models.Notification
                {
                    Payload = payload
                });
        }
    }
}
