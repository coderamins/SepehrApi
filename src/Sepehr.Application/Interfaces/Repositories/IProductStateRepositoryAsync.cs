using Sepehr.Domain.Entities;

namespace Sepehr.Application.Interfaces.Repositories
{
    public interface IProductStateRepositoryAsync : IGenericRepositoryAsync<ProductState>
    {
        Task<ProductState?> GetProductStateInfo(string desc);
    }
}