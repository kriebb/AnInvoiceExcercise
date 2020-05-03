using Backend.API.Domain.CustomerManagement;
using Backend.API.Dtos.CustomerManagement;
using Backend.API.Infrastructure.Mappings;
using Backend.API.Infrastructure.Mappings.BootstrapAutoMapper;
using Backend.API.Tests.Backend.API.InvoiceManagement;
using FluentAssertions;
using FluentAssertions.Execution;
using Xunit;

namespace Backend.API.Tests.Backend.API.CustomerManagement
{
    public class GivenContactInfoItemMapper
    {
        private IMapper<ContactInfoItem, ContactInfo> _sut;

        public GivenContactInfoItemMapper()
        {
            _sut = new AutoMapperFactory().CreateMapper<ContactInfoItem, ContactInfo>();
        }


        [Fact]
        public void WhenMapFromNull_ShouldReturnNull()
        {
            var destination = _sut.Map(null as ContactInfoItem);
            Assert.Null(destination);
        }

        [Fact]
        public void WhenMap_Dto_To_Entity_ShouldAllPropertiesMapped()
        {
            var faker = Generator.ContactInfoItemGenerator();

            var item = faker.Generate();


            var entity = _sut.Map(item);

            using (new AssertionScope())
            {
                item.Type.Should().Be(entity.Type);
                item.Value.Should().Be(entity.Value);
            }
        }
    }
}