using Infrastructure.CosmosDb;
using Infrastructure.ServiceBus;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RateWebhook.DomainModels;
using RateWebhook.ResourceAccessors;

namespace RateWebhook
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
            services.AddDbCollection<ThirdPartyRate>
                (
                    Configuration["simple-cosmos-endpoint"],
                    Configuration["simple-cosmos-connection"],
                    Configuration["rate-webhook-database-id"],
                    Configuration["thirdpartyrates-collection-id"]
                );
            services.AddScoped(typeof(IQueryRA<ThirdPartyRate>), typeof(ThirdPartyPersistence));
            services.AddScoped(typeof(ICommandRA<ThirdPartyRate>), typeof(ThirdPartyPersistence));

            services.AddQueueSender<Contracts.CreateQuote>
                (
                    Configuration["simple-bus-connection"], 
                    Configuration["simple-queue-name"]
                );

            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc();
        }
    }
}
