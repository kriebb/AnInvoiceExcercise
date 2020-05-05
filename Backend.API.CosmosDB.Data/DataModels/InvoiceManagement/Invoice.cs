using System;
using System.Collections.Generic;
using Backend.API.CosmosDB.Data.DataModels.CustomerManagement;
using Backend.API.CosmosDB.Data.DataModels.InvoiceManagement;
using Microsoft.Azure.Documents;
using Newtonsoft.Json;

namespace Backend.API.CosmosDB.Data.DataModels.InvoiceManagement
{
    internal class Invoice:Document
    {        
        [JsonProperty]
        public string Summary { get; set; }
        [JsonProperty]
        public DateTime Date { get; set; }
        [JsonProperty]
        public Customer Customer { get; set; }
        [JsonProperty]
        public int TotalAmount { get; set; }
        [JsonProperty]
        public IEnumerable<InvoiceLine> Lines { get; set; }


    }
}