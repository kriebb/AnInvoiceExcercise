using System;
using System.Collections.Generic;
using Backend.API.Domain.CustomerManagement;
using Backend.API.Domain.Infrastructure.EntityMangement;

namespace Backend.API.Domain.InvoiceManagement
{
    public class Invoice:Entity
    {
        public string Summary { get; set; }
        public DateTime Date { get; set; }
        public Customer Customer { get; set; }
        public int TotalAmount { get; set; }

        private readonly HashSet<InvoiceLine> _lines = new HashSet<InvoiceLine>();
        public IEnumerable<InvoiceLine> Lines => _lines;

        public void AddInvoiceLine(InvoiceLine invoiceLine)
        {
            _lines.Add(invoiceLine);
        }

        public void RemoveInvoiceLines(InvoiceLine invoiceLine)
        {
            _lines.Remove(invoiceLine);
        }

        public void ClearInvoiceLines()
        {
            _lines.Clear();
        }
    }
}