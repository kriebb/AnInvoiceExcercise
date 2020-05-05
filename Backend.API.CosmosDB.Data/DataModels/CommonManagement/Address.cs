
using Newtonsoft.Json;

namespace Backend.API.CosmosDB.Data.DataModels.CommonManagement
{
    internal class Address
    {
        [JsonProperty]
        public string Street { get; set; }
        [JsonProperty]
        public string StreetNumber { get; set; }
        [JsonProperty]
        public string PostalCode { get; set; }
        [JsonProperty]
        public string City { get; set; }
        [JsonProperty]
        public string Country { get; set; }
    }
}