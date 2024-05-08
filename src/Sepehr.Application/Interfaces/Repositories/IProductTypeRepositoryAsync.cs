using Sepehr.Domain.Entities;

namespace Sepehr.Application.Interfaces.Repositories
{
    public interface IProductTypeRepositoryAsync : IGenericRepositoryAsync<ProductType>
    {
        Task<List<ProductType>> GetAllProductTypes();
        Task<ProductType?> GetProductTypeInfo(string desc);
    }
}