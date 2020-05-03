﻿using AutoMapper;
using Backend.API.Domain.CustomerManagement;
using Backend.API.Domain.InvoiceManagement;
using Backend.API.Dtos.InvoiceManagement;

namespace Backend.API.InvoiceManagement.MappingMangement
{
    public class InvoiceItemMapper : Profile
    {
        public InvoiceItemMapper()
        {
            CreateMap<InvoiceItem, Invoice>()
                .ForMember(x => x.Lines, z => z.Ignore())
                .ForMember(x => x.Customer, z => z.MapFrom((item, invoice) => invoice.Customer = new Customer(){Id = item.CustomerId}
                ));
        }
    }
}