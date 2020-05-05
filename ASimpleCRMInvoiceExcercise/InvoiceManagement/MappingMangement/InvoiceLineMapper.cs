using AutoMapper;
using Backend.API.Domain.InvoiceManagement;
using Backend.API.Dtos.InvoiceManagement;

namespace Backend.API.InvoiceManagement.MappingMangement
{
    public class InvoiceLineMapper : Profile
    {
        public InvoiceLineMapper()
        {
            CreateMap<InvoiceLine, InvoiceLineItem>();
        }


    }
}