using System.Threading.Tasks;
using Backend.API.Domain.CustomerManagement;

namespace Backend.API.Domain.Services.CustomerManagement
{
    public interface ICustomerRepository
    {
        Task AddAsync(Customer newCustomer);
        Task<Customer> GetAsync(in long customerId);
        Task UpdateAsync(Customer contact);
    }
}
