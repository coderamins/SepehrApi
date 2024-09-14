using Sepehr.Application.Features.ProductBrands.Queries.GetAllProductBrands;
using Sepehr.Application.Features.ProductBrands.Queries.GetAllProductPricesByProductType;
using Sepehr.Domain.Entities;

namespace Sepehr.Application.Interfaces.Repositories
{
    public interface IProductBrandRepositoryAsync : IGenericRepositoryAsync<ProductBrand>
    {
        Task<IEnumerable<ProductBrand>> GetAllProductBrands(GetAllProductBrandsParameter validFilter);
        Task<ProductBrand?> GetProductBrandInfo(Guid ProductId,int BrandId);
        Task<ProductBrand?> GetProductBrandInfo(int ProductCode, int BrandId);
        Task<IQueryable<ProductBrand>> GetAllProductPricesByProductType(GetAllProductPricesByProductTypeParameter validFilter);
    }
}