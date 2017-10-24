using Microsoft.Azure.Documents.Client;
using System;

namespace Infrastructure.CosmosDb
{
    public interface IProvideCosmosDbConnection<T>
    {
        DocumentClient DocumentClient { get;  }
        Uri DocumentCollectionUri { get; }
    }

    public class CosmosConnectionProvider<T> : IProvideCosmosDbConnection<T>
    {
        private readonly DocumentClient client;
        private readonly Uri collectionUri;

        public CosmosConnectionProvider(string endpoint, string key,
            string databaseId, string collectionId
            )
        {
            client = new DocumentClient(new Uri(endpoint), key);
            collectionUri = UriFactory.CreateDocumentCollectionUri(databaseId, collectionId);
        }

        public DocumentClient DocumentClient => client;

        public Uri DocumentCollectionUri => collectionUri;
    }
}
