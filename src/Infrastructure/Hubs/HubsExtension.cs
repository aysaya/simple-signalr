using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Infrastructure.Hubs
{
    public static class HubsExtension
    {
        public static IServiceCollection AddHubSignalR<T>(this IServiceCollection services)
        {
            services.AddScoped<IProvideHubContext<T>, HubContextProvider<T>>();
            services.AddScoped<IHubNotifier<T>, HubNotifier<T>>();
            
            services.AddSignalR();

            return services;
        }

        public static IApplicationBuilder UseHubSignalR<T>(this IApplicationBuilder app, string hubName, IServiceProvider serviceProvider)
        {
            app.UseSignalR(routes => routes.MapHub<HubSender<T>>(hubName));

            serviceProvider.GetService<IProvideHubContext<T>>().HubContext = serviceProvider.GetService<IHubContext<HubSender<T>>>();

            return app;
        }
    }
}
