using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Infrastructure.Hubs
{
    public static class HubsExtension
    {
        public static IServiceCollection AddHubSignalR<T>(this IServiceCollection services)
        {
            services.AddScoped<IHubSender<T>, HubSender<T, HubNotifier<T>>>();
            services.AddScoped<IHubNotifier<T>, HubNotifier<T>>();
            
            services.AddSignalR();

            return services;
        }

        public static IApplicationBuilder UseHubSignalR<T>(this IApplicationBuilder app, string hubName)
        {
            app.UseSignalR(routes => routes.MapHub<HubNotifier<T>>(hubName));

            return app;
        }
    }
}
