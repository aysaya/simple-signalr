using Microsoft.Azure.ServiceBus;
using Notification.Hubs;
using Notification.ResourceAccessors;
using System.Text;
using System.Threading.Tasks;

namespace Notification.MessageHandlers
{
    public interface IProcessMessage
    {
        Task ProcessAsync(Message message);
    }
    public class MessageProcessor : IProcessMessage
    {
        private readonly ICommandRA commandRA;
        private readonly INotifyRateFeedClient notifyRateFeedClient;

        public MessageProcessor(ICommandRA commandRA, INotifyRateFeedClient notifyRateFeedClient)
        {
            this.commandRA = commandRA;
            this.notifyRateFeedClient = notifyRateFeedClient;
        }

        public async Task ProcessAsync(Message message)
        {
            var body = Encoding.UTF8.GetString(message.Body);
            var notifyRateFeedClients = notifyRateFeedClient.Notify(body);
            await commandRA.SaveAsync(new Models.Notification { Payload = body});
        }

    }
}
