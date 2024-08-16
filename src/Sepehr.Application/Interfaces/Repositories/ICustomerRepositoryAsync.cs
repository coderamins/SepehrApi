using Sepehr.Application.Features.Customers.Queries.GetAllCustomers;
using Sepehr.Domain.Entities;

namespace Sepehr.Application.Interfaces.Repositories
{
    public interface ICustomerRepositoryAsync : IGenericRepositoryAsync<Customer>
    {
        Task<bool> AllocateCustomerWarehouses(Guid id, List<int> warehouses);
        Task<bool> AssignCustomerLabels(ICollection<CustomerAssignedLabel> customerLabels);
        Task<List<Customer>> GetAllCustomers(GetAllCustomersParameter filter);
        Task<Customer> GetCustomerInfo(string nationalId);
        Task<Customer?> GetCustomerInfo(Guid Id);
        Task<Customer> UpdateCustomer(Customer customer);
    }
}