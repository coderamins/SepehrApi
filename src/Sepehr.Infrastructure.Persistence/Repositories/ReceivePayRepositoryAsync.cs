using DocumentFormat.OpenXml.Office2010.Excel;
using Microsoft.EntityFrameworkCore;
using Sepehr.Application.Features.ReceivePays.Queries.GetAllReceivePays;
using Sepehr.Application.Interfaces.Repositories;
using Sepehr.Domain;
using Sepehr.Domain.Entities;
using Sepehr.Domain.Enums;
using Sepehr.Infrastructure.Persistence.Context;

namespace Sepehr.Infrastructure.Persistence.Repositories
{
    public class ReceivePayRepositoryAsync : GenericRepositoryAsync<ReceivePay>, IReceivePayRepositoryAsync
    {
        private readonly DbSet<ReceivePay> _receivePays;
        private readonly DbSet<Attachment> _attachmens;
        private readonly DbContext _context;

        public ReceivePayRepositoryAsync(ApplicationDbContext dbContext) : base(dbContext)
        {
            _receivePays = dbContext.Set<ReceivePay>();
            _attachmens = dbContext.Set<Attachment>();
            _context=dbContext;
        }

        public async Task<List<ReceivePay>> GetAllReceivePays(GetAllReceivePaysParameter filter)
        {
            return await _receivePays
                .Where(r =>
                        (r.ReceivePayStatusId >= filter.StatusId || filter.StatusId == null) &&
                        (r.Created.Date >= filter.FromDate.ToDateTime("00:00").Date || string.IsNullOrEmpty(filter.FromDate)) &&
                        (r.Created.Date <= filter.ToDate.ToDateTime("00:00").Date || string.IsNullOrEmpty(filter.ToDate)) &&
                        ((r.IsAccountingApproval && filter.IsApproved == IsApprovalReceivePay.Approved) ||
                        (!r.IsAccountingApproval && filter.IsApproved == IsApprovalReceivePay.NotApproved) ||
                        (r.AccountingDocNo== filter.AccountingDocNo || filter.AccountingDocNo==null) ||
                        (r.ReceivePayCode== filter.ReceivePayCode || filter.ReceivePayCode == null) ||
                        filter.IsApproved == IsApprovalReceivePay.None))
                .Include(i => i.ReceiveFromCustomer)
                .Include(i => i.ReceiveFromOrganizationBank).ThenInclude(o=>o.Bank)
                .Include(i => i.ReceiveFromCashDesk)
                .Include(i => i.ReceiveFromIncome)
                .Include(i => i.ReceiveFromPettyCash)
                .Include(i => i.ReceiveFromCost)
                .Include(i => i.ReceiveFromShareHolder)
                .Include(i => i.PayToCustomer)
                .Include(i => i.PayToOrganizationBank).ThenInclude(o => o.Bank)
                .Include(i => i.PayToCashDesk)
                .Include(i => i.PayToIncome)
                .Include(i => i.PayToPettyCash)
                .Include(i => i.PayToCost)
                .Include(i => i.ReceiveFromCompany)
                .Include(i => i.PayToCompany)
                .Include(i => i.PayToShareHolder)

                .Include(i => i.PayToCustomer)
                .Include(i => i.ReceivePaymentTypeFrom)
                .Include(i => i.ReceivePaymentTypeTo)
                .Include(i => i.ReceivePayStatus)
                .OrderByDescending(p => p.Created).ToListAsync();
        }

        public async Task<ReceivePay?> GetReceivePayByIdAsync(Guid id)
        {
            return await _receivePays
                .Include(r => r.Attachments)
                .Include(i => i.ReceiveFromCustomer)
                .Include(i => i.PayToCustomer)
                .Include(i => i.ReceivePaymentTypeFrom)
                .Include(i => i.ReceivePaymentTypeTo)
                .Include(i => i.ReceivePayStatus)
                .Include(i => i.ReceiveFromCustomer)
                .Include(i => i.ReceiveFromOrganizationBank).ThenInclude(o => o.Bank)
                .Include(i => i.ReceiveFromCashDesk)
                .Include(i => i.ReceiveFromIncome)
                .Include(i => i.ReceiveFromPettyCash)
                .Include(i => i.ReceiveFromCost)
                .Include(i => i.ReceiveFromShareHolder)
                .Include(i => i.PayToCustomer)
                .Include(i => i.PayToOrganizationBank).ThenInclude(o => o.Bank)
                .Include(i => i.PayToCashDesk)
                .Include(i => i.PayToIncome)
                .Include(i => i.PayToPettyCash)
                .Include(i => i.PayToCost)
                .Include(i => i.ReceiveFromCompany)
                .Include(i => i.PayToCompany)
                .Include(i => i.PayToShareHolder)
                .FirstOrDefaultAsync(r => r.Id == id);
        }

        public async Task<IList<ReceivePay>> GetReceivePays(IEnumerable<Guid> receivePays)
        {
            return await _receivePays
                .Where(r => receivePays.Contains(r.Id)).ToListAsync();
        }

        public async Task<ReceivePay> UpdateReceivePayAsync(ReceivePay receivePay)
        {
            _attachmens.RemoveRange(_attachmens.Where(a => a.ReceivePayId != null));
            if (receivePay.Attachments!=null && receivePay.Attachments.Count > 0)
                await _attachmens.AddRangeAsync(receivePay.Attachments);

            receivePay = _receivePays.Update(receivePay).Entity;
            await _context.SaveChangesAsync();
            return receivePay;
        }
    }
}