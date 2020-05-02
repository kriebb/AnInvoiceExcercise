using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Backend.API.Domain.CustomerManagement;
using Backend.API.Domain.InvoiceManagement;
using Backend.API.Dtos.CustomerManagement;
using Backend.API.Dtos.InvoiceManagement;

namespace Backend.API.CustomerManagement.MappingMangement
{
    public class InvoiceMapper:Profile
    {
        public InvoiceMapper()
        {
            CreateMap<Invoice, InvoiceDto>();
        }
    }
}
