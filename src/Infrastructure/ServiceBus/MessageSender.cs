using Microsoft.Azure.ServiceBus;
using Newtonsoft.Json;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.ServiceBus
{
    public interface ISendMessage
    {
        Task SendAsync<T>(T message);
    }
    public class MessageSender : ISendMessage
    {
        private readonly IProvideServiceBusConnection bus;

        public MessageSender(IProvideServiceBusConnection bus)
        {
            this.bus = bus;
        }
        
        public async Task SendAsync<T>(T message)
        {
            await bus.QueueClient.SendAsync
                (
                    new Message(Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(message)))
                );
            System.Console.WriteLine("Message sent successfully!");
        }
    }
}
