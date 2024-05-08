using Microsoft.EntityFrameworkCore;
using Sepehr.Application.Interfaces.Repositories;
using Sepehr.Domain.Entities;
using Sepehr.Infrastructure.Persistence.Context;

namespace Sepehr.Infrastructure.Persistence.Repositories
{
    public class ProductStateRepositoryAsync : GenericRepositoryAsync<ProductState>, IProductStateRepositoryAsync
    {
        private readonly DbSet<ProductState> _productStates;

        public ProductStateRepositoryAsync(ApplicationDbContext dbContext) : base(dbContext)
        {
            _productStates = dbContext.Set<ProductState>();
        }

        public async Task<List<ProductState>> GetAllProductStates()
        {
            return await _productStates
                .OrderByDescending(p => p.Id).ToListAsync();
        }

        public async Task<ProductState?> GetProductStateInfo(string desc)
        {
            return await _productStates
                .FirstOrDefaultAsync(p => p.Desc==desc);
        }
    }
}