using Microsoft.EntityFrameworkCore;
using Sepehr.Application.Interfaces.Repositories;
using Sepehr.Domain.Entities;
using Sepehr.Infrastructure.Persistence.Context;

namespace Sepehr.Infrastructure.Persistence.Repositories
{
    public class CostRepositoryAsync : GenericRepositoryAsync<Cost>, ICostRepositoryAsync
    {
        private readonly DbSet<Cost> _productCosts;

        public CostRepositoryAsync(ApplicationDbContext dbContext) : base(dbContext)
        {
            _productCosts = dbContext.Set<Cost>();
        }

        public async Task<List<Cost>> GetAllProductCosts()
        {
            return await _productCosts
                .OrderByDescending(p => p.Id).ToListAsync();
        }

        public async Task<Cost?> GetCostInfo(string CostDesc)
        {
            return await _productCosts
                .FirstOrDefaultAsync(p => p.CostDescription.Equals(CostDesc));
        }
    }
}