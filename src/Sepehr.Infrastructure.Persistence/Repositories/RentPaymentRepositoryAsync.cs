using Microsoft.EntityFrameworkCore;
using Sepehr.Application.Exceptions;
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
        private readonly DbSet<RentPaymentDetail> _rentPaymentDetails;
        private readonly DbSet<LadingExitPermit> _ladingExitPermits;
        private readonly DbSet<UnloadingPermit> _purOrdTransRemitUnload;
        private readonly DbContext _dbContext;

        public RentPaymentRepositoryAsync(ApplicationDbContext dbContext) : base(dbContext)
        {
            _purOrdTransRemitUnload = dbContext.Set<UnloadingPermit>();
            _ladingExitPermits = dbContext.Set<LadingExitPermit>();
            _rentPayments = dbContext.Set<RentPayment>();
            _rentPaymentDetails = dbContext.Set<RentPaymentDetail>();
            _dbContext= dbContext;
        }

        public async Task<RentPayment> CreateRentPayment(RentPayment rentPayment)
        {
            foreach(var rentDet in rentPayment.RentPaymentDetails)
            {
                var ladingExit = await _ladingExitPermits.FirstOrDefaultAsync(x => x.Id == rentDet.LadingExitPermitId);

                var unloadPermit = await _purOrdTransRemitUnload.FirstOrDefaultAsync(x => x.Id == rentDet.UnloadingPermitId);

                if (ladingExit != null)
                {
                    var lExitEntry = _ladingExitPermits.Entry(ladingExit);
                    lExitEntry.State = EntityState.Modified;

                    ladingExit.FareAmountStatusId = (int)EFareAmountStatus.Payed;
                    lExitEntry.CurrentValues.SetValues(ladingExit);
                }

                if (unloadPermit != null)
                {
                    var unloadPermitEntry = _purOrdTransRemitUnload.Entry(unloadPermit);
                    unloadPermitEntry.State = EntityState.Modified;

                    unloadPermit.FareAmountStatusId = (int)EFareAmountStatus.Payed;
                    unloadPermitEntry.CurrentValues.SetValues(unloadPermit);
                }
            }

            await _dbContext.SaveChangesAsync();

            var _rentPayment = await _rentPayments.AddAsync(rentPayment);

            return _rentPayment.Entity;
        }

        public async Task<RentPayment> UpdateRentPayment(RentPayment rentPayment)
        {
            _rentPaymentDetails.RemoveRange(_rentPaymentDetails.Where(x => x.RentPaymentId == rentPayment.Id)); 
            _dbContext.Set<Attachment>().RemoveRange(_dbContext.Set<Attachment>().Where(x => x.RentPaymentId == rentPayment.Id)); 
            foreach (var rentDet in rentPayment.RentPaymentDetails)
            {
                var ladingExit = await _ladingExitPermits.FirstOrDefaultAsync(x => x.Id == rentDet.LadingExitPermitId);
                if (ladingExit == null)
                    throw new ApiException("مجوز خروج یافت نشد !");

                var unloadPermit = await _purOrdTransRemitUnload.FirstOrDefaultAsync(x => x.Id == rentDet.UnloadingPermitId);
                if (unloadPermit == null)
                    throw new ApiException("مجوز تخلیه یافت نشد !");

                var lExitEntry = _ladingExitPermits.Entry(ladingExit);
                lExitEntry.State = EntityState.Modified;

                ladingExit.FareAmountStatusId = (int)EFareAmountStatus.Payed;
                lExitEntry.CurrentValues.SetValues(ladingExit);

                var unloadPermitEntry = _ladingExitPermits.Entry(ladingExit);
                unloadPermitEntry.State = EntityState.Modified;

                unloadPermit.FareAmountStatusId = (int)EFareAmountStatus.Payed;
                unloadPermitEntry.CurrentValues.SetValues(ladingExit);
            }

            await _dbContext.SaveChangesAsync();

            var _rentPayment = _rentPayments.Update(rentPayment);

            return _rentPayment.Entity;
        }

        public async Task<IEnumerable<RentPayment>> GetAllRentPaymentsAsync(GetAllRentPaymentsParameter validFilter)
        {
            return
                await _rentPayments
                .Include(c => c.PaymentFromCashDesk)
                .Include(c => c.PaymentFromCost)
                .Include(c => c.PaymentFromCustomer)
                .Include(c => c.PaymentFromIncome)
                .Include(c => c.PaymentFromOrganizationBank)
                .Include(c => c.PaymentFromPettyCash)
                .Include(c => c.PaymentFromShareHolder)
                .Include(c => c.PaymentOriginType)
                .Include(c => c.ApplicationUser)
                .Include(r => r.RentPaymentDetails
                        .Where(x=>
                               ((x.UnloadingPermit!=null && x.UnloadingPermit.UnloadingPermitCode==validFilter.RentPaymentCode || validFilter.ReferenceCode == null) ||
                               (x.LadingExitPermit != null && x.LadingExitPermit.LadingExitPermitCode == validFilter.ReferenceCode || validFilter.ReferenceCode == null)) &&
                               ((x.UnloadingPermit != null && x.UnloadingPermit.DriverName.Contains(validFilter.DriverName)) ||
                               (x.LadingExitPermit != null && x.LadingExitPermit.LadingPermit.CargoAnnounce.DriverName.Contains(validFilter.DriverName)))
                               ))
                        .ThenInclude(x => x.UnloadingPermit)
                .Include(r => r.RentPaymentDetails)
                    .ThenInclude(r => r.LadingExitPermit)
                        .ThenInclude(x => x.LadingPermit)
                            .ThenInclude(x => x.CargoAnnounce)
                .AsSplitQuery()
                .Where(x =>
                x.IsActive &&
                (validFilter.RentPaymentCode == x.Id || validFilter.RentPaymentCode == null) &&
                (
                (x.Created >= validFilter.FromDate.ToDateTime("00:00") || string.IsNullOrEmpty(validFilter.FromDate)) &&
                (x.Created <= validFilter.ToDate.ToDateTime("00:00") || string.IsNullOrEmpty(validFilter.ToDate)) &&
                string.IsNullOrEmpty(validFilter.DriverName))
                ).ToListAsync();
        }

        public async Task<Tuple<List<LadingExitPermit>?, List<UnloadingPermit>?>> GetAllRentsAsync(
            GetAllRentsToPaymentParameter validParams)
        {
            var ladingExitPermits = await _ladingExitPermits
                .Include(x => x.LadingPermit).ThenInclude(x => x.CargoAnnounce)
                .Include(x => x.FareAmountStatus)
                .Include(c => c.ApplicationUser)
                .Include(x => x.LadingExitPermitDetails)
                .Where(x =>
                (x.FareAmountStatusId == (int?)validParams.FareAmountStatusId || validParams.FareAmountStatusId == null) &&
                (x.LadingPermit.CargoAnnounce.Order.FarePaymentTypeId == (int)EFarePaymentType.FareByOurselves) &&
                x.IsActive && //x.FareAmountStatusId==(int?)EFareAmountStatus.InProgress) &&
                (x.LadingPermit.CargoAnnounce != null && x.LadingPermit.CargoAnnounce.DriverName.Contains(validParams.DriverName) || string.IsNullOrEmpty(validParams.DriverName)) &&
                (x.LadingPermit.CargoAnnounce != null && x.LadingPermit.CargoAnnounce.DriverMobile.Contains(validParams.DriverMobile) || string.IsNullOrEmpty(validParams.DriverMobile)) &&
                (x.LadingExitPermitCode == validParams.ReferenceCode || validParams.ReferenceCode == null) &&
                (x.Created >= validParams.FromDate.ToDateTime("00:00") || string.IsNullOrEmpty(validParams.FromDate)) &&
                (x.Created <= validParams.ToDate.ToDateTime("00:00") || string.IsNullOrEmpty(validParams.ToDate)) &&
                (validParams.OrderType == OrderClassType.Sale || validParams.OrderType == null))
                .AsSplitQuery()
                .ToListAsync();

            var purOrdTransRemitUnloads =
                await _purOrdTransRemitUnload
                .Include(c => c.ApplicationUser)
                .Include(x => x.FareAmountStatus)
                .Include(m => m.UnloadingPermitDetails)
                .Include(m => m.EntrancePermit)
                .Where(x =>
                (x.FareAmountStatusId == (int?)validParams.FareAmountStatusId || validParams.FareAmountStatusId == null) &&
                (x.EntrancePermit.TransferRemittance.PurchaseOrder.FarePaymentTypeId == (int)EFarePaymentType.FareByOurselves) &&
                x.IsActive && //x.FareAmountStatusId==(int)EFareAmountStatus.InProgress) &&
                (x.DriverName.Contains(validParams.DriverName) || string.IsNullOrEmpty(validParams.DriverName)) &&
                (x.DriverMobile.Contains(validParams.DriverMobile) || string.IsNullOrEmpty(validParams.DriverMobile)) &&
                (x.UnloadingPermitCode == validParams.ReferenceCode || validParams.ReferenceCode == null) &&
                (x.Created >= validParams.FromDate.ToDateTime("00:00") || string.IsNullOrEmpty(validParams.FromDate)) &&
                (x.Created <= validParams.ToDate.ToDateTime("00:00") || string.IsNullOrEmpty(validParams.ToDate)) &&
                (validParams.OrderType == OrderClassType.Purchase || validParams.OrderType == null))
                .AsSplitQuery()
                .ToListAsync();

            return new Tuple<List<LadingExitPermit>?, List<UnloadingPermit>?>
                (ladingExitPermits, purOrdTransRemitUnloads);
        }

        public async Task<RentPayment?> GetRentPaymentInfo(int RentPaymentId)
        {
            return await _rentPayments
                .FirstOrDefaultAsync(p => p.Id == RentPaymentId);
        }
    }
}