using Microsoft.EntityFrameworkCore;
using Sepehr.Application.Features.PaymentRequests.Queries.GetAllPaymentRequests;
using Sepehr.Application.Interfaces.Repositories;
using Sepehr.Domain;
using Sepehr.Domain.Entities;
using Sepehr.Domain.Enums;
using Sepehr.Infrastructure.Persistence.Context;

namespace Sepehr.Infrastructure.Persistence.Repositories
{
    public class PaymentRequestRepositoryAsync : GenericRepositoryAsync<PaymentRequest>, IPaymentRequestRepositoryAsync
    {
        private readonly DbSet<PaymentRequest> _paymentRequests;
        private readonly DbSet<LadingExitPermit> _ladingExitPermits;
        private readonly DbSet<UnloadingPermit> _purOrdTransRemitUnload;
        private readonly ApplicationDbContext _dbContext;

        public PaymentRequestRepositoryAsync(ApplicationDbContext dbContext) : base(dbContext)
        {
            _purOrdTransRemitUnload = dbContext.Set<UnloadingPermit>();
            _ladingExitPermits = dbContext.Set<LadingExitPermit>();
            _paymentRequests = dbContext.Set<PaymentRequest>();
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<PaymentRequest>> GetAllPaymentRequestsAsync(GetAllPaymentRequestsParameter validFilter)
        {
            return
                await _paymentRequests
                .Include(x => x.PaymentRequestStatus)
                .Include(x => x.Customer)
                .Include(x => x.Approver)
                .Include(x => x.ApplicationUser)
                .Include(x => x.PaymentFromIncome)
                .Include(x => x.Approver)
                .Include(x => x.PaymentFromCashDesk)
                .Include(x =>x.PaymentFromCustomer)
                .Include(x =>x.PaymentFromOrganizationBank).ThenInclude(x=>x.Bank)
                .Include(x =>x.PaymentFromCashDesk)
                .Include(x =>x.PaymentFromIncome)
                .Include(x =>x.PaymentFromPettyCash)
                .Include(x =>x.PaymentFromCost)
                .Include(x =>x.PaymentFromShareHolder)

                .Where(x => x.PaymentRequestCode == validFilter.PaymentRequestCoode || validFilter.PaymentRequestCoode == null)
                .ToListAsync();
        }

        public async Task<PaymentRequest?> GetPaymentRequestInfo(Guid PaymentRequestId)
        {
            return await _paymentRequests
                .Include(x => x.PaymentRequestStatus)
                .Include(x => x.Customer)
                .Include(x => x.Approver)
                .Include(x => x.ApplicationUser)
                .Include(x => x.PaymentFromIncome)
                .Include(x => x.Approver)
                .Include(x => x.PaymentFromCashDesk)
                .Include(x => x.PaymentFromCustomer)
                .Include(x => x.PaymentFromOrganizationBank).ThenInclude(x => x.Bank)
                .Include(x => x.PaymentFromCashDesk)
                .Include(x => x.PaymentFromIncome)
                .Include(x => x.PaymentFromPettyCash)
                .Include(x => x.PaymentFromCost)
                .Include(x => x.PaymentFromShareHolder)
                .Include(x => x.Attachments)
                .FirstOrDefaultAsync(p => p.Id == PaymentRequestId);
        }

        public async Task ApproveAsync(PaymentRequest paymentRequest)
        {
            var pReq = await _paymentRequests.FirstAsync(x => x.Id == paymentRequest.Id);
           
            paymentRequest.PaymentRequestStatusId = (int)EPaymentRequestStatus.Approved;

            _paymentRequests.Entry(pReq).State = EntityState.Modified;
            _paymentRequests.Entry(pReq).CurrentValues.SetValues(paymentRequest);

            await _dbContext.SaveChangesAsync();
        }

        public async Task ProceedPaymentAsync(PaymentRequest paymentRequest)
        {
            var pReq = await _paymentRequests.FirstAsync(x => x.Id == paymentRequest.Id);

            paymentRequest.PaymentRequestStatusId = (int)EPaymentRequestStatus.Payed;

            _paymentRequests.Entry(pReq).State = EntityState.Modified;
            _paymentRequests.Entry(pReq).CurrentValues.SetValues(paymentRequest);

            await _dbContext.SaveChangesAsync();
        }

        public async Task RejectAsync(PaymentRequest paymentRequest)
        {
            var pReq = await _paymentRequests.FirstAsync(x => x.Id == paymentRequest.Id);

            paymentRequest.PaymentRequestStatusId = (int)EPaymentRequestStatus.Rejected;

            _paymentRequests.Entry(pReq).State = EntityState.Modified;
            _paymentRequests.Entry(pReq).CurrentValues.SetValues(paymentRequest);

            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdatePaymentRequestAsync(PaymentRequest paymentRequest)
        {
            var pReq = await _paymentRequests.FirstAsync(x => x.Id == paymentRequest.Id);

            _paymentRequests.Entry(pReq).State = EntityState.Modified;
            _paymentRequests.Entry(pReq).CurrentValues.SetValues(paymentRequest);

            await _dbContext.SaveChangesAsync();
        }
    }
}