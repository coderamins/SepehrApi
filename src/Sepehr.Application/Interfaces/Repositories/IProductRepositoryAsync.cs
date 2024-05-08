using Sepehr.Application.DTOs.Order;
using Sepehr.Application.Features.Products.Queries.GetAllProducts;
using Sepehr.Domain.Entities;
using Sepehr.Domain.ViewModels;

namespace Sepehr.Application.Interfaces.Repositories
{
    public interface IProductRepositoryAsync : IGenericRepositoryAsync<Product>
    {
        Task<bool> DisableProduct(Guid id);
        Task<bool> EnableProduct(Guid id);
        Task<List<Product>> GetAllProducts(GetAllProductsParameter filter);
        Task<IEnumerable<DapperProduct>> GetAllProductsByInventory(GetAllProductsParameter validFilter);
        Task<Product> GetProductInfoAsync(long productCode);
        Task<bool> IsUniqueBarcodeAsync(string barcode);
        Task<List<ProductType>> GetProductTypes();
        Task<int> GenerateNewProductCode(int? productTypeId);
        Task<List<ProductBrand>> GetProductBrands(List<OrderDetailRequest> details);
    }
}