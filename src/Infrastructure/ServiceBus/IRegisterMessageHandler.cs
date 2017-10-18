namespace Infrastructure.ServiceBus
{
    public interface IRegisterMessageHandler<T>
    {
        void Register();
    }
}
