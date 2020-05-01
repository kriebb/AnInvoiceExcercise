using EntityManagement;

namespace ASimpleCRMInvoiceExcercise.Tests
{
    public class InvoiceLine:Entity
    {
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public decimal TotalPrice { get; set; }
    }
}