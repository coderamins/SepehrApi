using Sepehr.Domain.Entities;

namespace Sepehr.Application.Interfaces.Repositories
{
    public interface IOrganizationBankRepositoryAsync : IGenericRepositoryAsync<OrganizationBank>
    {
        Task<OrganizationBank?> GetOrganizationBankInfo(string accountNo);
        Task<OrganizationBank?> GetOrganizationBankInfo(int Id);
    }
}