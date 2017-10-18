using Contracts;
using Infrastructure.ServiceBus;

namespace QuoteEngine
{
    public class RegisterThirdPartyRateHandler: IRegisterMessageHandler<ThirdPartyRate>
    {
        private readonly IHandleMessage handler;
        private readonly IProvideServiceBusConnection bus;

        public RegisterThirdPartyRateHandler(IHandleMessage handler, IProvideServiceBusConnection bus)
        {
            this.handler = handler;
            this.bus = bus;
        }

        public void Register()
        {
            bus.QueueClient.RegisterMessageHandler(handler.HandleAsync<ThirdPartyRate>, handler.HandleOption);
        }
    }
}
