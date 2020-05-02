using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Backend.API.Domain.CustomerManagement;
using Backend.API.Domain.Services;
using Backend.API.Domain.Services.CustomerManagement;
using Backend.API.Dtos;
using Backend.API.Dtos.CustomerManagement;
using Backend.API.ErrorManagement;
using Backend.API.Infrastructure.Mappings;
using Microsoft.AspNetCore.Mvc;

namespace Backend.API.CustomerManagement
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IMapper<CustomerItem, Customer> _customerMapper;
        private readonly IMapper<ContactInfoItem, ContactInfo> _contactMapper;

        private readonly IErrorService _service;

        //TODO: add business service layer. To much responsibilities for a controller
        public CustomerController(ICustomerRepository customerRepository,
            IMapper<CustomerItem, Customer> customerMapper,
            IMapper<ContactInfoItem, ContactInfo> contactMapper,

            IErrorService service)
        {
            _customerRepository = customerRepository;
            _customerMapper = customerMapper;
            _contactMapper = contactMapper;
            _service = service;
        }


        // POST: api/Customer - Klanten aan te maken
        [HttpPost]
        public async Task<ActionResult<CustomerItem>> Post([FromBody] CustomerItem value)
        {
            try
            {
                if (value == null) //TODO: add validators
                    return new BadRequestResult();

                var newCustomer = _customerMapper.Map(value);
                await _customerRepository.AddAsync(newCustomer);

                return CreatedAtAction(nameof(CustomerItem), new { id = newCustomer.Id }, value);
            }
            catch (Exception e) //TODO middleware            
            {
                return _service.Catch(e);
            }
        }

        // POST: api/Customer - Een contactgegeven (email/telefoon) toe te wijzen aan een klant

        [HttpPut]
        public async Task<ActionResult> Put(long customerId, [FromBody] ContactInfoItem value)
        {
            try
            {
                if (value == null) //TODO: add validators
                    return new BadRequestResult();

                var contact = _contactMapper.Map(value);
                var customer = await _customerRepository.GetAsync(customerId);
                if (customer == null)
                    return new NotFoundResult();

                if (!customer.AddContact(contact))
                {
                    return BadRequest("Couldn't add the contactInfo to the customer");
                }

                await _customerRepository.UpdateAsync(customer);

                return new OkResult(); //TODO: give more meaning back
            }
            catch (Exception e) //TODO middleware
            {
                return _service.Catch(e);
            }
        }

    }


}
