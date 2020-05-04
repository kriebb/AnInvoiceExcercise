using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.Azure.Documents;
using Microsoft.Azure.Documents.Client;

namespace Backend.API.CosmosDB.Data.Services.Infrastructure.Impl
{
    internal class DocumentDbRepository<T> : IDocumentDbRepository<T> where T:Document
    {
        private readonly CosmosDbConfig _cosmosDbConfig;

        public DocumentDbRepository(CosmosDbConfig cosmosDbConfig)
        {
            _cosmosDbConfig = cosmosDbConfig;
        }
        public T Get(Expression<Func<T, bool>> predicate)
        {
            return Client.CreateDocumentQuery<T>(Collection.DocumentsLink)
                .Where(predicate)
                .AsEnumerable()
                .FirstOrDefault();
        }

        public T GetById(long id)
        {
            T doc = Client.CreateDocumentQuery<T>(Collection.SelfLink)
                .Where(d => d.Id == id.ToString())
                .AsEnumerable()
                .FirstOrDefault();

            return doc;
        }

        public IEnumerable<T> Find(Expression<Func<T, bool>> predicate)
        {
            var ret = Client.CreateDocumentQuery<T>(Collection.SelfLink)
                .Where(predicate)
                .AsEnumerable();

            return ret;
        }

        public async Task<T> CreateAsync(T entity)
        {
            Document doc = await Client.CreateDocumentAsync(Collection.SelfLink, entity);
            T ret = (T)(dynamic)doc;
            return ret;
        }

        public async Task<T> UpdateAsync(string id, T entity)
        {
            if (!long.TryParse(id, out var longId))
            {
                throw new ArgumentException(nameof(id),$"Couldn't parse Id to Long from entity {typeof(T).FullName}. Value was {id}");
            }
            T doc = GetById(longId);
            return (T)await Client.ReplaceDocumentAsync(doc.SelfLink, entity);
        }

        public async Task DeleteAsync(long id)
        {
            T doc = GetById(id);
            await Client.DeleteDocumentAsync(doc.SelfLink);
        }

        private string _databaseId;
        public String DatabaseId
        {
            get
            {
                if (string.IsNullOrEmpty(_databaseId))
                {

                    _databaseId = _cosmosDbConfig.DatabaseId;
                }

                return _databaseId;
            }
        }

        private string _collectionId;
        public String CollectionId
        {
            get
            {
                if (string.IsNullOrEmpty(_collectionId))
                {

                    _collectionId = _cosmosDbConfig.CollectionId;
                }

                return _collectionId;
            }
        }

        private Database _database;
        private Database Database
        {
            get
            {
                if (_database == null)
                {

                    _database = GetOrCreateDatabase(DatabaseId);
                }

                return _database;
            }
        }

        private DocumentCollection _collection;
        private DocumentCollection Collection
        {
            get
            {
                if (_collection == null)
                {
                    _collection = GetOrCreateCollection(Database.SelfLink, CollectionId);
                }

                return _collection;
            }
        }

        private DocumentClient _client;
        private DocumentClient Client
        {
            get
            {
                if (_client == null)
                {
                    string endpoint = _cosmosDbConfig.EndPoint;
                    string authKey = _cosmosDbConfig.AuthKey;

                    //the UserAgentSuffix on the ConnectionPolicy is being used to enable internal tracking metrics
                    //this is not requirted when connecting to DocumentDB but could be useful if you, like us, want to run 
                    //some monitoring tools to track usage by application
                    ConnectionPolicy connectionPolicy = new ConnectionPolicy { UserAgentSuffix = "crmclientinvoiceapp/1" };

                    _client = new DocumentClient(new Uri(endpoint), authKey, connectionPolicy);
                }

                return _client;
            }
        }

        public DocumentCollection GetOrCreateCollection(string databaseLink, string collectionId)
        {
            var col = Client.CreateDocumentCollectionQuery(databaseLink)
                .Where(c => c.Id == collectionId)
                .AsEnumerable()
                .FirstOrDefault();

            if (col == null)
            {
                col = _client.CreateDocumentCollectionAsync(databaseLink,
                    new DocumentCollection { Id = collectionId },
                    new RequestOptions { OfferType = "S1" }).Result;
            }

            return col;
        }
        public Database GetOrCreateDatabase(string databaseId)
        {
            var db = Client.CreateDatabaseQuery()
                .Where(d => d.Id == databaseId)
                .AsEnumerable()
                .FirstOrDefault();

            if (db == null)
            {
                db = _client.CreateDatabaseAsync(new Database { Id = databaseId }).Result;
            }

            return db;
        }
    }
}