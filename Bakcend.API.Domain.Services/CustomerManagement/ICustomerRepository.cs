using System;
using System.Threading.Tasks;
using Backend.API.Domain.CustomerManagement;

namespace Backend.API.Domain.Services.CustomerManagement
{
    public interface ICustomerRepository
    {
        Task< Domain.CustomerManagement.Customer> AddAsync( Domain.CustomerManagement.Customer newDomainCustomer);
        Domain.CustomerManagement.Customer Get(Guid customerId);
        Task< Domain.CustomerManagement.Customer> UpdateAsync( Domain.CustomerManagement.Customer existingDomainCustomer);
    }
}
