using Sepehr.Domain.Entities;

namespace Sepehr.Application.Interfaces.Repositories
{
    public interface IProductBrandRepositoryAsync : IGenericRepositoryAsync<ProductBrand>
    {
        Task<ProductBrand?> GetProductBrandInfo(Guid ProductId,int BrandId);
        Task<ProductBrand?> GetProductBrandInfo(int ProductCode, int BrandId);
    }
}