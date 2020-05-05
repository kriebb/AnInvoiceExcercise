using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.Azure.Documents;
using Microsoft.Azure.Documents.Client;
using Newtonsoft.Json;

namespace Backend.API.CosmosDB.Data.Services.Infrastructure.Impl
{
    //https://github.com/Azure/azure-cosmos-dotnet-v2/blob/master/samples/code-samples/DocumentManagement/Program.cs#L154
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

        public T GetById(string id)
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

            ResourceResponse<Document> doc = await Client.CreateDocumentAsync(
                Collection.SelfLink,
                entity);
            return (T)(dynamic)doc.Resource;
        }

        public async Task<T> UpdateAsync(string id, T entity)
        {
            //https://azure.microsoft.com/en-us/blog/documentdb-adds-upsert/
            //https://stackoverflow.com/questions/27118998/converting-created-document-result-to-poco
            var response = await Client.UpsertDocumentAsync(Collection.SelfLink, entity);
            return (T)(dynamic)response.Resource;
        }

        public async Task DeleteAsync(string id)
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

        public String CollectionId => typeof(T).Name;

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
                    ConnectionPolicy connectionPolicy = new ConnectionPolicy { UserAgentSuffix = "crmclientinvoiceapp_1" };

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

        public static async Task<T> ReadAsAsync<T>(Document d)
        {
            using (var ms = new MemoryStream())
            using (var reader = new StreamReader(ms))
            {
                d.SaveTo(ms);
                ms.Position = 0;
                var stream = await reader.ReadToEndAsync();
                return JsonConvert.DeserializeObject<T>(stream);
            }
        }
    }
}