using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.CosmosDb
{
    public static class CosmosDbExtension
    {
        public static IServiceCollection AddDbCollection<T>
            (
                this IServiceCollection services,
                string endpoint, string connKey,
                string databaseId, string collectionId
            )
        {
            services.AddSingleton(typeof(IProvideCosmosDbConnection<T>), 
                p => new CosmosConnectionProvider<T>
                (
                    endpoint, connKey, databaseId, collectionId
                ));
            services.AddScoped(typeof(IProvideDocumentRepository<T>), typeof(DocumentDbRepository<T>));
            
            return services;
        }
    }
}
