using Microsoft.Azure.ServiceBus;
using Newtonsoft.Json;
using System;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Infrastructure.ServiceBus
{
    public interface IHandleMessage
    {
        Task HandleAsync<T>(Message message, CancellationToken token);
        Task HandleOption(ExceptionReceivedEventArgs arg);
    }

    public class MessageHandler : IHandleMessage
    {
        private IProcessMessage messageProcessor;

        public MessageHandler(IProcessMessage messageProcessor)
        {
            this.messageProcessor = messageProcessor;
        }

        public async Task HandleAsync<T>(Message message, CancellationToken token)
        {
            var body = JsonConvert.DeserializeObject<T>(Encoding.UTF8.GetString(message.Body));
            await messageProcessor.ProcessAsync(body);
            System.Console.WriteLine("Message handled successfully!");
        }

        public Task HandleOption(ExceptionReceivedEventArgs arg)
        {
            Console.WriteLine($"Message handler encountered an exception {arg.Exception}.");
            return Task.CompletedTask;
        }

    }
}
