using Microsoft.EntityFrameworkCore;
using Sepehr.Application.Features.RentPayments.Queries.GetAllRentPayments;
using Sepehr.Application.Interfaces.Repositories;
using Sepehr.Domain;
using Sepehr.Domain.Entities;
using Sepehr.Domain.Enums;
using Sepehr.Infrastructure.Persistence.Context;

namespace Sepehr.Infrastructure.Persistence.Repositories
{
    public class RentPaymentRepositoryAsync : GenericRepositoryAsync<RentPayment>, IRentPaymentRepositoryAsync
    {
        private readonly DbSet<RentPayment> _rentPayments;
        private readonly DbSet<LadingExitPermit> _ladingExitPermits;
        private readonly DbSet<PurchaseOrderTransferRemittanceUnloadingPermit> _purOrdTransRemitUnload;

        public RentPaymentRepositoryAsync(ApplicationDbContext dbContext) : base(dbContext)
        {
            _purOrdTransRemitUnload = dbContext.Set<PurchaseOrderTransferRemittanceUnloadingPermit>();
            _ladingExitPermits = dbContext.Set<LadingExitPermit>();
            _rentPayments = dbContext.Set<RentPayment>();
        }

        public async Task<IEnumerable<RentPayment>> GetAllRentPaymentsAsync(GetAllRentPaymentsParameter validFilter)
        {
            return
                await _rentPayments
                .Include(r => r.PurchaseOrderTransferRemittanceUnloadingPermit)
                .Include(r => r.LadingExitPermit).ThenInclude(x=>x.LadingPermit).ThenInclude(x=>x.CargoAnnounce)
                .Where(x =>
                x.IsActive &&
                (validFilter.RentPaymentCode==x.Id || validFilter.RentPaymentCode==null) &&
                (
                (x.PurchaseOrderTransferRemittanceUnloadingPermit!=null && x.PurchaseOrderTransferRemittanceUnloadingPermit.UnloadingPermitCode==validFilter.ReferenceCode) || 
                (x.LadingExitPermit!=null && x.LadingExitPermit.LadingExitPermitCode==validFilter.ReferenceCode) || validFilter.ReferenceCode==null) &&
                (x.Created>=validFilter.FromDate.ToDateTime("00:00") || string.IsNullOrEmpty(validFilter.FromDate)) &&
                (x.Created<=validFilter.ToDate.ToDateTime("00:00") || string.IsNullOrEmpty(validFilter.ToDate)) &&
                ((x.PurchaseOrderTransferRemittanceUnloadingPermit!=null && x.PurchaseOrderTransferRemittanceUnloadingPermit.DriverName.Contains(validFilter.DriverName)) ||
                (x.LadingExitPermit!=null && x.LadingExitPermit.LadingPermit.CargoAnnounce.DriverName.Contains(validFilter.DriverName)) || 
                string.IsNullOrEmpty(validFilter.DriverName))
                ).ToListAsync();
        }

        public async Task<Tuple<List<LadingExitPermit>?, List<PurchaseOrderTransferRemittanceUnloadingPermit>?>> GetAllRentsAsync(
            GetAllRentsToPaymentParameter validParams)
        {
            var ladingExitPermits =await _ladingExitPermits
                .Include(x => x.LadingExitPermitDetails)
                .Where(x=>
                (x.IsActive && x.FareAmountApproved) &&
                (x.LadingPermit.CargoAnnounce!=null && x.LadingPermit.CargoAnnounce.DriverName.Contains(validParams.DriverName) || string.IsNullOrEmpty(validParams.DriverName)) &&
                (x.LadingPermit.CargoAnnounce != null && x.LadingPermit.CargoAnnounce.DriverMobile.Contains(validParams.DriverMobile) || string.IsNullOrEmpty(validParams.DriverMobile)) &&
                (x.LadingExitPermitCode==validParams.ReferenceCode || validParams.ReferenceCode==null) &&
                (x.Created>= validParams.FromDate.ToDateTime("00:00") || string.IsNullOrEmpty(validParams.FromDate)) &&
                (x.Created<= validParams.ToDate.ToDateTime("00:00") || string.IsNullOrEmpty(validParams.ToDate)) &&
                (validParams.OrderType== OrderClassType.Sale || validParams.OrderType == null) &&
                !x.FareAmountPayStatus)                
                .ToListAsync();

            var purOrdTransRemitUnloads =
                await _purOrdTransRemitUnload
                .Include(m => m.PurchaseOrderTransferRemittanceUnloadingPermitDetails)
                .Include(m => m.PurchaseOrderTransferRemittanceEntrancePermit)
                .Where(x=>
                (x.IsActive && x.FareAmountApproved) &&
                (x.DriverName.Contains(validParams.DriverName) || string.IsNullOrEmpty(validParams.DriverName)) &&
                (x.DriverMobile.Contains(validParams.DriverMobile) || string.IsNullOrEmpty(validParams.DriverMobile)) &&
                (x.UnloadingPermitCode == validParams.ReferenceCode || validParams.ReferenceCode == null) &&
                (x.Created >= validParams.FromDate.ToDateTime("00:00") || string.IsNullOrEmpty(validParams.FromDate)) &&
                (x.Created <= validParams.ToDate.ToDateTime("00:00") || string.IsNullOrEmpty(validParams.ToDate)) &&
                (validParams.OrderType == OrderClassType.Purchase || validParams.OrderType == null) &&
                !x.FareAmountPayStatus)
                .ToListAsync();

            return new Tuple<List<LadingExitPermit>?,List<PurchaseOrderTransferRemittanceUnloadingPermit>?>
                (ladingExitPermits, purOrdTransRemitUnloads);
        }

        public async Task<RentPayment?> GetRentPaymentInfo(int RentPaymentId)
        {
            return await _rentPayments
                .FirstOrDefaultAsync(p => p.Id == RentPaymentId);
        }
    }
}