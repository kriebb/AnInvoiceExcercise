using System;
using System.Collections.Generic;
using Backend.API.CosmosDB.Data.DataModels.CustomerManagement;
using Microsoft.Azure.Documents;

namespace Backend.API.CosmosDB.Data.DataModels.InvoiceManagement
{
    internal class Invoice:Document
    {
        public string Summary { get; set; }
        public DateTime Date { get; set; }
        public Customer Customer { get; set; }
        public int TotalAmount { get; set; }

        public IEnumerable<InvoiceLine> Lines { get; set; }


    }
}