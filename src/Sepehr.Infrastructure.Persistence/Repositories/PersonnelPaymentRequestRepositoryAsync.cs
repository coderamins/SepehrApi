using Microsoft.EntityFrameworkCore;
using Sepehr.Application.Features.PersonnelPaymentRequests.Queries.GetAllPersonnelPaymentRequests;
using Sepehr.Application.Interfaces.Repositories;
using Sepehr.Domain;
using Sepehr.Domain.Entities;
using Sepehr.Domain.Enums;
using Sepehr.Infrastructure.Persistence.Context;

namespace Sepehr.Infrastructure.Persistence.Repositories
{
    public class PersonnelPaymentRequestRepositoryAsync : GenericRepositoryAsync<PersonnelPaymentRequest>, IPersonnelPaymentRequestRepositoryAsync
    {
        private readonly DbSet<PersonnelPaymentRequest> _personnelPaymentRequests;
        private readonly DbSet<LadingExitPermit> _ladingExitPermits;
        private readonly DbSet<UnloadingPermit> _purOrdTransRemitUnload;
        private readonly ApplicationDbContext _dbContext;

        public PersonnelPaymentRequestRepositoryAsync(ApplicationDbContext dbContext) : base(dbContext)
        {
            _purOrdTransRemitUnload = dbContext.Set<UnloadingPermit>();
            _ladingExitPermits = dbContext.Set<LadingExitPermit>();
            _personnelPaymentRequests = dbContext.Set<PersonnelPaymentRequest>();
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<PersonnelPaymentRequest>> GetAllPersonnelPaymentRequestsAsync(GetAllPersonnelPaymentRequestsParameter validFilter)
        {
            return
                await _personnelPaymentRequests
                .Include(x => x.PaymentRequestStatus)
                .Include(x => x.Personnel)
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
                .Where(x=>x.PaymentRequestCode==validFilter.PaymentRequestCoode || validFilter.PaymentRequestCoode==null)
                .ToListAsync();
        }

        public async Task<PersonnelPaymentRequest?> GetPersonnelPaymentRequestInfo(Guid PersonnelPaymentRequestId)
        {
            return await _personnelPaymentRequests
                .Include(x => x.PaymentRequestStatus)
                .Include(x => x.Personnel)
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
                .FirstOrDefaultAsync(p => p.Id == PersonnelPaymentRequestId);
        }

        public async Task ApproveAsync(PersonnelPaymentRequest paymentRequest)
        {
            var pReq = await _personnelPaymentRequests.FirstAsync(x => x.Id == paymentRequest.Id);
            pReq.PaymentRequestStatusId = (int)EPaymentRequestStatus.Approved;

            _personnelPaymentRequests.Entry(pReq).State = EntityState.Modified;
            _personnelPaymentRequests.Entry(pReq).CurrentValues.SetValues(paymentRequest);

            await _dbContext.SaveChangesAsync();
        }

        public async Task ProceedPaymentAsync(PersonnelPaymentRequest paymentRequest)
        {
            var pReq = await _personnelPaymentRequests.FirstAsync(x => x.Id == paymentRequest.Id);
            pReq.PaymentRequestStatusId = (int)EPaymentRequestStatus.Payed;

            _personnelPaymentRequests.Entry(pReq).State = EntityState.Modified;
            _personnelPaymentRequests.Entry(pReq).CurrentValues.SetValues(paymentRequest);

            await _dbContext.SaveChangesAsync();
        }

    }
}