using AutoMapper;
using Backend.API.Domain.InvoiceManagement;
using Backend.API.Dtos.InvoiceManagement;

namespace Backend.API.InvoiceManagement.MappingMangement
{
    public class InvoiceMapper : Profile
    {
        public InvoiceMapper()
        {
            CreateMap<Invoice, InvoiceItem>();
        }


    }
}
