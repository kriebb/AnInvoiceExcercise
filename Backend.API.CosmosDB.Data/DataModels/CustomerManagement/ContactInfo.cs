using Newtonsoft.Json;

namespace Backend.API.CosmosDB.Data.DataModels.CustomerManagement
{
    internal class ContactInfo
    {
        [JsonProperty]
        public string Type { get; set; }
        [JsonProperty]
        public string Value { get; set; }
    }
}