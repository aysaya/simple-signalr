using Microsoft.Azure.ServiceBus;
using Newtonsoft.Json;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.ServiceBus
{
    public interface ISendMessage<T>
    {
        Task SendAsync(T message);
    }

    public class MessageSender<T> : ISendMessage<T>
    {
        private readonly IProvideServiceBusConnection<T> bus;

        public MessageSender(IProvideServiceBusConnection<T> bus)
        {
            this.bus = bus;
        }

        public async Task SendAsync(T message)
        {
            await bus.QueueClient.SendAsync
                (
                    new Message(Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(message)))
                );
            System.Console.WriteLine($"Message {((dynamic)message).Id} sent successfully!");
        }
    }

    public interface IPublishMessage<T>
    {
        Task SendAsync(T message);
    }

    public class TopicSender<T> : IPublishMessage<T>
    {
        private readonly IProvideServiceBusConnection<T> bus;

        public TopicSender(IProvideServiceBusConnection<T> bus)
        {
            this.bus = bus;
        }
        
        public async Task SendAsync(T message)
        {
            await bus.TopicClient.SendAsync
                (
                    new Message(Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(message)))
                );
            System.Console.WriteLine($"Message {((dynamic)message).Id} published successfully!");
        }
    }
}
