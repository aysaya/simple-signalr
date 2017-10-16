namespace Notification
{
    public interface IProvideServiceBusConnection
    {
        string ConnectionString { get;}
        string TopicName { get; }
        string SubscriptionName { get; }
    }
    public class ServiceBusConnectionProvider : IProvideServiceBusConnection
    {
        private readonly string connectionString;
        private readonly string topicName;
        private readonly string subscriptionName;

        public ServiceBusConnectionProvider(string connectionString, string topicName, string subscriptionName)
        {
            this.connectionString = connectionString;
            this.topicName = topicName;
            this.subscriptionName = subscriptionName;
        }

        public string ConnectionString => connectionString;
        public string TopicName => topicName;
        public string SubscriptionName => subscriptionName;
    }
}
