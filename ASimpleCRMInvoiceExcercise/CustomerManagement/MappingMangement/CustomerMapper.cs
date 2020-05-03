using AutoMapper;
using Backend.API.Domain.CustomerManagement;
using Backend.API.Dtos.CustomerManagement;

namespace Backend.API.CustomerManagement.MappingMangement
{
    public class CustomerMapper : Profile
    {
        public CustomerMapper()
        {
            CreateMap<CustomerItem, Customer>()
                .ForMember(x => x.Address, x => x.Ignore())
                .ForMember(x => x.Contacts, x => x.Ignore());
        }
    }
}
