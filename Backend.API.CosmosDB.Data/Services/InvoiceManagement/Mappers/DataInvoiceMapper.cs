using AutoMapper;

namespace Backend.API.CosmosDB.Data.Services.InvoiceManagement.Mappers
{
    internal class DomainInvoiceMapper : Profile
    {
        public DomainInvoiceMapper()
        {
            CreateMap<DataModels.InvoiceManagement.Invoice, Domain.InvoiceManagement.Invoice>();
            CreateMap<DataModels.InvoiceManagement.InvoiceLine, Domain.InvoiceManagement.InvoiceLine>();

        }
    }
}