using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.ServiceBus
{
    public static class ServiceBusExtensions
    {
        public static IServiceCollection AddServiceBus(this IServiceCollection services,
            string connectionString, string queueName = null,
            string topicName = null, string subscriptionName = null)
        {
            var conn = new ServiceBusConnectionProvider(connectionString, queueName, topicName, subscriptionName);

            services.AddSingleton<IProvideServiceBusConnection>(conn);
            services.AddScoped<IHandleMessage, MessageHandler>();
            services.AddScoped<ISendMessage, MessageSender>();
            services.AddScoped<IPublishMessage, MessagePublisher>();
            return services;
        }
    }
}
