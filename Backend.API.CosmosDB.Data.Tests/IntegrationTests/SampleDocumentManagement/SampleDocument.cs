using System.Collections.Generic;
using Microsoft.Azure.Documents;
using Newtonsoft.Json;

namespace Backend.API.CosmosDB.Data.Tests.IntegrationTests.SampleDocumentManagement
{
    public class SampleDocument:Document
    {
        [JsonProperty]
        public string SampleString { get; set; }
        [JsonProperty]

        public IEnumerable<string> SampleCollection { get; set; } = new List<string>();
    }
}