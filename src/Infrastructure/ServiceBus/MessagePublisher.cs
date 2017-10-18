using Microsoft.Azure.ServiceBus;
using Newtonsoft.Json;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.ServiceBus
{
    public interface IPublishMessage
    {
        Task PublishAsync<T>(T message);
    }

    public class MessagePublisher : IPublishMessage
    {
        private readonly IProvideServiceBusConnection bus;

        public MessagePublisher(IProvideServiceBusConnection bus)
        {
            this.bus = bus;
        }

        public async Task PublishAsync<T>(T message)
        {
            await bus.TopicClient.SendAsync(
                new Message
                (
                    Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(message))
                ));
            System.Console.WriteLine("Message published successfully!");
        }
    }
}
