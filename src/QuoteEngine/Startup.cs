using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using QuoteEngine.ResourceAccessors;
using BasicQueueSender.MessageHandlers;
using QuoteEngine.MessageHandlers;

namespace QuoteEngine
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
            var queueName = Configuration["simple-queue-name"];
            var topicName = Configuration["simple-topic-name"];

            services.AddSingleton<IProvideServiceBusConnection>
                (
                    new ServiceBusConnectionProvider(connectionString, queueName, topicName)
                );

            //TODO: implement durable persistence
            var quoteStore = new MemoryPersistence();
            services.AddSingleton<IQueryRA>(quoteStore);
            services.AddSingleton<ICommandRA>(quoteStore);

            services.AddScoped<IHandleMessage, MessageHandler>();
            services.AddScoped<IPublishMessage, MessagePublisher>();
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
