using Backend.API.Domain.InvoiceManagement;

namespace Backend.API.Domain.Services.InvoiceManagement
{
    public interface IInvoiceRepository
    {
        void Add(Invoice invoice);
        Invoice Get(in int id);
    }
}
