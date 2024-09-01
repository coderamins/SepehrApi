using Sepehr.Application.Features.Customers.Queries.GetAllCustomers;
using Sepehr.Domain.Entities;
using Sepehr.Domain.ViewModels;

namespace Sepehr.Application.Interfaces.Repositories
{
    public interface ICustomerRepositoryAsync : IGenericRepositoryAsync<Customer>
    {
        Task<bool> AllocateCustomerWarehouses(Guid id, List<int> warehouses);
        Task<bool> AssignCustomerLabels(ICollection<CustomerAssignedLabel> customerLabels);
        Task<IQueryable<Customer>> GetAllCustomers(GetAllCustomersParameter filter);
        Task<List<CustomerBalanceViewModel>> GetCustomerBalance(GetCustomersBalanceParameter validFilter);
        Task<Customer> GetCustomerInfo(string nationalId);
        Task<Customer?> GetCustomerInfo(Guid Id);
        Task<Customer> UpdateCustomer(Customer customer);
    }
}