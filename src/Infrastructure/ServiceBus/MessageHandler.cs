using Microsoft.Azure.ServiceBus;
using Newtonsoft.Json;
using System;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Infrastructure.ServiceBus
{
    public interface IHandleMessage<T>
    {
        Task HandleAsync(Message message, CancellationToken token);
        Task HandleOption(ExceptionReceivedEventArgs arg);
    }

    public class MessageHandler<T> : IHandleMessage<T>
    {
        private IProcessMessage<T> messageProcessor;

        public MessageHandler(IProcessMessage<T> messageProcessor)
        {
            this.messageProcessor = messageProcessor;
        }

        public async Task HandleAsync(Message message, CancellationToken token)
        {
            var body = JsonConvert.DeserializeObject<T>(Encoding.UTF8.GetString(message.Body));
            await messageProcessor.ProcessAsync(body);
            Console.WriteLine($"Message {((dynamic)body).Id} handled successfully!");
        }

        public Task HandleOption(ExceptionReceivedEventArgs arg)
        {
            Console.WriteLine($"Message handler encountered an exception {arg.Exception}.");
            return Task.CompletedTask;
        }
    }
}
