using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using QuoteEngine.ResourceAccessors;
using QuoteEngine.MessageHandlers;
using Infrastructure.ServiceBus;
using Infrastructure.CosmosDb;
using QuoteEngine.DomainModels;

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
            services.AddDbCollection<Quote>
                (
                    Configuration["simple-cosmos-endpoint"],
                    Configuration["simple-cosmos-connection"],
                    Configuration["quote-engine-database-id"],
                    Configuration["quotes-collection-id"]
                );
            services.AddScoped(typeof(IQueryRA<Quote>), typeof(QuotePersistence));
            services.AddScoped(typeof(ICommandRA<Quote>), typeof(QuotePersistence));

            var connectionString = Configuration["simple-bus-connection"];
            var queueName = Configuration["simple-queue-name"];
            var topicName = Configuration["simple-topic-name"];

            services.AddTopicSender<Contracts.NewQuoteReceived>(connectionString, topicName);
            services.AddQueueHandler<Contracts.CreateQuote, ThirdPartyRateProcessor> (connectionString, queueName);           
            
            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, IServiceProvider serviceProvider)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.RegisterHandler<Contracts.CreateQuote>(serviceProvider);
            app.UseMvc();
        }
    }
}
