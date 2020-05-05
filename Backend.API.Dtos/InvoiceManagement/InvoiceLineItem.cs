using System;

namespace Backend.API.Dtos.InvoiceManagement
{
    public class InvoiceLineItem
    {
        public Guid Id { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public decimal TotalPrice { get; set; }
    }
}