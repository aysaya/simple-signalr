using Contracts;
using Infrastructure.ServiceBus;

namespace Notification
{
    public class RegisterNewQuoteReceivedHandler : IRegisterMessageHandler<NewQuoteReceived>
    {
        private readonly IHandleMessage handler;
        private readonly IProvideServiceBusConnection bus;

        public RegisterNewQuoteReceivedHandler(IHandleMessage handler, IProvideServiceBusConnection bus)
        {
            this.handler = handler;
            this.bus = bus;
        }

        public void Register()
        {
            bus.SubscriptionClient                
                .RegisterMessageHandler(handler.HandleAsync<NewQuoteReceived>, handler.HandleOption);
        }
    }
}
