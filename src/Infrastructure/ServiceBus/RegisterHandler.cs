namespace Infrastructure.ServiceBus
{
    public interface IRegisterHandler<T>
    {
    }

    public class RegisterHandler<T> : IRegisterHandler<T>
    {
        public RegisterHandler(IHandleMessage<T> handler, IProvideServiceBusConnection<T> bus)
        {
            bus.QueueClient.RegisterMessageHandler(handler.HandleAsync, handler.HandleOption);
        }
    }

    public class RegisterSubscriptionHandler<T> : IRegisterHandler<T>
    {
        public RegisterSubscriptionHandler(IHandleMessage<T> handler, IProvideServiceBusConnection<T> bus)
        {
            bus.SubscriptionClient.RegisterMessageHandler(handler.HandleAsync, handler.HandleOption);
        }
    }
}
