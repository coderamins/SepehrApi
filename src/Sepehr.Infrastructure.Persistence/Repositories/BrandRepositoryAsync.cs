using Microsoft.EntityFrameworkCore;
using Sepehr.Application.Interfaces.Repositories;
using Sepehr.Domain.Entities;
using Sepehr.Infrastructure.Persistence.Context;

namespace Sepehr.Infrastructure.Persistence.Repositories
{
    public class BrandRepositoryAsync : GenericRepositoryAsync<Brand>, IBrandRepositoryAsync
    {
        private readonly DbSet<Brand> _productBrands;

        public BrandRepositoryAsync(ApplicationDbContext dbContext) : base(dbContext)
        {
            _productBrands = dbContext.Set<Brand>();
        }

        public async Task<List<Brand>> GetAllProductBrands()
        {
            return await _productBrands
                .OrderByDescending(p => p.Id).ToListAsync();
        }

        public async Task<Brand?> GetBrandInfo(string Name)
        {
            return await _productBrands
                .FirstOrDefaultAsync(p => p.Name.Equals(Name));
        }
    }
}