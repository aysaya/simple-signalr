using Microsoft.Azure.ServiceBus;

namespace Infrastructure.ServiceBus
{
    public interface IProvideServiceBusConnection
    {
        QueueClient QueueClient { get; }
        TopicClient TopicClient { get; }
        SubscriptionClient SubscriptionClient { get; }
    }

    public class ServiceBusConnectionProvider : IProvideServiceBusConnection
    {
        private readonly QueueClient queueClient;
        private readonly TopicClient topicClient;
        private readonly SubscriptionClient subscription;

        public ServiceBusConnectionProvider(
            string connectionString, string queueName = null,
            string topicName = null, string subscriptionName = null)
        {
            if (queueName != null)
                queueClient = new QueueClient(connectionString, queueName);

            if (topicName != null)
                topicClient = new TopicClient(connectionString, topicName);

            if (subscription != null)
                subscription = new SubscriptionClient(connectionString, topicName, subscriptionName);
        }

        public QueueClient QueueClient { get => queueClient; }
        public TopicClient TopicClient { get => topicClient; }
        public SubscriptionClient SubscriptionClient { get => subscription; }        
    }
}
