using System.Threading.Tasks;
using AutoFixture;
using Backend.API.CustomerManagement;
using Backend.API.Domain.CustomerManagement;
using Backend.API.Domain.Services.CustomerManagement;
using Backend.API.Dtos.CustomerManagement;
using Backend.API.Infrastructure.Mappings;
using Microsoft.AspNetCore.Mvc;
using Xunit;

namespace Backend.API.Tests.Backend.API.CustomerManagement
{
    /*
     *  - Klanten aan te maken
        - Een contactgegeven (email/telefoon) toe te wijzen aan een klant
     */
    public class GivenCustomerController
    {
        private CustomerController _sut;
        private Fixture _fixture;

        public GivenCustomerController()
        {
            _fixture = new Fixture();
            _sut = _fixture.Create<CustomerController>();

            _fixture.Freeze<ICustomerRepository>();
            _fixture.Freeze<IMapper<Customer, CustomerItem>>();
            _fixture.Freeze<IMapper<ContactInfo, ContactInfoItem>>();
            
        }
        [Fact]
        public async Task WhenAddingNullAsCustomer_ShouldReturnBadRequest()
        {
            var result = await _sut.Post(null as CustomerItem);
            Assert.IsType<BadRequestResult>(result);


        }
        [Fact]
        public async Task WhenAddingAValidCustomer_ShouldReturnOK()
        {
            var result = await _sut.Post(new CustomerItem());
            Assert.IsType<OkResult>(result);

        }

        [Fact]
        public async Task WhenAddingCOntactInfoToNonExistingCustomer_ShouldReturnNotFound()
        {
            var result = await _sut.Put(0,new ContactInfoItem());
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task WhenAddingNullAsContactDataToACustomer_ShouldReturnBadRequest()
        {
            var result = await _sut.Put(0, null as ContactInfoItem);
            Assert.IsType<BadRequestResult>(result);
        }

        [Fact]
        public async Task WhenAddingValidContactDataToACustomer_ItShouldReturnOIk()
        {
            var result = await _sut.Put(1, new ContactInfoItem());
            Assert.IsType<OkResult>(result);
        }

        [Fact]
        public async Task WhenAddingDuplicateContactDataToACustomer_ItShouldReturnBadRequest()
        {
            var result1 = await _sut.Put(1, new ContactInfoItem());
            Assert.IsType<OkResult>(result1);
            var result2 = await _sut.Put(1, new ContactInfoItem());
            Assert.IsType<BadRequestResult>(result2);
        }


    }
}
