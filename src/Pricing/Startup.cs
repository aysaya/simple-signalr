using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Pricing.MessageHandlers;
using Pricing.ResourceAccessors;
using System;

namespace Pricing
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

            services.AddSingleton<IProvideServiceBusConnection>
                (
                    new ServiceBusConnectionProvider(connectionString, topicName, subscriptionName)
                );

            //TODO: implement durable persistence
            var quoteStore = new MemoryPersistence();
            services.AddSingleton<IQueryRA>(quoteStore);
            services.AddSingleton<ICommandRA>(quoteStore);

            services.AddScoped<IHandleMessage, MessageHandler>();
            services.AddScoped<IProcessMessage, MessageProcessor>();
            services.AddScoped<IRegisterMessageHandler, RegisterMessageHandler>();
            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, IServiceProvider serviceProvider)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            serviceProvider.GetService<IRegisterMessageHandler>().Register();

            app.UseMvc();
        }
    }
}
