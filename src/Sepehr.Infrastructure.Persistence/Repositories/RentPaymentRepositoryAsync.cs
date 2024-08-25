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
        private readonly DbSet<UnloadingPermit> _purOrdTransRemitUnload;

        public RentPaymentRepositoryAsync(ApplicationDbContext dbContext) : base(dbContext)
        {
            _purOrdTransRemitUnload = dbContext.Set<UnloadingPermit>();
            _ladingExitPermits = dbContext.Set<LadingExitPermit>();
            _rentPayments = dbContext.Set<RentPayment>();
        }

        public async Task<IEnumerable<RentPayment>> GetAllRentPaymentsAsync(GetAllRentPaymentsParameter validFilter)
        {
            return
                await _rentPayments
                .Include(c => c.ApplicationUser)
                .Include(r => r.UnloadingPermit)
                .Include(r => r.LadingExitPermit).ThenInclude(x=>x.LadingPermit).ThenInclude(x=>x.CargoAnnounce)
                .Where(x =>
                x.IsActive &&
                (validFilter.RentPaymentCode==x.Id || validFilter.RentPaymentCode==null) &&
                (
                (x.UnloadingPermit!=null && x.UnloadingPermit.UnloadingPermitCode==validFilter.ReferenceCode) || 
                (x.LadingExitPermit!=null && x.LadingExitPermit.LadingExitPermitCode==validFilter.ReferenceCode) || validFilter.ReferenceCode==null) &&
                (x.Created>=validFilter.FromDate.ToDateTime("00:00") || string.IsNullOrEmpty(validFilter.FromDate)) &&
                (x.Created<=validFilter.ToDate.ToDateTime("00:00") || string.IsNullOrEmpty(validFilter.ToDate)) &&
                ((x.UnloadingPermit!=null && x.UnloadingPermit.DriverName.Contains(validFilter.DriverName)) ||
                (x.LadingExitPermit!=null && x.LadingExitPermit.LadingPermit.CargoAnnounce.DriverName.Contains(validFilter.DriverName)) || 
                string.IsNullOrEmpty(validFilter.DriverName))
                ).ToListAsync();
        }

        public async Task<Tuple<List<LadingExitPermit>?, List<UnloadingPermit>?>> GetAllRentsAsync(
            GetAllRentsToPaymentParameter validParams)
        {
            var ladingExitPermits =await _ladingExitPermits
                .Include(x=>x.LadingPermit).ThenInclude(x=>x.CargoAnnounce)
                .Include(c => c.ApplicationUser)
                .Include(x => x.LadingExitPermitDetails)
                .Where(x=>
                (x.LadingPermit.CargoAnnounce.Order.FarePaymentTypeId==(int)EFarePaymentType.FareByOurselves) &&
                (x.IsActive && x.FareAmountStatusId==(int?)EFareAmountStatus.InProgress) &&
                (x.LadingPermit.CargoAnnounce!=null && x.LadingPermit.CargoAnnounce.DriverName.Contains(validParams.DriverName) || string.IsNullOrEmpty(validParams.DriverName)) &&
                (x.LadingPermit.CargoAnnounce != null && x.LadingPermit.CargoAnnounce.DriverMobile.Contains(validParams.DriverMobile) || string.IsNullOrEmpty(validParams.DriverMobile)) &&
                (x.LadingExitPermitCode==validParams.ReferenceCode || validParams.ReferenceCode==null) &&
                (x.Created>= validParams.FromDate.ToDateTime("00:00") || string.IsNullOrEmpty(validParams.FromDate)) &&
                (x.Created<= validParams.ToDate.ToDateTime("00:00") || string.IsNullOrEmpty(validParams.ToDate)) &&
                (validParams.OrderType== OrderClassType.Sale || validParams.OrderType == null))                
                .ToListAsync();

            var purOrdTransRemitUnloads =
                await _purOrdTransRemitUnload
                .Include(c => c.ApplicationUser)
                .Include(m => m.UnloadingPermitDetails)
                .Include(m => m.EntrancePermit)
                .Where(x=>
                (x.EntrancePermit.TransferRemittance.PurchaseOrder.FarePaymentTypeId == (int)EFarePaymentType.FareByOurselves) &&
                (x.IsActive && x.FareAmountStatusId==(int)EFareAmountStatus.InProgress) &&
                (x.DriverName.Contains(validParams.DriverName) || string.IsNullOrEmpty(validParams.DriverName)) &&
                (x.DriverMobile.Contains(validParams.DriverMobile) || string.IsNullOrEmpty(validParams.DriverMobile)) &&
                (x.UnloadingPermitCode == validParams.ReferenceCode || validParams.ReferenceCode == null) &&
                (x.Created >= validParams.FromDate.ToDateTime("00:00") || string.IsNullOrEmpty(validParams.FromDate)) &&
                (x.Created <= validParams.ToDate.ToDateTime("00:00") || string.IsNullOrEmpty(validParams.ToDate)) &&
                (validParams.OrderType == OrderClassType.Purchase || validParams.OrderType == null))
                .ToListAsync();

            return new Tuple<List<LadingExitPermit>?,List<UnloadingPermit>?>
                (ladingExitPermits, purOrdTransRemitUnloads);
        }

        public async Task<RentPayment?> GetRentPaymentInfo(int RentPaymentId)
        {
            return await _rentPayments
                .FirstOrDefaultAsync(p => p.Id == RentPaymentId);
        }
    }
}