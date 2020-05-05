using System;
using System.Threading.Tasks;
using Backend.API.Domain.InvoiceManagement;

namespace Backend.API.Domain.Services.InvoiceManagement
{
    public interface IInvoiceRepository
    {
        Task<Invoice> AddAsync(Invoice invoice);
        Invoice Get(Guid invoiceId);
        Task<Invoice> UpdateAsync(Invoice invoice);
    }
}
