using System.Threading.Tasks;
using AutoFixture;
using Backend.API.CosmosDB.Data.DataModels.CustomerManagement;
using Backend.API.CosmosDB.Data.Services.CustomerManagement;
using Backend.API.CosmosDB.Data.Services.Infrastructure;
using Backend.API.Data.Generator;
using Backend.API.Infrastructure.Mappings;
using Backend.API.Infrastructure.Mappings.BootstrapAutoMapper;
using NSubstitute;
using Xunit;

namespace Backend.API.CosmosDB.Data.Tests.UnitTests
{
    public class GivenCustomerRepository

    {
        private CustomerRepository _sut;
        private IDocumentDbRepository<Customer> _genericRepo;

        public GivenCustomerRepository()
        {
            _genericRepo = Substitute.For<IDocumentDbRepository<DataModels.CustomerManagement.Customer>>();
            var fixture = new Fixture();

            var autoMapper = new AutoMapperFactory();
            IMapper<Domain.CustomerManagement.Customer, DataModels.CustomerManagement.Customer> dataModelCustomerMapper
                = autoMapper.CreateMapper<Domain.CustomerManagement.Customer, DataModels.CustomerManagement.Customer>();

            IMapper<DataModels.CustomerManagement.Customer, Domain.CustomerManagement.Customer> domainCustomerMapper 
                = autoMapper.CreateMapper<DataModels.CustomerManagement.Customer, Domain.CustomerManagement.Customer>();

            fixture.Inject(_genericRepo);
            fixture.Inject(dataModelCustomerMapper);
            fixture.Inject(domainCustomerMapper);

            _sut = fixture.Create<CustomerRepository>();

        }
        [Fact]
        public async Task WhenWeAskToAddACustomer_ShouldBeAskedToStoreInCosmosDB()
        {
            var newCustomer = DomainGenerator.CustomerGenerator().Generate();

            var updatedCustomer = await _sut.AddAsync(newCustomer);

            await _genericRepo.Received().CreateAsync(Arg.Any<Customer>());

        }

        [Fact]
        public async Task WhenWeAskToUpdateACustomer_ShouldBeAskedToUpsertInCosmosDB()
        {
            var newCustomer = DomainGenerator.CustomerGenerator().Generate();

            var updatedCustomer = await _sut.UpdateAsync(newCustomer);

            await _genericRepo.Received().UpdateAsync(Arg.Is<string>(newCustomer.Id.ToString()), Arg.Any<Customer>());

        }

    }
}
