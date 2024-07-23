using Microsoft.EntityFrameworkCore;
using Sepehr.Application.Exceptions;
using Sepehr.Application.Features.LadingPermits.Queries.GetAllLadingPermits;
using Sepehr.Application.Interfaces.Repositories;
using Sepehr.Domain.Entities;
using Sepehr.Infrastructure.Persistence.Context;
using Sepehr.Infrastructure.Persistence.Seeds;

namespace Sepehr.Infrastructure.Persistence.Repositories
{
    public class LadingPermitRepositoryAsync : GenericRepositoryAsync<LadingPermit>, ILadingPermitRepositoryAsync
    {
        private readonly DbSet<LadingPermit> _ladingPermits;
        private readonly ApplicationDbContext _dbContext;

        public LadingPermitRepositoryAsync(ApplicationDbContext dbContext) : base(dbContext)
        {
            _ladingPermits = dbContext.Set<LadingPermit>();
            _dbContext = dbContext;
        }

        public async Task<bool> AttachFiles(ICollection<Attachment>? attachments, int Id)
        {
            var ladingPermit = await _ladingPermits
                .Where(l => l.IsActive)
                .AsNoTracking().FirstOrDefaultAsync(l => l.Id == Id);
            if (ladingPermit == null)
                throw new ApiException("مجوز بارگیری یافت نشد !");

            foreach (var item in attachments)
            {
                ladingPermit.Attachments.Add(item);
            }

            _dbContext.Attach(ladingPermit);
            return true;
        }

        public async Task<IQueryable<LadingPermit>> GetAllLadingPermits(GetAllLadingPermitsParameter filter)
        {
            return _ladingPermits
                .Where(l => l.IsActive)
                .Include(c => c.ApplicationUser)
                .Include(l => l.CargoAnnounce).ThenInclude(l => l.VehicleType)
                .Include(l => l.CargoAnnounce).ThenInclude(l => l.Order)
                .Where(l => l.HasExitPermit == filter.HasExitPermit || filter.HasExitPermit == null)
                .OrderByDescending(p => p.Id).AsQueryable();
        }

        public async Task<LadingPermit?> GetLadingPermitInfo(string desc)
        {
            return await _ladingPermits
                .Where(l => l.IsActive)
                .Include(c => c.ApplicationUser)
                .Include(l => l.CargoAnnounce).ThenInclude(l => l.VehicleType)
                .Include(l => l.CargoAnnounce).ThenInclude(l => l.Order)
                .FirstOrDefaultAsync(p => p.Description == desc);
        }

        public async Task<LadingPermit?> GetLadingPermitInfo(int Id)
        {
            return await _ladingPermits
                .Where(l => l.IsActive)
                .Include(c => c.ApplicationUser)
                .Include(l => l.CargoAnnounce).ThenInclude(l => l.VehicleType)
                .Include(l => l.CargoAnnounce).ThenInclude(l => l.Order)
                .FirstOrDefaultAsync(p => p.Id == Id);
        }

    }
}