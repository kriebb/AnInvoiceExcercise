using System;
using System.Threading.Tasks;
using Backend.API.CosmosDB.Data.DataModels.CustomerManagement;
using Backend.API.CosmosDB.Data.Services.Infrastructure;
using Backend.API.Domain.Services.CustomerManagement;
using Backend.API.Infrastructure.Mappings;

namespace Backend.API.CosmosDB.Data.Services.CustomerManagement
{
    internal class CustomerRepository : ICustomerRepository
    {
        private readonly IDocumentDbRepository<DataModels.CustomerManagement.Customer> _genericRepo;
        private readonly IMapper<Domain.CustomerManagement.Customer, Customer> _dataModelCustomerMapper;
        private readonly IMapper<Customer, Domain.CustomerManagement.Customer> _domainCustomerMapper;

        public CustomerRepository(
            IDocumentDbRepository<DataModels.CustomerManagement.Customer> genericRepo,
            IMapper<Domain.CustomerManagement.Customer, DataModels.CustomerManagement.Customer> dataModelCustomerMapper,
            IMapper<DataModels.CustomerManagement.Customer, Domain.CustomerManagement.Customer> domainCustomerMapper
        )
        {
            _genericRepo = genericRepo;
            _dataModelCustomerMapper = dataModelCustomerMapper;
            _domainCustomerMapper = domainCustomerMapper;
        }
        public async Task<Domain.CustomerManagement.Customer> AddAsync(Domain.CustomerManagement.Customer newDomainCustomer)
        {
            DataModels.CustomerManagement.Customer dataCustomer = _dataModelCustomerMapper.Map(newDomainCustomer);

            dataCustomer = await _genericRepo.CreateAsync(dataCustomer);

            Domain.CustomerManagement.Customer domainCustomer = _domainCustomerMapper.Map(dataCustomer);

            return domainCustomer;
        }

        public Domain.CustomerManagement.Customer Get(Guid CustomerId)
        {
            var CustomerDocument = _genericRepo.GetById(CustomerId.ToString());
            Domain.CustomerManagement.Customer domainCustomer = _domainCustomerMapper.Map(CustomerDocument);

            return domainCustomer;

        }

        public async Task<Domain.CustomerManagement.Customer> UpdateAsync(Domain.CustomerManagement.Customer existingDomainCustomer)
        {
            DataModels.CustomerManagement.Customer dataCustomer = _dataModelCustomerMapper.Map(existingDomainCustomer);

            var CustomerDocument = await _genericRepo.UpdateAsync(dataCustomer.Id, dataCustomer);

            Domain.CustomerManagement.Customer domainCustomer = _domainCustomerMapper.Map(CustomerDocument);

            return domainCustomer;
        }
    }
}
