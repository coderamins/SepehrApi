using Sepehr.Domain.Entities;

namespace Sepehr.Application.Interfaces.Repositories
{
    public interface IProductStandardRepositoryAsync : IGenericRepositoryAsync<ProductStandard>
    {
        Task<ProductStandard?> GetProductStandardInfo(string desc);
    }
}