using Sepehr.Application.Features.CustomerLabels.Command.CreateCustomerLabel;
using Sepehr.Application.Features.Orders.Queries.GetAllOrders;
using Sepehr.Domain.Entities;

namespace Sepehr.Application.Interfaces.Repositories
{
    public interface ICustomerLabelRepositoryAsync : IGenericRepositoryAsync<CustomerLabel>
    {
        Task<List<CustomerLabel>> GetAllCustomerLabelsAsync(GetAllCustomerLabelsParameter filter);
        Task<CustomerLabel?> GetCustomerLabelInfo(CreateCustomerLabelCommand filter);
        Task<CustomerLabel?> GetCustomerLabelInfo(int Id);
        Task<CustomerLabel> UpdateCustomerLabelAsync(CustomerLabel customerLabel);
    }
}