using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Notification.ResourceAccessors;
using Notification.Hubs;
using Microsoft.AspNetCore.SignalR;
using Infrastructure.ServiceBus;
using Contracts;
using Notification.MessageHandlers;

namespace Notification
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            var connectionString = Configuration["simple-bus-connection"];
            var subscriptionName = Configuration["simple-subscription-name"];
            var topicName = Configuration["simple-topic-name"];

            services.AddServiceBus(connectionString, null, topicName, subscriptionName);

            //TODO: implement durable persistence
            var quoteStore = new MemoryPersistence();
            services.AddSingleton<IQueryRA>(quoteStore);
            services.AddSingleton<ICommandRA>(quoteStore);
            services.AddSingleton<IProvideRateFeedClientContext, RateFeedClients>();
            services.AddSingleton<INotifyRateFeedClient, RateFeedClientNotifier>();
            services.AddScoped<IProcessMessage, NewQuoteReceivedProcessor>();

            services.AddScoped<IRegisterMessageHandler<NewQuoteReceived>, RegisterNewQuoteReceivedHandler>();

            services.AddSignalR();
            services.AddCors(p => p.AddPolicy("AllowAllOrigins",
                builder =>
                {
                    builder.AllowAnyOrigin();
                    builder.AllowAnyHeader();
                    builder.AllowAnyMethod();
                }
            ));

            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, IServiceProvider serviceProvider)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCors("AllowAllOrigins");
            app.UseSignalR(routes => routes.MapHub<RateFeedHub>("rate-feed-hub"));

            serviceProvider.GetService<IRegisterMessageHandler<NewQuoteReceived>>().Register();

            serviceProvider.GetService<IProvideRateFeedClientContext>().RateFeedClients = serviceProvider.GetService<IHubContext<RateFeedHub>>();

            app.UseMvc();
        }
    }
}
