using Microsoft.EntityFrameworkCore;
using Sepehr.Application.Interfaces.Repositories;
using Sepehr.Domain.Entities;
using Sepehr.Infrastructure.Persistence.Context;

namespace Sepehr.Infrastructure.Persistence.Repositories
{
    public class ProductStandardRepositoryAsync : GenericRepositoryAsync<ProductStandard>, IProductStandardRepositoryAsync
    {
        private readonly DbSet<ProductStandard> _productStandards;

        public ProductStandardRepositoryAsync(ApplicationDbContext dbContext) : base(dbContext)
        {
            _productStandards = dbContext.Set<ProductStandard>();
        }

        public async Task<List<ProductStandard>> GetAllProductStandards()
        {
            return await _productStandards
                .OrderByDescending(p => p.Id).ToListAsync();
        }

        public async Task<ProductStandard?> GetProductStandardInfo(string desc)
        {
            return await _productStandards
                .FirstOrDefaultAsync(p => p.Desc==desc);
        }
    }
}