using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Infrastructure.ServiceBus
{
    public static class ServiceBusExtensions
    {
        public static IServiceCollection AddQueueSender<T>(this IServiceCollection services,
            string connectionString, string queueName)
        {
            var conn = new ServiceBusConnectionProvider<T>(connectionString, queueName);
            services.AddScoped(typeof(IProvideServiceBusConnection<T>), (p => conn));
            services.AddScoped(typeof(ISendMessage<T>), typeof(MessageSender<T>));

            return services;
        }

        public static IServiceCollection AddQueueHandler<T, TProcessor>(this IServiceCollection services,
            string connectionString, string queueName)
        {
            var conn = new ServiceBusConnectionProvider<T>(connectionString, queueName);
            services.AddScoped(typeof(IProvideServiceBusConnection<T>), (p => conn));
            services.AddScoped(typeof(IHandleMessage<T>), typeof(MessageHandler<T>));
            services.AddScoped(typeof(IRegisterHandler<T>), typeof(RegisterHandler<T>));
            services.AddScoped(typeof(IProcessMessage<T>), typeof(TProcessor));

            return services;
        }

        public static IServiceCollection AddTopicSender<T>(this IServiceCollection services,
            string connectionString, string topicName)
        {
            var conn = new ServiceBusConnectionProvider<T>(connectionString, topicName, null);
            services.AddScoped(typeof(IProvideServiceBusConnection<T>), (p => conn));
            services.AddScoped(typeof(ISendMessage<T>), typeof(TopicSender<T>));

            return services;
        }

        public static IServiceCollection AddSubscriptionHandler<T, TProcessor>(this IServiceCollection services,
            string connectionString, string topicName, string subscriptionName)
        {
            var conn = new ServiceBusConnectionProvider<T>(connectionString, topicName, subscriptionName);
            services.AddScoped(typeof(IProvideServiceBusConnection<T>), (p => conn));
            services.AddScoped(typeof(IHandleMessage<T>), typeof(MessageHandler<T>));
            services.AddScoped(typeof(IRegisterHandler<T>), typeof(RegisterSubscriptionHandler<T>));
            services.AddScoped(typeof(IProcessMessage<T>), typeof(TProcessor));

            return services;
        }

        public static IApplicationBuilder RegisterHandler<T>(this IApplicationBuilder app, IServiceProvider serviceProvider)
        {
            serviceProvider.GetService<IRegisterHandler<T>>();
            
            return app; 
        }

        public static IApplicationBuilder RegisterSubscriptionHandler<T>(this IApplicationBuilder app, IServiceProvider serviceProvider)
        {
            serviceProvider.GetService<IRegisterHandler<T>>();
            return app;
        }
    }
}
