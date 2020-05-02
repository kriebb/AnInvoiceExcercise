using Value;

namespace Backend.API.Domain.CustomerManagement
{
    public class ContactInfo : ValueObject
    {
        public string Type { get; set; }
        public string Value { get; set; }
        public static string Email { get; set; } = "Email";
        public static string CellNumber { get; set; } = "CellNumber";
    }
}