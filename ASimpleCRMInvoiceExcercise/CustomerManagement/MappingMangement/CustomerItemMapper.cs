using AutoMapper;
using Backend.API.Domain.CustomerManagement;
using Backend.API.Dtos.CustomerManagement;
using Backend.API.Dtos.InvoiceManagement;

namespace Backend.API.CustomerManagement.MappingMangement
{
    public class CustomerItemMapper : Profile
    {
        public CustomerItemMapper()
        {
            CreateMap<Customer, CustomerItem>();
        }
    }
}