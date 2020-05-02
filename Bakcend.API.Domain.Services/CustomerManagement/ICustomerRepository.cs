using Backend.API.Domain.CustomerManagement;

namespace Backend.API.Domain.Services.CustomerManagement
{
    public interface ICustomerRepository
    {
        void Add(Customer newCustomer);
        Customer Get(in long customerId);
    }
}
