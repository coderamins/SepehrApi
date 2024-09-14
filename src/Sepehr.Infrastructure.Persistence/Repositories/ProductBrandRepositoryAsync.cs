using Microsoft.EntityFrameworkCore;
using Sepehr.Application.Features.ProductBrands.Queries.GetAllProductBrands;
using Sepehr.Application.Features.ProductBrands.Queries.GetAllProductPricesByProductType;
using Sepehr.Application.Interfaces.Repositories;
using Sepehr.Domain.Entities;
using Sepehr.Infrastructure.Persistence.Context;

namespace Sepehr.Infrastructure.Persistence.Repositories
{
    public class ProductBrandRepositoryAsync : GenericRepositoryAsync<ProductBrand>, IProductBrandRepositoryAsync
    {
        private readonly DbSet<ProductBrand> _productProductBrands;
        private readonly DbSet<ProductType> _productTypes;

        public ProductBrandRepositoryAsync(ApplicationDbContext dbContext) : base(dbContext)
        {
            _productProductBrands = dbContext.Set<ProductBrand>();
            _productTypes = dbContext.Set<ProductType>();
        }

        public async Task<IEnumerable<ProductBrand>> GetAllProductBrands(GetAllProductBrandsParameter validFilter)
        {
            return _productProductBrands;
        }

        public async Task<List<ProductBrand>> GetAllProductProductBrands()
        {
            return await _productProductBrands
                .OrderByDescending(p => p.Id).ToListAsync();
        }

        public async Task<ProductBrand?> GetProductBrandInfo(Guid ProductId,int BrandId)
        {
            return await _productProductBrands
                .FirstOrDefaultAsync(p => p.ProductId==ProductId && p.BrandId==BrandId);
        }

        public async Task<ProductBrand?> GetProductBrandInfo(int ProductCode, int BrandId)
        {
            return await _productProductBrands
                .Include(c=>c.ProductInventories)
                .FirstOrDefaultAsync(p => p.Product.ProductCode == ProductCode && p.BrandId == BrandId);
        }

        public async Task<IQueryable<ProductBrand>> GetAllProductPricesByProductType(GetAllProductPricesByProductTypeParameter validFilter)
        {
            var prodPrices = _productProductBrands
                .Include(c => c.Brand)                    
                .Include(c => c.Product)
                    .ThenInclude(x => x.ProductPrices.Where(p => p.IsActive));
                //.Where(x =>
                //    (x.Id == validFilter.ProductTypeId || validFilter.ProductTypeId == null));
                //(string.Concat(x.Brand.Name, ' ',
                //    x.ProductBrand.Product.ProductName, ' ',
                //    x.ProductBrand.Product.ProductType.Desc).Contains(validFilter.Keyword ?? "") || string.IsNullOrEmpty(validFilter.Keyword))
                //)

            return prodPrices;
        }
    }
}