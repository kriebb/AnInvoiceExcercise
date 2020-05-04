using AutoMapper;

namespace Backend.API.CosmosDB.Data.Services.InvoiceManagement
{
    internal class DomainInvoiceMapper:Profile
    {
        public DomainInvoiceMapper()
        {
            CreateMap<Domain.InvoiceManagement.Invoice, DataModels.InvoiceManagement.Invoice>();
            CreateMap<Domain.InvoiceManagement.InvoiceLine, DataModels.InvoiceManagement.InvoiceLine>();

        }
    }
}
