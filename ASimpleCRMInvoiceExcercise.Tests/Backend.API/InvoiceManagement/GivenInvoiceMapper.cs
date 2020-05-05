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
    public class GivenInvoiceMapper
    {
        private IMapper<Invoice, InvoiceItem> _sut;

        public GivenInvoiceMapper()
        {
            _sut = new AutoMapperFactory().CreateMapper<Invoice, InvoiceItem>();
        }

        [Fact]
        public void WhenMapFromNullAsInvoice_ShouldReturnNull()
        {
            var item = _sut.Map(null);

            Assert.Null(item);
        }
        [Fact]
        public void WhenMapFromInvoice_To_InvoiceDto_AllPropertiesShouldBeMapped()
        {
            var entity = DomainGenerator.InvoiceGenerator().Generate();
            var item = _sut.Map(entity);

            using (new AssertionScope())
            {
                entity.Customer.Id.Should().Be(item.CustomerId);
                entity.Date.Should().Be(item.Date);
                entity.Summary.Should().Be(item.Summary);
                entity.TotalAmount.Should().Be(item.TotalAmount);
            }
        }

        [Fact]
        public void WhenMapFromInvoiceWithNoCustomer_To_InvoiceDto_AllPropertiesShouldBeMapped()
        {
            var entity = DomainGenerator.InvoiceGenerator().Generate();
            entity.Customer = null;
            var item = _sut.Map(entity);

            using (new AssertionScope())
            {
                entity.Customer?.Id.Should().Be(item.CustomerId);
                entity.Date.Should().Be(item.Date);
                entity.Summary.Should().Be(item.Summary);
                entity.TotalAmount.Should().Be(item.TotalAmount);
            }
        }
    }
}