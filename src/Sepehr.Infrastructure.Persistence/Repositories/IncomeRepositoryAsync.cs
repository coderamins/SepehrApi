using Microsoft.EntityFrameworkCore;
using Sepehr.Application.Interfaces.Repositories;
using Sepehr.Domain.Entities;
using Sepehr.Infrastructure.Persistence.Context;

namespace Sepehr.Infrastructure.Persistence.Repositories
{
    public class IncomeRepositoryAsync : GenericRepositoryAsync<Income>, IIncomeRepositoryAsync
    {
        private readonly DbSet<Income> _incomes;

        public IncomeRepositoryAsync(ApplicationDbContext dbContext) : base(dbContext)
        {
            _incomes = dbContext.Set<Income>();
        }

        public async Task<List<Income>> GetAllProductIncomes()
        {
            return await _incomes
                .OrderByDescending(p => p.Id).ToListAsync();
        }

        public async Task<Income?> GetIncomeInfo(string IncomeDesc)
        {
            return await _incomes
                .FirstOrDefaultAsync(p => p.IncomeDescription.Equals(IncomeDesc));
        }
    }
}