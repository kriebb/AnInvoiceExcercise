using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Backend.API.Domain.CustomerManagement;
using Backend.API.Dtos.CustomerManagement;

namespace Backend.API.CustomerManagement.MappingMangement
{
    public class CustomerMapper:Profile
    {
        public CustomerMapper()
        {
            CreateMap<Customer, CustomerDto>();
        }
    }
}
