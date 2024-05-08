using Microsoft.EntityFrameworkCore;
using Sepehr.Application.Interfaces.Repositories;
using Sepehr.Domain.Entities;
using Sepehr.Infrastructure.Persistence.Context;

namespace Sepehr.Infrastructure.Persistence.Repositories
{
    public class CashDeskRepositoryAsync : GenericRepositoryAsync<CashDesk>, ICashDeskRepositoryAsync
    {
        private readonly DbSet<CashDesk> _productCashDesks;

        public CashDeskRepositoryAsync(ApplicationDbContext dbContext) : base(dbContext)
        {
            _productCashDesks = dbContext.Set<CashDesk>();
        }

        public async Task<List<CashDesk>> GetAllProductCashDesks()
        {
            return await _productCashDesks
                .OrderByDescending(p => p.Id).ToListAsync();
        }

    }
}