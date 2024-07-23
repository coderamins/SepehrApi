using Microsoft.EntityFrameworkCore;
using Sepehr.Application.Interfaces.Repositories;
using Sepehr.Domain.Entities;
using Sepehr.Infrastructure.Persistence.Context;

namespace Sepehr.Infrastructure.Persistence.Repositories
{
    public class PuOrderTransRemitUnloadPermitRepositoryAsync :
        GenericRepositoryAsync<PurchaseOrderTransferRemittanceUnloadingPermit>, 
        IPuOrderTransRemitUnloadPermitRepositoryAsync
    {
        private readonly DbSet<PurchaseOrderTransferRemittanceUnloadingPermit> _remittUnloadingPermits;

        public PuOrderTransRemitUnloadPermitRepositoryAsync(ApplicationDbContext dbContext) : base(dbContext)
        {
            _remittUnloadingPermits = dbContext.Set<PurchaseOrderTransferRemittanceUnloadingPermit>();
        }

        public async Task<PurchaseOrderTransferRemittanceUnloadingPermit?> GetRemittanceUnloadingPermitInfo(Guid Id)
        {
            return await _remittUnloadingPermits
                .Include(c => c.ApplicationUser)
                .FirstOrDefaultAsync(x => x.Id == Id);
        }
    }
}