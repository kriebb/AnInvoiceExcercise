using System;
using System.Linq;
using System.Threading.Tasks;
using Backend.API.CosmosDB.Data.Services.Infrastructure.Impl;
using Microsoft.Azure.Documents.Client;
using Microsoft.Extensions.Configuration;

namespace Backend.API.CosmosDB.Data.Tests.IntegrationTests.Infrastructure
{
    public class CosmosDBFixture : IDisposable, Xunit.IAsyncLifetime
    {
        private readonly IConfigurationRoot _config;
        private DocumentClient _client;

        public CosmosDBFixture()
        {
            _config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json",optional:false)
                .AddEnvironmentVariables()
                .Build();

            CosmosDbConfig = new CosmosDbConfig();

            _config.GetSection("CosmosDbConfig").Bind(CosmosDbConfig);

            // Initializing collection with configuration
            _client = new DocumentClient(
                new Uri(CosmosDbConfig.EndPoint),
                CosmosDbConfig.AuthKey);
        }


        internal CosmosDbConfig CosmosDbConfig { get; private set; }



        public void Dispose()
        {
            // Cleanup 
        }

        public async Task InitializeAsync()
        {
        }

        public async Task DisposeAsync()
        {
            // Cleanup
            try
            {

                var db = _client.CreateDatabaseQuery().Where(d => d.Id ==  CosmosDbConfig.DatabaseId).AsEnumerable().FirstOrDefault();
                if (db == null)
                    throw new ArgumentException($"Couldn't find db {CosmosDbConfig.DatabaseId}");
                await this._client.DeleteDatabaseAsync(db.SelfLink);

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);

            }
        }
    }
}