namespace Backend.API.CosmosDB.Data.Services.Infrastructure.Impl
{
    internal class CosmosDbConfig
    {
        public string EndPoint { get; set; }
        public string AuthKey { get; set; }
        public string CollectionId { get; set; }
        public string DatabaseId { get; set; }
    }
}