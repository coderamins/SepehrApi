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

        public PaymentRequestRepositoryAsync(ApplicationDbContext dbContext) : base(dbContext)
        {
            _purOrdTransRemitUnload = dbContext.Set<UnloadingPermit>();
            _ladingExitPermits = dbContext.Set<LadingExitPermit>();
            _paymentRequests = dbContext.Set<PaymentRequest>();
        }

        public async Task<IEnumerable<PaymentRequest>> GetAllPaymentRequestsAsync(GetAllPaymentRequestsParameter validFilter)
        {
            return
                await _paymentRequests
                .Include(x=>x.PaymentRequestReason)
                .Include(x => x.PaymentRequestStatus)
                .Include(x => x.Customer)
                .Include(x=>x.Approver)
                .Include(x=>x.ApplicationUser)
                .Include(x=>x.Bank)
                .Where(x=>x.PaymentRequestCode==validFilter.PaymentRequestCoode || validFilter.PaymentRequestCoode==null)
                .ToListAsync();
        }

        public async Task<PaymentRequest?> GetPaymentRequestInfo(Guid PaymentRequestId)
        {
            return await _paymentRequests
                .Include(x => x.PaymentRequestReason)
                .Include(x => x.PaymentRequestStatus)
                .Include(x => x.Approver)
                .Include(x => x.Customer)
                .Include(x => x.ApplicationUser)
                .Include(x => x.Bank)
                .Include(x => x.Attachments)
                .FirstOrDefaultAsync(p => p.Id == PaymentRequestId);
        }
    }
}