using Microsoft.EntityFrameworkCore;
using Sepehr.Application.Features.UnloadingPermits.Queries.GetAllUnloadingPermits;
using Sepehr.Application.Interfaces.Repositories;
using Sepehr.Domain.Entities;
using Sepehr.Domain.Enums;
using Sepehr.Infrastructure.Persistence.Context;

namespace Sepehr.Infrastructure.Persistence.Repositories
{
    public class UnloadingPermitRepositoryAsync :
        GenericRepositoryAsync<UnloadingPermit>, 
        IUnloadingPermitRepositoryAsync
    {
        private readonly DbSet<UnloadingPermit> _unloadingPermits;

        public UnloadingPermitRepositoryAsync(ApplicationDbContext dbContext) : base(dbContext)
        {
            _unloadingPermits = dbContext.Set<UnloadingPermit>();
        }

        public async Task<IEnumerable<UnloadingPermit>> GetAllUnloadingPermits(GetAllUnloadingPermitsParameter validFilter)
        {
            return _unloadingPermits
                .Include(x=>x.EntrancePermit)
                .Include(x=>x.ApplicationUser)
                .Include(x => x.UnloadingPermitDetails)
                        .ThenInclude(c => c.TransferRemittanceDetail)
                        .ThenInclude(x => x.ProductBrand)
                        .ThenInclude(x=>x.Brand)
                .Include(x => x.UnloadingPermitDetails)
                        .ThenInclude(c => c.TransferRemittanceDetail)
                        .ThenInclude(x => x.ProductBrand)
                        .ThenInclude(c => c.Product)
                        .ThenInclude(x => x.ProductMainUnit)
                .Include(x => x.UnloadingPermitDetails)
                        .ThenInclude(c => c.TransferRemittanceDetail)
                        .ThenInclude(x => x.ProductBrand)
                        .ThenInclude(c => c.Product)
                        .ThenInclude(x => x.ProductSubUnit)
                .AsSplitQuery()
                .Where(x => x.UnloadingPermitCode == validFilter.UnloadingPermitCode || validFilter.UnloadingPermitCode == null);

        }

        public async Task<UnloadingPermit?> GetUnloadingPermitInfo(Guid Id)
        {
            return await _unloadingPermits
                .Include(x => x.EntrancePermit)
                .Include(x => x.ApplicationUser)
                .Include(x => x.UnloadingPermitDetails)
                        .ThenInclude(c => c.TransferRemittanceDetail)
                        .ThenInclude(x => x.ProductBrand)
                        .ThenInclude(x => x.Brand)
                .Include(x => x.UnloadingPermitDetails)
                        .ThenInclude(c => c.TransferRemittanceDetail)
                        .ThenInclude(x => x.ProductBrand)
                        .ThenInclude(c => c.Product)
                        .ThenInclude(x => x.ProductMainUnit)
                .Include(x => x.UnloadingPermitDetails)
                        .ThenInclude(c => c.TransferRemittanceDetail)
                        .ThenInclude(x => x.ProductBrand)
                        .ThenInclude(c => c.Product)
                        .ThenInclude(x => x.ProductSubUnit)
                .Include(x => x.Attachments)
                .AsSplitQuery()
                .FirstOrDefaultAsync(x => x.Id == Id);
        }

        public async Task<UnloadingPermit?> GetUnloadingPermitInfo(int unloadingPermitCode)
        {
            return await _unloadingPermits
                .Include(x => x.EntrancePermit)
                .Include(x => x.ApplicationUser)
                .Include(x => x.UnloadingPermitDetails)
                        .ThenInclude(c => c.TransferRemittanceDetail)
                        .ThenInclude(x => x.ProductBrand)
                        .ThenInclude(x => x.Brand)
                .Include(x => x.UnloadingPermitDetails)
                        .ThenInclude(c => c.TransferRemittanceDetail)
                        .ThenInclude(x => x.ProductBrand)
                        .ThenInclude(c => c.Product)
                        .ThenInclude(x=>x.ProductMainUnit)
                .Include(x => x.UnloadingPermitDetails)
                        .ThenInclude(c => c.TransferRemittanceDetail)
                        .ThenInclude(x => x.ProductBrand)
                        .ThenInclude(c => c.Product)
                        .ThenInclude(x => x.ProductSubUnit)
                .Include(x => x.Attachments)
                .AsSplitQuery()
                .FirstOrDefaultAsync(/*x => x.UnloadingPermitCode == unloadingPermitCode*/);
        }

        public EFarePaymentType CheckFarePaymentType(Guid id)
        {
            return (EFarePaymentType)_unloadingPermits.Include(x => x.EntrancePermit).ThenInclude(x => x.TransferRemittance).ThenInclude(x => x.PurchaseOrder)
                .First(x => x.Id == id).EntrancePermit.TransferRemittance.PurchaseOrder.FarePaymentTypeId;
        }

    }
}