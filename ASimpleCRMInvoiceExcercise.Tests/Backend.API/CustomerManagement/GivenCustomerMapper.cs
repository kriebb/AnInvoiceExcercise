using Backend.API.CustomerManagement.MappingMangement;
using Backend.API.Domain.CustomerManagement;
using Backend.API.Dtos.CustomerManagement;
using Backend.API.Infrastructure.Mappings;
using Backend.API.Infrastructure.Mappings.BootstrapAutoMapper;
using Bogus;
using FluentAssertions;
using FluentAssertions.Execution;
using Xunit;

namespace Backend.API.Tests.Backend.API.CustomerManagement
{
    public class GivenCustomerMapper
    {
        private IMapper<CustomerItem, Customer> _sut;

        public GivenCustomerMapper()
        {
            _sut = new AutoMapperFactory().CreateMapper<CustomerItem, Customer>();
        }


        [Fact]
        public void WhenMapFromNullAsCustomerDto_ShouldReturnNull()
        {
            var destination = _sut.Map(null as CustomerItem);
            Assert.Null(destination);
        }


        [Fact]
        public void WhenMap_Customer_CustomerDto_ShouldAllPropertiesMapped()
        {
            var faker = new Faker();

            var item = new CustomerItem();
            item.FirstName = faker.Person.FirstName;
            item.LastName = faker.Person.LastName;
            item.Id = faker.Random.Number(1,int.MaxValue);

            var entity = _sut.Map(item);

            using (new AssertionScope())
            {
               item.Id.Should().Be(entity.Id);
               item.FirstName.Should().Be(entity.FirstName);
               item.LastName.Should().Be(entity.LastName);
            }
        }
    }
}