using Microsoft.Azure.ServiceBus;
using Pricing.MessageHandlers;

namespace Pricing
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
            new SubscriptionClient(bus.ConnectionString, bus.TopicName, bus.SubscriptionName)                
                .RegisterMessageHandler(handler.Handle, handler.HandleOption);
        }
    }
}
