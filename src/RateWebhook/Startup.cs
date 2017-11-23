using Infrastructure.ServiceBus;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RateWebhook.DomainModels;
using RateWebhook.ResourceAccessors;
using Infrastructure.Common;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Infrastructure.Authentication;
using Infrastructure.Authorization;

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
            ////to use cosmos db
            //services.AddStorageDb(o=>o.UseCosmosDbStorage<ThirdPartyRate>
            //    (Configuration["simple-cosmos-endpoint"],
            //     Configuration["simple-cosmos-connection"],
            //     Configuration["rate-webhook-database-id"],
            //     Configuration["thirdpartyrates-collection-id"]
            //    ));

            services.AddStorageDb(o => o.UseSqliteStorage<ThirdPartyRate>("Data Source=Webhook"));

            services.AddScoped(typeof(IQueryRA<ThirdPartyRate>), typeof(ThirdPartyPersistence));
            services.AddScoped(typeof(ICommandRA<ThirdPartyRate>), typeof(ThirdPartyPersistence));
            
            services.AddQueueSender<Contracts.CreateQuote>
                (
                    Configuration["simple-bus-connection"], 
                    Configuration["simple-queue-name"]
                );

            services.AddAuthentication(sharedOptions =>
            {
                sharedOptions.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddAzureAdB2CBearer(options => Configuration.Bind("AzureAdB2C", options));

            services.AddAuth();


            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseAuthentication();
            app.UseMvc();
        }
    }
}
