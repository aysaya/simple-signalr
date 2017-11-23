using System;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace Infrastructure.Authentication
{
    public static class AzureAdServiceCollectionExtensions
    {
        public static AuthenticationBuilder AddAzureAdB2CBearer(this AuthenticationBuilder builder)
            => builder.AddAzureAdB2CBearer(_ => { });

        public static AuthenticationBuilder AddAzureAdB2CBearer(this AuthenticationBuilder builder, Action<AzureAdB2COptions> configureOptions)
        {
            builder.Services.Configure(configureOptions);
            builder.Services.AddSingleton<IConfigureOptions<JwtBearerOptions>, ConfigureAzureOptions>();
            builder.AddJwtBearer(o => o.RequireHttpsMetadata = false);
            return builder;
        }

        private class ConfigureAzureOptions : IConfigureNamedOptions<JwtBearerOptions>
        {
            private readonly AzureAdB2COptions azureOptions;

            public ConfigureAzureOptions(IOptions<AzureAdB2COptions> azureOptions)
            {
                this.azureOptions = azureOptions.Value;
            }

            public void Configure(string name, JwtBearerOptions options)
            {
                options.Audience = azureOptions.ClientId;
                options.Authority = $"{azureOptions.Instance}/{azureOptions.Domain}/{azureOptions.SignUpSignInPolicyId}/v2.0";
            }

            public void Configure(JwtBearerOptions options)
            {
                Configure(Options.DefaultName, options);
            }
        }

        public class AzureAdB2COptions
        {
            public string ClientId { get; set; }
            public string Instance { get; set; }
            public string Domain { get; set; }
            public string SignUpSignInPolicyId { get; set; }
        }
    }
}
