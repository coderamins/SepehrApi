using Sepehr.Domain.Entities;

namespace Sepehr.Application.Interfaces.Repositories
{
    public interface IIncomeRepositoryAsync : IGenericRepositoryAsync<Income>
    {
        Task<Income?> GetIncomeInfo(string IncomeDesc);
    }
}