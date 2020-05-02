using Backend.API.Domain.Infrastructure.EntityMangement;

namespace Backend.API.Domain.InvoiceManagement
{
    public class InvoiceLine:Entity
    {
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public decimal TotalPrice { get; set; }
    }
}