using System.Threading.Tasks;
using AutoFixture;
using Backend.API.CosmosDB.Data.DataModels.InvoiceManagement;
using Backend.API.CosmosDB.Data.Services.Infrastructure;
using Backend.API.CosmosDB.Data.Services.InvoiceManagement;
using Backend.API.Data.Generator;
using Backend.API.Infrastructure.Mappings;
using Backend.API.Infrastructure.Mappings.BootstrapAutoMapper;
using NSubstitute;
using Xunit;

namespace Backend.API.CosmosDB.Data.Tests.UnitTests
{
    public class GivenInvoiceRepository

    {
        private InvoiceRepository _sut;
        private IDocumentDbRepository<Invoice> _genericRepo;

        public GivenInvoiceRepository()
        {
            _genericRepo = Substitute.For<IDocumentDbRepository<DataModels.InvoiceManagement.Invoice>>();
            var fixture = new Fixture();

            var autoMapper = new AutoMapperFactory();
            IMapper<Domain.InvoiceManagement.Invoice, DataModels.InvoiceManagement.Invoice> dataModelInvoiceMapper
                = autoMapper.CreateMapper<Domain.InvoiceManagement.Invoice, DataModels.InvoiceManagement.Invoice>();

            IMapper<DataModels.InvoiceManagement.Invoice, Domain.InvoiceManagement.Invoice> domainInvoiceMapper 
                = autoMapper.CreateMapper<DataModels.InvoiceManagement.Invoice, Domain.InvoiceManagement.Invoice>();

            fixture.Inject(_genericRepo);
            fixture.Inject(dataModelInvoiceMapper);
            fixture.Inject(domainInvoiceMapper);

            _sut = fixture.Create<InvoiceRepository>();

        }
        [Fact]
        public async Task WhenWeAskToAdd_ShouldBeAskedToStoreInCosmosDB()
        {
            var newEntity = DomainGenerator.InvoiceGenerator().Generate();

            var updatedEntity = await _sut.AddAsync(newEntity);

            await _genericRepo.Received().CreateAsync(Arg.Any<Invoice>());

        }

        [Fact]
        public async Task WhenWeAskToUpdate_ShouldBeAskedToUpsertInCosmosDB()
        {
            var newInvoice = DomainGenerator.InvoiceGenerator().Generate();

            var updatedInvoice = await _sut.UpdateAsync(newInvoice);

            await _genericRepo.Received().UpdateAsync(Arg.Is<string>(newInvoice.Id.ToString()), Arg.Any<Invoice>());

        }
    }
}
