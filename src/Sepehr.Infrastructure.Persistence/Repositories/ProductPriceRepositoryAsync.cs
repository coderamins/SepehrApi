using Microsoft.EntityFrameworkCore;
using Sepehr.Application.Features.ProductBrands.Queries.GetAllProductPricesByProductType;
using Sepehr.Application.Features.ProductPrices.Queries.GetAllProductPrices;
using Sepehr.Application.Interfaces.Repositories;
using Sepehr.Domain.Entities;
using Sepehr.Infrastructure.Persistence.Context;

namespace Sepehr.Infrastructure.Persistence.Repositories
{
    public class ProductPriceRepositoryAsync : GenericRepositoryAsync<ProductPrice>, IProductPriceRepositoryAsync
    {
        private readonly DbSet<ProductPrice> _productPrices;

        public ProductPriceRepositoryAsync(ApplicationDbContext dbContext) : base(dbContext)
        {
            _productPrices = dbContext.Set<ProductPrice>();
        }

        public async Task<List<ProductPrice>> GetAllProductPrices(GetAllProductPricesParameter filter)
        {
            return await _productPrices
                .Include(c => c.ApplicationUser)
                .Include(p => p.ProductBrand).ThenInclude(b=>b.Product).ThenInclude(p=>p.ProductInventories).ThenInclude(i=>i.Warehouse)
                .Include(p => p.ProductBrand).ThenInclude(b=>b.Brand)
                //.Include(i => i.ProductBrand)
                .OrderByDescending(p => p.Created).ToListAsync();
        }


    }
}