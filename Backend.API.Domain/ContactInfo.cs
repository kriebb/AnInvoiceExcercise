using Value;

namespace ASimpleCRMInvoiceExcercise.Tests
{
    public class ContactInfo : ValueObject
    {
        public string Type { get; set; }
        public string Value { get; set; }
    }
}