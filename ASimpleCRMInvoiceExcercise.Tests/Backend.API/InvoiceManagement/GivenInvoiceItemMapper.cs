using Backend.API.Data.Generator;
using Backend.API.Domain.InvoiceManagement;
using Backend.API.Dtos.InvoiceManagement;
using Backend.API.Infrastructure.Mappings;
using Backend.API.Infrastructure.Mappings.BootstrapAutoMapper;
using FluentAssertions;
using FluentAssertions.Execution;
using Xunit;

namespace Backend.API.Tests.Backend.API.InvoiceManagement
{
    public class GivenInvoiceItemMapper
    {
        private IMapper<InvoiceItem, Invoice> _sut;

        public GivenInvoiceItemMapper()
        {
            _sut = new AutoMapperFactory().CreateMapper<InvoiceItem, Invoice>();
        }


        [Fact]
        public void WhenMapFromNullAsInvoiceDto_ShouldReturnNull()
        {
            var entity = _sut.Map(null);
            Assert.Null(entity);
        }

        [Fact]
        public void WhenMapFromInvoice_To_InvoiceDto_AllPropertiesShouldBeMapped()
        {
            var item = ApiDtoGenerator.InvoiceItemGenerator().Generate();
            var entity = _sut.Map(item);

            using (new AssertionScope())
            {
                item.Date.Should().Be(entity.Date);
                item.CustomerId.Should().Be(entity.Customer.Id);
                item.Summary.Should().Be(entity.Summary);
                item.TotalAmount.Should().Be(entity.TotalAmount);
                item.Id.Should().Be(entity.Id);
            }
        }


    }
}