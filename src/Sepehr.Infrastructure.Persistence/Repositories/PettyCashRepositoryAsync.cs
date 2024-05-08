using Microsoft.EntityFrameworkCore;
using Sepehr.Application.Interfaces.Repositories;
using Sepehr.Domain.Entities;
using Sepehr.Infrastructure.Persistence.Context;

namespace Sepehr.Infrastructure.Persistence.Repositories
{
    public class PettyCashRepositoryAsync : GenericRepositoryAsync<PettyCash>, IPettyCashRepositoryAsync
    {
        private readonly DbSet<PettyCash> _productPettyCashs;

        public PettyCashRepositoryAsync(ApplicationDbContext dbContext) : base(dbContext)
        {
            _productPettyCashs = dbContext.Set<PettyCash>();
        }

        public async Task<List<PettyCash>> GetAllProductPettyCashs()
        {
            return await _productPettyCashs
                .OrderByDescending(p => p.Id).ToListAsync();
        }

    }
}