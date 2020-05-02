using System;
using System.Collections.Generic;
using System.Text;

namespace Backend.API.Dtos.InvoiceManagement
{
    public class InvoiceItem
    {
        public string Summary { get; set; }
        public DateTime Date { get; set; }
        public long CustomerId { get; set; }
        public int TotalAmount { get; set; }
        public long Id { get; set; }
    }

}
