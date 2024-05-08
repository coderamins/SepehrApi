using Microsoft.EntityFrameworkCore;
using Sepehr.Application.Exceptions;
using Sepehr.Application.Features.LadingPermits.Queries.GetAllLadingPermits;
using Sepehr.Application.Interfaces.Repositories;
using Sepehr.Domain.Entities;
using Sepehr.Infrastructure.Persistence.Context;
using Sepehr.Infrastructure.Persistence.Seeds;

namespace Sepehr.Infrastructure.Persistence.Repositories
{
    public class LadingLicenseRepositoryAsync : GenericRepositoryAsync<LadingLicense>, ILadingLicenseRepositoryAsync
    {
        private readonly DbSet<LadingLicense> _ladingLicenses;
        private readonly ApplicationDbContext _dbContext;

        public LadingLicenseRepositoryAsync(ApplicationDbContext dbContext) : base(dbContext)
        {
            _ladingLicenses = dbContext.Set<LadingLicense>();
            _dbContext = dbContext;
        }

        public async Task<bool> AttachFiles(ICollection<Attachment>? attachments, int Id)
        {
            var ladingLicense = await _ladingLicenses.AsNoTracking().FirstOrDefaultAsync(l => l.Id == Id);
            if (ladingLicense == null)
                throw new ApiException("مجوز بارگیری یافت نشد !");

            foreach (var item in attachments)
            {
                ladingLicense.Attachments.Add(item);
            }

            _dbContext.Attach(ladingLicense);
            return true;
        }

        public async Task<List<LadingLicense>> GetAllLadingLicenses(GetAllLadingPermitsParameter filter)
        {
            return await _ladingLicenses
                .Include(l => l.CargoAnnounce).ThenInclude(l => l.VehicleType)
                .Include(l => l.ProductSubUnit)
                .Include(l => l.CargoAnnounce).ThenInclude(l => l.Order)
                .Include(l => l.LadingLicenseDetails).ThenInclude(l => l.OrderDetail).ThenInclude(l => l.ProductSubUnit)
                .Include(l => l.LadingLicenseDetails).ThenInclude(l => l.OrderDetail).ThenInclude(l => l.Product).ThenInclude(l => l.ProductMainUnit)
                .Where(l => l.HasExitPermit == filter.HasExitPermit || filter.HasExitPermit == null)
                .OrderByDescending(p => p.Id).ToListAsync();
        }

        public async Task<LadingLicense?> GetLadingLicenseInfo(string desc)
        {
            return await _ladingLicenses
                .Include(l => l.CargoAnnounce).ThenInclude(l => l.VehicleType)
                .Include(l => l.ProductSubUnit)
                .Include(l => l.CargoAnnounce).ThenInclude(l => l.Order)
                .Include(l => l.LadingLicenseDetails).ThenInclude(l => l.OrderDetail).ThenInclude(l => l.ProductSubUnit)
                .Include(l => l.LadingLicenseDetails).ThenInclude(l => l.OrderDetail).ThenInclude(l => l.Product).ThenInclude(l => l.ProductMainUnit)
                .FirstOrDefaultAsync(p => p.Description == desc);
        }

        public async Task<LadingLicense?> GetLadingLicenseInfo(int Id)
        {
            return await _ladingLicenses
                .Include(l => l.CargoAnnounce).ThenInclude(l => l.VehicleType)
                .Include(l => l.ProductSubUnit)
                .Include(l => l.CargoAnnounce).ThenInclude(l => l.Order)
                .Include(l => l.LadingLicenseDetails).ThenInclude(l => l.OrderDetail).ThenInclude(l => l.ProductSubUnit)
                .Include(l => l.LadingLicenseDetails).ThenInclude(l => l.OrderDetail).ThenInclude(l => l.Product).ThenInclude(l => l.ProductMainUnit)
                .FirstOrDefaultAsync(p => p.Id == Id);
        }
    }
}