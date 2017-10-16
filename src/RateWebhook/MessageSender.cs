using Microsoft.Azure.ServiceBus;
using Newtonsoft.Json;
using System.Text;
using System.Threading.Tasks;

namespace RateWebhook
{
    public interface ISendMessage
    {
        Task Send<T>(T message);
    }
    public class MessageSender : ISendMessage
    {
        private readonly IProvideServiceBusConnection bus;
        public MessageSender(IProvideServiceBusConnection bus)
        {
            this.bus = bus;
        }
        public async Task Send<T>(T message)
        {
            var client = new QueueClient(bus.ConnectionString, bus.QueueName);
            var msg = new Message(Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(message)));
            await client.SendAsync(msg);
            System.Console.WriteLine("Message sent successfully!");
        }
    }
}
