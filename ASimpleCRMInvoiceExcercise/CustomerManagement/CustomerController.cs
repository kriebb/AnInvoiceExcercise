using System.Collections.Generic;
using Backend.API.Domain.CustomerManagement;
using Backend.API.Domain.Services;
using Backend.API.Domain.Services.CustomerManagement;
using Backend.API.Dtos;
using Backend.API.Dtos.CustomerManagement;
using Backend.API.Infrastructure.Mappings;
using Microsoft.AspNetCore.Mvc;

namespace Backend.API.CustomerManagement
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IMapper<Customer, CustomerDto> _customerDtoMapper;
        private readonly IMapper<ContactInfo, ContactInfoDto> _contactDtoMapper;

        public CustomerController(ICustomerRepository customerRepository, 
            IMapper<Customer, CustomerDto> customerDtoMapper,
            IMapper<ContactInfo, ContactInfoDto> contactDtoMapper)
        {
            _customerRepository = customerRepository;
            _customerDtoMapper = customerDtoMapper;
            _contactDtoMapper = contactDtoMapper;
        }


        // POST: api/Customer - Klanten aan te maken
        [HttpPost]
        public void Post([FromBody] CustomerDto value)
        {
            var newCustomer = _customerDtoMapper.Map(value);
            _customerRepository.Add(newCustomer);
        }

        // POST: api/Customer - Een contactgegeven (email/telefoon) toe te wijzen aan een klant

        [HttpPut]
        public void Put(long customerId,[FromBody] ContactInfoDto value)
        {
            var contact = _contactDtoMapper.Map(value);
            var customer = _customerRepository.Get(customerId);
            customer.AddContact(contact);
        }

    }


}
