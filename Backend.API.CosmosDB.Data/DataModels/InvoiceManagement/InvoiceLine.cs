using Newtonsoft.Json;

namespace Backend.API.CosmosDB.Data.DataModels.InvoiceManagement
{
    internal class InvoiceLine
    {
        [JsonProperty]
        public string Id { get; set; }
        [JsonProperty]
        public int Quantity { get; set; }
        [JsonProperty]
        public decimal Price { get; set; }
        [JsonProperty]
        public decimal TotalPrice { get; set; }
    }
}