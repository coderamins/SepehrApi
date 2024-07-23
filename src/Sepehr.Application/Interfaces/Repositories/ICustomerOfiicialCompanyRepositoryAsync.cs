using Sepehr.Application.Features.CustomerOfficialCompanys.Queries.GetAllCustomerOfficialCompanys;
using Sepehr.Domain.Entities;

namespace Sepehr.Application.Interfaces.Repositories
{
    public interface ICustomerOfficialCompanyRepositoryAsync : IGenericRepositoryAsync<CustomerOfficialCompany>
    {
        Task<List<CustomerOfficialCompany>> GetAllCustomerOfficialCompanies(GetAllCustomerOfficialCompanysParameter filter);
        Task<CustomerOfficialCompany> GetCustomerOfficialCompanyById(int companyId);
    }
}