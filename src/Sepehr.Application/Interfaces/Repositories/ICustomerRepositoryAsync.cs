using Sepehr.Domain.Entities;

namespace Sepehr.Application.Interfaces.Repositories
{
    public interface ICustomerRepositoryAsync : IGenericRepositoryAsync<Customer>
    {
        Task<bool> AllocateCustomerWarehouses(Guid id, List<int> warehouses);
        Task<List<Customer>> GetAllCustomers();
        Task<Customer> GetCustomerInfo(string nationalId);
        Task<Customer?> GetCustomerInfo(Guid Id);
    }
}