using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Authorization
{
    public static class AzureAdAuthorizationExtension
    {
        public static IServiceCollection AddAuth(this IServiceCollection services)
        {
            services.AddAuthorization(o => o.AddPolicy("Admin", p => p.RequireClaim("extension_Group", "Admin")));
            services.AddAuthorization(o => o.AddPolicy("User", p => p.RequireClaim("extension_Group", new[] { "User", "Admin" })));

            return services;
        }
    }
}
