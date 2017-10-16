using BasicQueueSender.MessageHandlers;
using Microsoft.Azure.ServiceBus;

namespace QuoteEngine
{
    public interface IRegisterMessageHandler
    {
        void Register();
    }
    public class RegisterMessageHandler : IRegisterMessageHandler
    {
        private readonly IHandleMessage handler;
        private readonly IProvideServiceBusConnection bus;
        public RegisterMessageHandler(IHandleMessage handler, IProvideServiceBusConnection bus)
        {
            this.handler = handler;
            this.bus = bus;
        }

        public void Register()
        {
            new QueueClient(bus.ConnectionString, bus.QueueName)
                .RegisterMessageHandler(handler.Handle, handler.HandleOption);
        }
    }
}
