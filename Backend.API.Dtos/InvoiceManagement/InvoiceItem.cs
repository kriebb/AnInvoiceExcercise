using System;
using System.Collections.Generic;
using System.Text;

namespace Backend.API.Dtos.InvoiceManagement
{
    public class InvoiceItem
    {
        public string Summary { get; set; }
        public DateTime Date { get; set; }
        public Guid CustomerId { get; set; }
        public int TotalAmount { get; set; }
        public Guid Id { get; set; }

        public InvoiceLineItem[] Lines { get; set; }
    }
}
