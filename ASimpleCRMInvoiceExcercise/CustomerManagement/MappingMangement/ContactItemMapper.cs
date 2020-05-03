using AutoMapper;
using Backend.API.Domain.CustomerManagement;
using Backend.API.Dtos.CustomerManagement;

namespace Backend.API.CustomerManagement.MappingMangement
{
    public class ContactItemMapper : Profile
    {
        public ContactItemMapper()
        {
            CreateMap<ContactInfo, ContactInfoItem>();
        }
    }
}