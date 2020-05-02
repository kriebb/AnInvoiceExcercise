using AutoMapper;
using Backend.API.Domain.CommonManagement;
using Backend.API.Dtos.CustomerManagement;

namespace Backend.API.CustomerManagement.MappingManagement
{
    public class AddressMapper:Profile
    {
        public AddressMapper()
        {
            CreateMap<Address, AddressItem>();
        }
    }
}