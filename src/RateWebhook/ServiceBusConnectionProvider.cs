namespace RateWebhook
{
    public interface IProvideServiceBusConnection
    {
        string ConnectionString { get;}
        string QueueName { get; }
    }
    public class ServiceBusConnectionProvider : IProvideServiceBusConnection
    {
        private readonly string connectionString;
        private readonly string queueName;
        public ServiceBusConnectionProvider(string connectionString, string queueName)
        {
            this.connectionString = connectionString;
            this.queueName = queueName;
        }
        public string ConnectionString => connectionString;
        public string QueueName => queueName;
    }
}
