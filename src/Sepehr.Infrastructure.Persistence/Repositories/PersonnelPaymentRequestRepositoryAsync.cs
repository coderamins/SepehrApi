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

        public PersonnelPaymentRequestRepositoryAsync(ApplicationDbContext dbContext) : base(dbContext)
        {
            _purOrdTransRemitUnload = dbContext.Set<UnloadingPermit>();
            _ladingExitPermits = dbContext.Set<LadingExitPermit>();
            _personnelPaymentRequests = dbContext.Set<PersonnelPaymentRequest>();
        }

        public async Task<IEnumerable<PersonnelPaymentRequest>> GetAllPersonnelPaymentRequestsAsync(GetAllPersonnelPaymentRequestsParameter validFilter)
        {
            return
                await _personnelPaymentRequests
                .Include(x=>x.PaymentRequestReason)
                .Include(x => x.PaymentRequestStatus)
                .Include(x => x.Customer)
                .Include(x=>x.Approver)
                .Include(x=>x.ApplicationUser)
                .Include(x=>x.Bank)
                .Where(x=>x.PaymentRequestCode==validFilter.PaymentRequestCoode || validFilter.PaymentRequestCoode==null)
                .ToListAsync();
        }

        public async Task<PersonnelPaymentRequest?> GetPersonnelPaymentRequestInfo(Guid PersonnelPaymentRequestId)
        {
            return await _personnelPaymentRequests
                .Include(x => x.PaymentRequestReason)
                .Include(x => x.PaymentRequestStatus)
                .Include(x => x.Approver)
                .Include(x => x.Customer)
                .Include(x => x.ApplicationUser)
                .Include(x => x.Bank)
                .Include(x => x.Attachments)
                .FirstOrDefaultAsync(p => p.Id == PersonnelPaymentRequestId);
        }
    }
}