using Microsoft.EntityFrameworkCore;
using Sepehr.Application.Exceptions;
using Sepehr.Application.Interfaces.Repositories;
using Sepehr.Domain.Entities;
using Sepehr.Infrastructure.Persistence.Context;

namespace Sepehr.Infrastructure.Persistence.Repositories
{
    public class CostRepositoryAsync : GenericRepositoryAsync<Cost>, ICostRepositoryAsync
    {
        private readonly DbSet<Cost> _productCosts;
        private readonly ApplicationDbContext _dbContext;

        public CostRepositoryAsync(ApplicationDbContext dbContext) : base(dbContext)
        {
            _productCosts = dbContext.Set<Cost>();
            _dbContext = dbContext; 
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

        public async Task UpdateCostAsync(Cost cost)
        {
            var c=await _productCosts.FirstOrDefaultAsync(x=>x.Id==cost.Id);
            if (c == null)
                throw new ApiException("هزینه یافت نشد !");

            _dbContext.Entry(c).State=EntityState.Modified;
            _dbContext.Entry(c).CurrentValues.SetValues(cost);
            await _dbContext.SaveChangesAsync();
        }
    }
}