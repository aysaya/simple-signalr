using Microsoft.Azure.ServiceBus;
using System;
using System.Threading;
using System.Threading.Tasks;
using QuoteEngine.MessageHandlers;

namespace BasicQueueSender.MessageHandlers
{
    public interface IHandleMessage
    {
        Task Handle(Message message, CancellationToken token);
        Task HandleOption(ExceptionReceivedEventArgs arg);
    }

    public class MessageHandler : IHandleMessage
    {
        private IProcessMessage messageProcessor;

        public MessageHandler(IProcessMessage messageProcessor)
        {
            this.messageProcessor = messageProcessor;
        }

        public async Task Handle(Message message, CancellationToken token)
        {
            await messageProcessor.ProcessAsync(message);
            System.Console.WriteLine("Message handled successfully!");
        }

        public Task HandleOption(ExceptionReceivedEventArgs arg)
        {
            Console.WriteLine($"Message handler encountered an exception {arg.Exception}.");
            return Task.CompletedTask;
        }

    }
}
