namespace QuoteEngine
{
    public interface IProvideServiceBusConnection
    {
        string ConnectionString { get;}
        string QueueName { get; }
        string TopicName { get; }
    }

    public class ServiceBusConnectionProvider : IProvideServiceBusConnection
    {
        private readonly string connectionString;
        private readonly string queueName;
        private string topicName;

        public ServiceBusConnectionProvider(string connectionString, string queueName, string topicName)
        {
            this.connectionString = connectionString;
            this.queueName = queueName;
            this.topicName = topicName;
        }

        public string ConnectionString => connectionString;
        public string QueueName => queueName;
        public string TopicName => topicName;
    }
}
