using Infrastructure.CosmosDb;
using Infrastructure.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Infrastructure.Common
{
    public static class StorageDbExtension
    {
        public static IServiceCollection AddStorageDb(this IServiceCollection services, Action<StorageDbOptionsBuilder> options)
        {
            options.Invoke(new StorageDbOptionsBuilder(services));
            return services;
        }
    }
    public static class StorageDbOptionsExtension
    {
        public static IStorageDbOptionsBuilder UseSqliteStorage<T>(this StorageDbOptionsBuilder options, string conn) where T: class
        {
            options.Services.AddDbContext<SqliteDbContext<T>>(o => o.UseSqlite(conn));
            options.Services.AddSingleton<ISqliteDbContext<T>, SqliteDbContext<T>>();
            options.Services.AddSingleton<IProvideRepository<T>, SqliteRepository<T>>();
            return options;
        }
    
        public static IStorageDbOptionsBuilder UseCosmosDbStorage<T>(this StorageDbOptionsBuilder options,
            string endpoint, string connKey,
            string databaseId, string collectionId) 
        {
            options.Services.AddSingleton(typeof(IProvideCosmosDbConnection<T>),
                p => new CosmosConnectionProvider<T>
                (
                    endpoint, connKey, databaseId, collectionId
                ));
            options.Services.AddScoped(typeof(IProvideRepository<T>), typeof(DocumentDbRepository<T>));
            return options;
        }
    }
}
