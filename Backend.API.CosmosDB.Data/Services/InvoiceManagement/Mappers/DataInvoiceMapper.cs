using AutoMapper;

namespace Backend.API.CosmosDB.Data.Services.InvoiceManagement
{
    internal class DataInvoiceMapper:Profile
    {
        public DataInvoiceMapper()
        {
            CreateMap<DataModels.InvoiceManagement.Invoice, Domain.InvoiceManagement.Invoice>();
        }
    }
}