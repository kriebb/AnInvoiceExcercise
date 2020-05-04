using System.Threading.Tasks;
using Backend.API.Domain.InvoiceManagement;

namespace Backend.API.Domain.Services.InvoiceManagement
{
    public interface IInvoiceRepository
    {
        Task AddAsync(Invoice invoice);
        Task<Invoice> GetAsync(long id);
        Task UpdateAsync(Invoice invoice);
    }
}
