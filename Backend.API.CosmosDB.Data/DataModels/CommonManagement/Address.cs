
namespace Backend.API.CosmosDB.Data.DataModels.CommonManagement
{
    internal class Address
    {
        public string Street { get; set; }
        public string StreetNumber { get; set; }
        public string PostalCode { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
    }
}