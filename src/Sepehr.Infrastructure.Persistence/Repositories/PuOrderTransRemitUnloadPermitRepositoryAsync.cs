using Microsoft.EntityFrameworkCore;
using Sepehr.Application.Interfaces.Repositories;
using Sepehr.Domain.Entities;
using Sepehr.Infrastructure.Persistence.Context;

namespace Sepehr.Infrastructure.Persistence.Repositories
{
    public class PuOrderTransRemitUnloadPermitRepositoryAsync :
        GenericRepositoryAsync<UnloadingPermit>, 
        IPuOrderTransRemitUnloadPermitRepositoryAsync
    {
        private readonly DbSet<UnloadingPermit> _remittUnloadingPermits;

        public PuOrderTransRemitUnloadPermitRepositoryAsync(ApplicationDbContext dbContext) : base(dbContext)
        {
            _remittUnloadingPermits = dbContext.Set<UnloadingPermit>();
        }

        public async Task<UnloadingPermit?> GetRemittanceUnloadingPermitInfo(Guid Id)
        {
            return await _remittUnloadingPermits
                .Include(c => c.ApplicationUser)
                .FirstOrDefaultAsync(x => x.Id == Id);
        }
    }
}