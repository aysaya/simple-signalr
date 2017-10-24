using System.Collections.Generic;
using Microsoft.Azure.Documents.Linq;
using System.Threading.Tasks;

namespace Infrastructure.CosmosDb
{
    public interface IProvideDocumentRepository<T>
    {
        Task<T> SaveAsync(T t);

        Task<T[]> GetAllAsync();
    }

    public class DocumentDbRepository<T> : IProvideDocumentRepository<T>
    {
        private readonly IProvideCosmosDbConnection<T> cosmosDbConnection;
        public DocumentDbRepository(IProvideCosmosDbConnection<T> cosmosDbConnection)
        {
            this.cosmosDbConnection = cosmosDbConnection;
        }

        public async Task<T> SaveAsync(T t)
        {
            var result = (dynamic)(await cosmosDbConnection.DocumentClient.CreateDocumentAsync
                (
                    cosmosDbConnection.DocumentCollectionUri,
                    t
                )).Resource;

            System.Console.WriteLine($"Request {result.Id} saved successfully!");
            return result;
        }

        public async Task<T[]> GetAllAsync()
        {
            var query = cosmosDbConnection.DocumentClient
                .CreateDocumentQuery<T>
                (cosmosDbConnection.DocumentCollectionUri)
                .AsDocumentQuery();

            var results = new List<T>();
            while (query.HasMoreResults)
            {
                results.AddRange(await query.ExecuteNextAsync<T>());
            }

            return results.ToArray();
        }
    }
}
