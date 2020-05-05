using Backend.API.Data.Generator;
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
    public class GivenContactInfoMapper
    {
        private IMapper<ContactInfo, ContactInfoItem> _sut;

        public GivenContactInfoMapper()
        {
            _sut = new AutoMapperFactory().CreateMapper<ContactInfo, ContactInfoItem>();
        }


        [Fact]
        public void WhenMapFromNull_ShouldReturnNull()
        {
            var destination = _sut.Map(null);
            Assert.Null(destination);
        }

        [Fact]
        public void WhenMap_Item_To_Dto_ShouldAllPropertiesMapped()
        {
            var faker = DomainGenerator.ContactInfoGenerator();

            var entity = faker.Generate();


            var item = _sut.Map(entity);

            using (new AssertionScope())
            {
                entity.Type.Should().Be(item.Type);
                entity.Value.Should().Be(item.Value);
            }
        }
    }
}