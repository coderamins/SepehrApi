using Sepehr.Domain.Entities;

namespace Sepehr.Application.Interfaces.Repositories
{
    public interface ICostRepositoryAsync : IGenericRepositoryAsync<Cost>
    {
        Task<Cost?> GetCostInfo(string CostDesc);
        Task UpdateCostAsync(Cost cost);
    }
}