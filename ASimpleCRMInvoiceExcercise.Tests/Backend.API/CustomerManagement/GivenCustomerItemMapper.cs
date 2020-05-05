using Backend.API.Data.Generator;
using Backend.API.Domain.CustomerManagement;
using Backend.API.Dtos.CustomerManagement;
using Backend.API.Dtos.InvoiceManagement;
using Backend.API.Infrastructure.Mappings;
using Backend.API.Infrastructure.Mappings.BootstrapAutoMapper;
using Backend.API.Tests.Backend.API.InvoiceManagement;
using Bogus;
using FluentAssertions;
using FluentAssertions.Execution;
using Xunit;

namespace Backend.API.Tests.Backend.API.CustomerManagement
{
    public class GivenCustomerItemMapper
    {
        private IMapper<Customer, CustomerItem> _sut;

        public GivenCustomerItemMapper()
        {
            _sut = new AutoMapperFactory().CreateMapper<Customer, CustomerItem>();
        }


        [Fact]
        public void WhenMapFromNullAsCustomer_ShouldReturnNull()
        {
            var destination = _sut.Map(null as Customer);
            Assert.Null(destination);
        }

        [Fact]
        public void WhenMap_CustomerDto_Customer_ShouldAllPropertiesMapped()
        {
            var faker = new Faker();


            var entity = DomainGenerator.CustomerGenerator().Generate();

            var item = _sut.Map(entity);

            using (new AssertionScope())
            {
                entity.Id.Should().Be(item.Id);
                entity.FirstName.Should().Be(item.FirstName);
                entity.LastName.Should().Be(item.LastName);
            }
        }
    }
}