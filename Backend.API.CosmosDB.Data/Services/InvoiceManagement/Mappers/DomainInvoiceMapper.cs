using AutoMapper;

namespace Backend.API.CosmosDB.Data.Services.InvoiceManagement.Mappers
{
    internal class DataInvoiceMapper:Profile
    {
        public DataInvoiceMapper()
        {
            CreateMap<Domain.InvoiceManagement.Invoice, DataModels.InvoiceManagement.Invoice>()
                .ForMember(x => x.TimeToLive, x => x.Ignore())
                .ForMember(x => x.ResourceId, x => x.Ignore())
                .ForMember(x => x.SelfLink, x => x.Ignore())
                .ForMember(x => x.AltLink, x => x.Ignore())
                .ForMember(x => x.Timestamp, x => x.Ignore())
                .ForMember(x => x.ETag, x => x.Ignore());

            CreateMap<Domain.InvoiceManagement.InvoiceLine, DataModels.InvoiceManagement.InvoiceLine>();

        }
    }
}
