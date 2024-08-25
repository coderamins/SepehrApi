using Microsoft.EntityFrameworkCore;
using Sepehr.Application.Exceptions;
using Sepehr.Application.Features.LadingExitPermits.Queries.GetAllLadingExitPermits;
using Sepehr.Application.Interfaces;
using Sepehr.Application.Interfaces.Repositories;
using Sepehr.Domain.Entities;
using Sepehr.Domain.Enums;
using Sepehr.Infrastructure.Persistence.Context;

namespace Sepehr.Infrastructure.Persistence.Repositories
{
    public class LadingExitPermitRepositoryAsync :
    GenericRepositoryAsync<LadingExitPermit>, ILadingExitPermitRepositoryAsync
    {
        private readonly DbSet<LadingPermit> _ladingPermitRepo;
        private readonly DbSet<LadingExitPermit> _productLadingExitPermits;
        private readonly DbSet<ProductInventory> _inventory;
        private readonly DbSet<OfficialWarehoseInventory> _officialInventory;
        private readonly ApplicationDbContext _dbContext;
        private readonly IAuthenticatedUserService _authenticatedUser;

        public LadingExitPermitRepositoryAsync(ApplicationDbContext dbContext
            , IAuthenticatedUserService authenticatedUser) : base(dbContext)
        {
            _ladingPermitRepo = dbContext.Set<LadingPermit>();
            _productLadingExitPermits = dbContext.Set<LadingExitPermit>();
            _inventory = dbContext.Set<ProductInventory>();
            _officialInventory = dbContext.Set<OfficialWarehoseInventory>();
            _dbContext = dbContext;
            _authenticatedUser = authenticatedUser;
        }

        public EFarePaymentType CheckFarePaymentType(Guid id)
        {
            return (EFarePaymentType)_productLadingExitPermits.Include(x => x.LadingPermit).ThenInclude(x => x.CargoAnnounce).ThenInclude(x=>x.Order)
                .First(x => x.Id == id).LadingPermit.CargoAnnounce.Order.FarePaymentTypeId;
        }

        public async Task<LadingExitPermit> CreateLadingExitPermit(LadingExitPermit ladingExitPermit)
        {
            var _ladingPermit = await
                _ladingPermitRepo
                .Include(c => c.CargoAnnounce).ThenInclude(i => i.CargoAnnounceDetails).ThenInclude(c => c.OrderDetail)
                .FirstOrDefaultAsync(o => o.Id == ladingExitPermit.LadingPermitId);

            if (_ladingPermit == null || _ladingPermit.CargoAnnounce == null)
                throw new ApiException("مجوز بارگیری یافت نشد !");

            foreach (var item in ladingExitPermit.LadingExitPermitDetails)
            {
                var cargoAnncDetail = _ladingPermit.CargoAnnounce.CargoAnnounceDetails.First(d => d.Id == item.CargoAnnounceDetailId);
                var inv = _inventory
                          .FirstOrDefault(o => o.ProductBrandId == cargoAnncDetail.OrderDetail.ProductBrandId && 
                                               o.WarehouseId == _dbContext.Set<OrderDetail>().First(d => d.Id == cargoAnncDetail.OrderDetailId).WarehouseId);
                
                var of_inv = _officialInventory
                            .FirstOrDefault(o => o.ProductId == cargoAnncDetail.OrderDetail.ProductId &&
                                                 o.WarehouseId== _dbContext.Set<OrderDetail>().First(d => d.Id == cargoAnncDetail.OrderDetailId).WarehouseId);
                if (inv == null)
                    throw new ApiException("انبار محصول یافت نشد !");

                if (of_inv != null)
                {
                    var FormalInvEntry = _officialInventory.Entry(of_inv);
                    FormalInvEntry.State = EntityState.Modified;

                    of_inv.FloorInventory -= (double)item.RealAmount;

                    FormalInvEntry.CurrentValues.SetValues(of_inv);
                }

                var InvEntry = _inventory.Entry(inv);
                InvEntry.State = EntityState.Modified;                

                inv.FloorInventory -= (double)item.RealAmount;
                //----- مابه التفاوت موجودی واقعی و موجودی تقریبی بروزرسانی می شود ------
                inv.ApproximateInventory += cargoAnncDetail.LadingAmount - item.RealAmount;
                                
                InvEntry.CurrentValues.SetValues(inv);
            }

            var exitPermit = await _productLadingExitPermits.AddAsync(ladingExitPermit);
            _ladingPermit.HasExitPermit = true;
            _ladingPermitRepo.Update(_ladingPermit);
            await _dbContext.SaveChangesAsync();

            return exitPermit.Entity;
        }

        public async Task<IQueryable<LadingExitPermit>> GetAllLadingExitPermits(GetAllLadingExitPermitsParameter validFilter)
        {
            return _productLadingExitPermits
                .Include(t => t.LadingPermit)
                .Include(c => c.ApplicationUser)
                .Include(l => l.LadingExitPermitDetails).ThenInclude(t => t.CargoAnnounceDetail).ThenInclude(t => t.OrderDetail).ThenInclude(t => t.ProductBrand).ThenInclude(t => t.Brand)
                .Include(l => l.LadingExitPermitDetails).ThenInclude(t => t.CargoAnnounceDetail).ThenInclude(t => t.OrderDetail).ThenInclude(t => t.Product).ThenInclude(t => t.ProductMainUnit)
                .Include(l => l.LadingExitPermitDetails).ThenInclude(t => t.CargoAnnounceDetail).ThenInclude(t => t.OrderDetail).ThenInclude(t => t.Product).ThenInclude(t => t.ProductSubUnit)
                .Where(l => l.IsActive)
                .OrderByDescending(p => p.Id).AsQueryable();
        }

        public async Task<LadingExitPermit?> GetLadingExitPermitInfo(Guid LadingExitPermitId)
        {
            return await _productLadingExitPermits
                .Include(t => t.LadingPermit)
                .Include(t => t.Attachments)
                .Include(c => c.ApplicationUser)
                .Include(l => l.LadingExitPermitDetails).ThenInclude(t => t.CargoAnnounceDetail).ThenInclude(t => t.OrderDetail).ThenInclude(t => t.ProductBrand).ThenInclude(t => t.Brand)
                .Include(l => l.LadingExitPermitDetails).ThenInclude(t => t.CargoAnnounceDetail).ThenInclude(t => t.OrderDetail).ThenInclude(t => t.Product).ThenInclude(t => t.ProductMainUnit)
                .Include(l => l.LadingExitPermitDetails).ThenInclude(t => t.CargoAnnounceDetail).ThenInclude(t => t.OrderDetail).ThenInclude(t => t.Product).ThenInclude(t => t.ProductSubUnit)
                .Where(l => l.IsActive)
                .FirstOrDefaultAsync(p => p.Id == LadingExitPermitId);
        }

        public async Task<LadingExitPermit> UpdateLadingExitPermit(LadingExitPermit ladingExitPermit)
        {
            _dbContext.LadingExitPermitDetails.RemoveRange(
                _dbContext.LadingExitPermitDetails
                .Where(d => !ladingExitPermit.LadingExitPermitDetails.Select(e => e.Id).Contains(d.Id)));

            var updateladingExitPermit = _productLadingExitPermits.Update(ladingExitPermit);
            await _dbContext.SaveChangesAsync();
            return updateladingExitPermit.Entity;
        }

    }
}