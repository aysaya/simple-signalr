using Contracts;
using Infrastructure.Common;
using Infrastructure.ServiceBus;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Pricing.DomainModel;
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
            services.AddStorageDb(o => o.UseSqliteStorage<Quote>("Data Source=Pricing"));

            services.AddScoped(typeof(IQueryRA<Quote>), typeof(QuotePersistence));
            services.AddScoped(typeof(ICommandRA<Quote>), typeof(QuotePersistence));
           
            services.AddSubscriptionHandler<NewQuoteReceived, NewQuoteReceivedProcessor>
                (
                    Configuration["simple-bus-connection"],
                    Configuration["simple-topic-name"],
                    Configuration["simple-subscription-name"]
                );

            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, IServiceProvider serviceProvider)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.RegisterSubscriptionHandler<NewQuoteReceived>(serviceProvider);

            app.UseMvc();
        }
    }
}
