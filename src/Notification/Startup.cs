using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Notification.ResourceAccessors;
using Infrastructure.ServiceBus;
using Contracts;
using Notification.MessageHandlers;
using Notification.DomainModels;
using Infrastructure.Common;
using Infrastructure.Hubs;

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
            services.AddStorageDb(o=>o.UseSqliteStorage<RateFeed>("Data Source=Notification"));

            services.AddScoped(typeof(IQueryRA<RateFeed>), typeof(RateFeedPersistence));
            services.AddScoped(typeof(ICommandRA<RateFeed>), typeof(RateFeedPersistence));

            services.AddSubscriptionHandler<NewQuoteReceived, NewQuoteReceivedProcessor>
                (
                    Configuration["simple-bus-connection"],
                    Configuration["simple-topic-name"],
                    Configuration["simple-subscription-name"]
                );

            services.AddHubSignalR<RateFeed>();

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

            app.RegisterSubscriptionHandler<NewQuoteReceived>(serviceProvider);

            app.UseHubSignalR<RateFeed>("rate-feed-hub");
            
            app.UseMvc();
        }
    }
}
