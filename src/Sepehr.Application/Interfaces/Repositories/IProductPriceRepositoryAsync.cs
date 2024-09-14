using Sepehr.Application.Features.ProductBrands.Queries.GetAllProductPricesByProductType;
using Sepehr.Application.Features.ProductPrices.Queries.GetAllProductPrices;
using Sepehr.Domain.Entities;

namespace Sepehr.Application.Interfaces.Repositories
{
    public interface IProductPriceRepositoryAsync : IGenericRepositoryAsync<ProductPrice>
    {
        Task<List<ProductPrice>> GetAllProductPrices(GetAllProductPricesParameter filter);
    }
}