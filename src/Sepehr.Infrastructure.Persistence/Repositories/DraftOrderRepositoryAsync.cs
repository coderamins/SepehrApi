using Microsoft.EntityFrameworkCore;
using Sepehr.Application.Features.DraftOrders.Queries.GetAllDraftOrders;
using Sepehr.Application.Interfaces.Repositories;
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

        public async Task<List<DraftOrder>> GetAllDraftOrders(GetAllDraftOrdersParameter filter)
        {
            return await _draftOrders
                .Where(x => x.Converted == filter.Converted || filter.Converted == null)
                .Include(c => c.ApplicationUser)
                .OrderByDescending(p => p.Created).ToListAsync();
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