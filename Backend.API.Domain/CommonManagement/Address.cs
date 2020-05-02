using Value;

namespace Backend.API.Domain.CommonManagement
{
    public class Address:ValueObject
    {
        public string Street { get; set; }
        public string StreetNumber { get; set; }
        public string PostalCode { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
    }
}