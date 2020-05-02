using AutoMapper;
using Backend.API.Domain.CustomerManagement;
using Backend.API.Dtos.CustomerManagement;

namespace Backend.API.CustomerManagement.MappingManagement
{
    public class CustomerItemMapper : Profile
    {
        public CustomerItemMapper()
        {
            CreateMap<Customer, CustomerItem>();
        }
    }
}