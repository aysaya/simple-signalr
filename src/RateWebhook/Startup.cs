using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
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
            var connectionString = Configuration["simple-bus-connection"];
            var queueName = Configuration["simple-queue-name"]; 

            services.AddSingleton<IProvideServiceBusConnection>
                (
                    new ServiceBusConnectionProvider(connectionString, queueName)
                );

            var thirdPartyRatesStore = new MemoryPersistence();
            services.AddSingleton<IQueryRA>(thirdPartyRatesStore);
            services.AddSingleton<ICommandRA>(thirdPartyRatesStore);

            services.AddScoped<ISendMessage, MessageSender>();
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
