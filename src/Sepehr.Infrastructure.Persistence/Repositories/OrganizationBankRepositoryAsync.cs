using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using Sepehr.Application.Interfaces.Repositories;
using Sepehr.Domain.Entities;
using Sepehr.Infrastructure.Persistence.Context;

namespace Sepehr.Infrastructure.Persistence.Repositories
{
    public class OrganizationBankRepositoryAsync : GenericRepositoryAsync<OrganizationBank>, IOrganizationBankRepositoryAsync
    {
        private readonly DbSet<OrganizationBank> _organizationBanks;

        public OrganizationBankRepositoryAsync(ApplicationDbContext dbContext) : base(dbContext)
        {
            _organizationBanks = dbContext.Set<OrganizationBank>();
        }

        public async Task<List<OrganizationBank>> GetAllOrganizationBanks()
        {
            return await _organizationBanks
                .Include(b => b.Bank)
                .OrderByDescending(p => p.Id).ToListAsync();
        }

        public async Task<OrganizationBank?> GetOrganizationBankInfo(string accountNo)
        {
            return await _organizationBanks
                .Include(b => b.Bank)
                .FirstOrDefaultAsync(p => p.AccountNo.Equals(accountNo));
        }

        public async Task<OrganizationBank?> GetOrganizationBankInfo(int Id)
        {
            return await _organizationBanks
                .Include(b=>b.Bank)
                .FirstOrDefaultAsync(p => p.Id.Equals(Id));
        }
    }
}