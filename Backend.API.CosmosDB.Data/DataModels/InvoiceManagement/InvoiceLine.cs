namespace Backend.API.CosmosDB.Data.DataModels.InvoiceManagement
{
    internal class InvoiceLine
    {
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public decimal TotalPrice { get; set; }
    }
}