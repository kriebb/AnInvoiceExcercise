using System;
using System.Threading.Tasks;
using Backend.API.CosmosDB.Data.DataModels.InvoiceManagement;
using Backend.API.CosmosDB.Data.Services.Infrastructure;
using Backend.API.Domain.Services.InvoiceManagement;
using Backend.API.Infrastructure.Mappings;

namespace Backend.API.CosmosDB.Data.Services.InvoiceManagement
{
    internal class InvoiceRepository : IInvoiceRepository
    {
        private readonly IDocumentDbRepository<DataModels.InvoiceManagement.Invoice> _genericRepo;
        private readonly IMapper<Domain.InvoiceManagement.Invoice, Invoice> _dataModelInvoiceMapper;
        private readonly IMapper<Invoice, Domain.InvoiceManagement.Invoice> _domainInvoiceMapper;

        public InvoiceRepository(
            IDocumentDbRepository<DataModels.InvoiceManagement.Invoice> genericRepo,
            IMapper<Domain.InvoiceManagement.Invoice, DataModels.InvoiceManagement.Invoice> dataModelInvoiceMapper,
            IMapper<DataModels.InvoiceManagement.Invoice, Domain.InvoiceManagement.Invoice> domainInvoiceMapper
        )
        {
            _genericRepo = genericRepo;
            _dataModelInvoiceMapper = dataModelInvoiceMapper;
            _domainInvoiceMapper = domainInvoiceMapper;
        }
        public async Task<Domain.InvoiceManagement.Invoice> AddAsync(Domain.InvoiceManagement.Invoice newDomainInvoice)
        {
            DataModels.InvoiceManagement.Invoice dataInvoice = _dataModelInvoiceMapper.Map(newDomainInvoice);

            dataInvoice = await _genericRepo.CreateAsync(dataInvoice);

            Domain.InvoiceManagement.Invoice domainInvoice = _domainInvoiceMapper.Map(dataInvoice);

            return domainInvoice;
        }

        public Domain.InvoiceManagement.Invoice Get(Guid invoiceId)
        {
            var invoiceDocument = _genericRepo.GetById(invoiceId.ToString());
            Domain.InvoiceManagement.Invoice domainInvoice = _domainInvoiceMapper.Map(invoiceDocument);

            return domainInvoice;

        }

        public async Task<Domain.InvoiceManagement.Invoice> UpdateAsync(Domain.InvoiceManagement.Invoice existingDomainInvoice)
        {
            DataModels.InvoiceManagement.Invoice dataInvoice = _dataModelInvoiceMapper.Map(existingDomainInvoice);

            var invoiceDocument = await _genericRepo.UpdateAsync(dataInvoice.Id, dataInvoice);

            Domain.InvoiceManagement.Invoice domainInvoice = _domainInvoiceMapper.Map(invoiceDocument);

            return domainInvoice;
        }
    }
}