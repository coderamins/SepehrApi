using Microsoft.EntityFrameworkCore;
using Sepehr.Application.Features.DraftOrders.Queries.GetAllDraftOrders;
using Sepehr.Application.Interfaces.Repositories;
using Sepehr.Domain;
using Sepehr.Domain.Entities;
using Sepehr.Infrastructure.Persistence.Context;

namespace Sepehr.Infrastructure.Persistence.Repositories
{
    public class DraftOrderRepositoryAsync : GenericRepositoryAsync<DraftOrder>, IDraftOrderRepositoryAsync
    {
        private readonly DbSet<DraftOrder> _draftOrders;

        public DraftOrderRepositoryAsync(ApplicationDbContext dbContext) : base(dbContext)
        {
            _draftOrders = dbContext.Set<DraftOrder>();
        }

        public IQueryable<DraftOrder> GetAllDraftOrders(GetAllDraftOrdersParameter filter)
        {
            return _draftOrders
                .Where(x => 
                      (x.CreatedBy == filter.CreatorId || filter.CreatorId == null || filter.CreatorId == Guid.Empty) &&
                      (x.Converted == filter.Converted || filter.Converted == null) &&
                      (x.Created.Date >= filter.FromDate.ToDateTime("00:00").Date || string.IsNullOrEmpty(filter.FromDate)) &&
                      (x.Created.Date <= filter.ToDate.ToDateTime("00:00").Date || string.IsNullOrEmpty(filter.ToDate)))
                .Include(c => c.ApplicationUser)
                .OrderByDescending(p => p.Created);
        }

        public async Task<DraftOrder?> GetDraftOrderById(Guid id)
        {
            return await _draftOrders
                .Include(c => c.ApplicationUser)
                .Include(c => c.Attachments)
                .OrderByDescending(p => p.Created).FirstOrDefaultAsync(x=>x.Id==id);
        }

    }
}