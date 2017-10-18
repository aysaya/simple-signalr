using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using QuoteEngine.ResourceAccessors;
using QuoteEngine.MessageHandlers;
using Infrastructure.ServiceBus;
using Contracts;

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

            services.AddServiceBus(connectionString, queueName, topicName, null);
            
            //TODO: implement durable persistence
            var quoteStore = new MemoryPersistence();
            services.AddSingleton<IQueryRA>(quoteStore);
            services.AddSingleton<ICommandRA>(quoteStore);
            
            services.AddScoped<IProcessMessage, ThirdPartyRateProcessor>();
            services.AddScoped<IRegisterMessageHandler<ThirdPartyRate>, RegisterThirdPartyRateHandler>();
            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, IServiceProvider serviceProvider)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            serviceProvider.GetService<IRegisterMessageHandler<ThirdPartyRate>>().Register();
            app.UseMvc();
        }
    }
}
