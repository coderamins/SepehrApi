using Microsoft.EntityFrameworkCore;
using Sepehr.Application.Interfaces.Repositories;
using Sepehr.Domain.Entities;
using Sepehr.Infrastructure.Persistence.Context;
using Sepehr.Infrastructure.Persistence.Seeds;

namespace Sepehr.Infrastructure.Persistence.Repositories
{
    public class ProductTypeRepositoryAsync : GenericRepositoryAsync<ProductType>, IProductTypeRepositoryAsync
    {
        private readonly DbSet<ProductType> _productTypes;

        public ProductTypeRepositoryAsync(ApplicationDbContext dbContext) : base(dbContext)
        {
            _productTypes = dbContext.Set<ProductType>();
        }

        public async Task<List<ProductType>> GetAllProductTypes()
        {
            return await _productTypes
                .OrderByDescending(p => p.Id).ToListAsync();
        }

        public async Task<ProductType?> GetProductTypeInfo(string desc)
        {
            return await _productTypes
                .FirstOrDefaultAsync(p => p.Desc == desc);
        }
    }
}