using Microsoft.EntityFrameworkCore;
using Sepehr.Application.Features.LadingExitPermits.Queries.GetAllLadingExitPermits;
using Sepehr.Application.Interfaces.Repositories;
using Sepehr.Domain.Entities;
using Sepehr.Infrastructure.Persistence.Context;

namespace Sepehr.Infrastructure.Persistence.Repositories
{
    public class LadingExitPermitRepositoryAsync : 
    GenericRepositoryAsync<LadingExitPermit>, ILadingExitPermitRepositoryAsync
    {
        private readonly DbSet<LadingExitPermit> _productLadingExitPermits;
        private readonly ApplicationDbContext _dbContext;

        public LadingExitPermitRepositoryAsync(ApplicationDbContext dbContext) : base(dbContext)
        {
            _productLadingExitPermits = dbContext.Set<LadingExitPermit>();
            _dbContext=dbContext;
        }

        public async Task<LadingExitPermit> CreateLadingExitPermit(LadingExitPermit ladingExitPermit)
        {
            var _ladingExitPermit =await 
                _productLadingExitPermits
                .Include(e=>e.LadingExitPermitDetails).ThenInclude(c=>c.CargoAnnounceDetail).ThenInclude(c=>c.OrderDetail)
                .FirstAsync(o => o.Id == ladingExitPermit.Id);
            

            foreach(var item in _ladingExitPermit.LadingExitPermitDetails)
            {
                var inv = _dbContext.ProductInventories.FirstOrDefault(o => o.ProductBrandId == item.CargoAnnounceDetail.OrderDetail.ProductBrandId);
                //inv. item.RealAmount;
            }

            throw new NotImplementedException();
        }

        public async Task<IQueryable<LadingExitPermit>> GetAllLadingExitPermits(GetAllLadingExitPermitsParameter validFilter)
        {
            return _productLadingExitPermits
                .Include(t => t.LadingPermit)
                .Include(l => l.LadingExitPermitDetails).ThenInclude(t => t.CargoAnnounceDetail).ThenInclude(t => t.OrderDetail).ThenInclude(t => t.ProductBrand).ThenInclude(t => t.Brand)
                .Include(l => l.LadingExitPermitDetails).ThenInclude(t => t.CargoAnnounceDetail).ThenInclude(t => t.OrderDetail).ThenInclude(t => t.Product).ThenInclude(t => t.ProductMainUnit)
                .Include(l => l.LadingExitPermitDetails).ThenInclude(t => t.CargoAnnounceDetail).ThenInclude(t => t.OrderDetail).ThenInclude(t => t.Product).ThenInclude(t => t.ProductSubUnit)
                .Where(l=>l.IsActive)
                .OrderByDescending(p => p.Id).AsQueryable();
        }


        public async Task<LadingExitPermit?> GetLadingExitPermitInfo(Guid LadingExitPermitId)
        {
            return await _productLadingExitPermits
                .Include(t => t.LadingPermit)
                .Include(l => l.LadingExitPermitDetails).ThenInclude(t => t.CargoAnnounceDetail).ThenInclude(t => t.OrderDetail).ThenInclude(t => t.ProductBrand).ThenInclude(t => t.Brand)
                .Include(l => l.LadingExitPermitDetails).ThenInclude(t => t.CargoAnnounceDetail).ThenInclude(t => t.OrderDetail).ThenInclude(t => t.Product).ThenInclude(t => t.ProductMainUnit)
                .Include(l => l.LadingExitPermitDetails).ThenInclude(t => t.CargoAnnounceDetail).ThenInclude(t => t.OrderDetail).ThenInclude(t => t.Product).ThenInclude(t => t.ProductSubUnit)
                .Where(l=>l.IsActive)
                .FirstOrDefaultAsync(p => p.Id == LadingExitPermitId);
        }

        public async Task<LadingExitPermit> UpdateLadingExitPermit(LadingExitPermit ladingExitPermit)
        {
            _dbContext.LadingExitPermitDetails.RemoveRange(
                _dbContext.LadingExitPermitDetails
                .Where(d => !ladingExitPermit.LadingExitPermitDetails.Select(e => e.Id).Contains(d.Id)));

            var updateladingExitPermit= _productLadingExitPermits.Update(ladingExitPermit);

            return updateladingExitPermit.Entity;
        }
    }
}