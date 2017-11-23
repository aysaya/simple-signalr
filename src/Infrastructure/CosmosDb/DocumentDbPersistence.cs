using System.Collections.Generic;
using Microsoft.Azure.Documents.Linq;
using System.Threading.Tasks;
using Infrastructure.Common;

namespace Infrastructure.CosmosDb
{
    public class DocumentDbRepository<T> : IProvideRepository<T>
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

        public Task DeleteAllAsync()
        {
            throw new System.NotImplementedException();
        }
    }
}
