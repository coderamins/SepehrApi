using Microsoft.EntityFrameworkCore;
using Sepehr.Application.Features.UnloadingPermits.Queries.GetAllUnloadingPermits;
using Sepehr.Application.Interfaces.Repositories;
using Sepehr.Domain.Entities;
using Sepehr.Infrastructure.Persistence.Context;

namespace Sepehr.Infrastructure.Persistence.Repositories
{
    public class UnloadingPermitRepositoryAsync :
        GenericRepositoryAsync<UnloadingPermit>, 
        IUnloadingPermitRepositoryAsync
    {
        private readonly DbSet<UnloadingPermit> _unloadingPermits;

        public UnloadingPermitRepositoryAsync(ApplicationDbContext dbContext) : base(dbContext)
        {
            _unloadingPermits = dbContext.Set<UnloadingPermit>();
        }

        public async Task<IEnumerable<UnloadingPermit>> GetAllUnloadingPermits(GetAllUnloadingPermitsParameter validFilter)
        {
            return _unloadingPermits
                .Include(x=>x.EntrancePermit)
                .Include(x=>x.ApplicationUser)
                .Include(x=>x.UnloadingPermitDetails)
                .Where(x => x.UnloadingPermitCode == validFilter.UnloadingPermitCode || validFilter.UnloadingPermitCode == null);

        }

        public async Task<UnloadingPermit?> GetUnloadingPermitInfo(Guid Id)
        {
            return await _unloadingPermits
                .Include(x => x.EntrancePermit)
                .Include(x => x.ApplicationUser)
                .Include(x => x.UnloadingPermitDetails)
                .Include(x => x.Attachments)
                .FirstOrDefaultAsync(x => x.Id == Id);
        }

        public async Task<UnloadingPermit?> GetUnloadingPermitInfo(int unloadingPermitCode)
        {
            return await _unloadingPermits
                .Include(x => x.EntrancePermit)
                .Include(x => x.ApplicationUser)
                .Include(x => x.UnloadingPermitDetails)
                .Include(x => x.Attachments)
                .FirstOrDefaultAsync(x => x.UnloadingPermitCode == unloadingPermitCode);
        }
    }
}