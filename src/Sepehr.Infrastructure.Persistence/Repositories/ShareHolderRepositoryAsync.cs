using Microsoft.EntityFrameworkCore;
using Sepehr.Application.Interfaces.Repositories;
using Sepehr.Domain.Entities;
using Sepehr.Infrastructure.Persistence.Context;

namespace Sepehr.Infrastructure.Persistence.Repositories
{
    public class ShareHolderRepositoryAsync : GenericRepositoryAsync<ShareHolder>, IShareHolderRepositoryAsync
    {
        private readonly DbSet<ShareHolder> _productShareHolders;

        public ShareHolderRepositoryAsync(ApplicationDbContext dbContext) : base(dbContext)
        {
            _productShareHolders = dbContext.Set<ShareHolder>();
        }

        public async Task<List<ShareHolder>> GetAllProductShareHolders()
        {
            return await _productShareHolders
                .OrderByDescending(p => p.Id).ToListAsync();
        }


        public async Task<ShareHolder?> GetShareHolderInfo(int ShareHolderCode)
        {
            return await _productShareHolders
                .FirstOrDefaultAsync(p => p.ShareHolderCode == ShareHolderCode);
        }
    }
}