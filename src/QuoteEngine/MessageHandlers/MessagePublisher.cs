using Microsoft.Azure.ServiceBus;
using Newtonsoft.Json;
using System.Text;
using System.Threading.Tasks;

namespace QuoteEngine.MessageHandlers
{
    public interface IPublishMessage
    {
        Task Publish(string message);
    }
    public class MessagePublisher : IPublishMessage
    {
        private readonly IProvideServiceBusConnection bus;

        public MessagePublisher(IProvideServiceBusConnection bus)
        {
            this.bus = bus;
        }

        public async Task Publish(string message)
        {
            var client = new TopicClient(bus.ConnectionString, bus.TopicName);

            await client.SendAsync(
                new Message
                (
                    Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(message))
                ));
            System.Console.WriteLine("Message published successfully!");
        }
    }
}
