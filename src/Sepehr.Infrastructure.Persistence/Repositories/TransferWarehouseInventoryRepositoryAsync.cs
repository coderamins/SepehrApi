using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Sepehr.Application.Exceptions;
using Sepehr.Application.Features.TransferWarehouseInventories.Queries.GetAllTransferWarehouseInventories;
using Sepehr.Application.Interfaces.Repositories;
using Sepehr.Domain.Entities;
using Sepehr.Infrastructure.Persistence.Context;
using System.Data;

namespace Sepehr.Infrastructure.Persistence.Repositories
{
    public class TransferWarehouseInventoryRepositoryAsync :
        GenericRepositoryAsync<TransferWarehouseInventory>,
        ITransferWarehouseInventoryRepositoryAsync
    {
        private readonly DbSet<ProductInventory> _productInventory;
        private readonly DbSet<TransferWarehouseInventory> _transferWarehouseInventorys;
        private readonly DbSet<TransferWarehouseInventoryDetail> _transferWarehouseInventoryDetails;
        private readonly DbSet<EntrancePermit> _transRemittEntrancePermits;
        private readonly DbSet<UnloadingPermit> _orderTransferWarehouseInventoryUnloadingPermits;
        private readonly ApplicationDbContext _dbContext;
        private readonly IMapper _mapper;

        public TransferWarehouseInventoryRepositoryAsync(
            ApplicationDbContext dbContext,
            IMapper mapper
            ) : base(dbContext)
        {
            _transRemittEntrancePermits = dbContext.Set<EntrancePermit>();
            _transferWarehouseInventorys = dbContext.Set<TransferWarehouseInventory>();
            _transferWarehouseInventoryDetails = dbContext.Set<TransferWarehouseInventoryDetail>();
            _orderTransferWarehouseInventoryUnloadingPermits = dbContext.Set<UnloadingPermit>();
            _dbContext = dbContext;
            _mapper = mapper;
            _productInventory = dbContext.Set<ProductInventory>();
        }

        public async Task<TransferWarehouseInventory> CreateTransferWarehouseInventory(TransferWarehouseInventory transRemittance)
        {
            foreach (var item in transRemittance.Details)
            {
                var _originWarehouse = await _productInventory.AsNoTracking().FirstOrDefaultAsync(w =>
                                    w.WarehouseId == transRemittance.OriginWarehouseId
                                    && w.ProductBrandId == item.ProductBrandId);
                if (_originWarehouse == null)
                    throw new ApiException(string.Format("موجودی خرید برای کالا برند {0} یافت نشد !",
                        _dbContext.Set<ProductBrand>().AsNoTracking().First(b => b.Id == item.ProductBrandId).Brand.Name));

                var _destWarehouse = await _productInventory.FirstOrDefaultAsync(w =>
                                    w.WarehouseId == transRemittance.DestinationWarehouseId
                                    && w.ProductBrandId == item.ProductBrandId);

                //----موجودی خرید انبار مبدا کم می شود
                _originWarehouse.PurchaseInventory -= item.TransferAmount;
                _productInventory.Update(_originWarehouse);

                //----موجودی در راه انبار مقصد زیاد می شود
                if (_destWarehouse == null)
                {
                    await _dbContext.ProductInventories
                        .AddAsync(new ProductInventory
                        {
                            OnTransitInventory = item.TransferAmount,
                            ProductBrandId = item.ProductBrandId,
                            WarehouseId = transRemittance.OriginWarehouseId,
                            IsActive = true,
                        });
                }
                else
                {
                    _destWarehouse.OnTransitInventory += item.TransferAmount;
                    _productInventory.Update(_destWarehouse);
                }
            }

            var transRemitt = await _transferWarehouseInventorys.AddAsync(transRemittance);
            await _dbContext.SaveChangesAsync();

            return transRemittance;
        }

        public async Task<IEnumerable<TransferWarehouseInventory>> GetAllTransferWarehouseInventoriesAsync(GetAllTransferWarehouseInventoriesParameter validFilter)
        {
            return await _dbContext.TransferWarehouseInventories
                .Include(c => c.ApplicationUser)
                .Include(t => t.TransferWarehouseInventoryType)
                .Include(t => t.VehicleType)
                .Include(t => t.DestinationWarehouse)
                .Include(t => t.OriginWarehouse)
                .Include(t => t.Details).ThenInclude(d => d.UnloadingPermitDetail)
                .Include(t => t.EntrancePermit).ThenInclude(t => t.UnloadingPermits)
                .Include(t => t.TransferWarehouseInventoryStatus)
                .Include(t => t.Details).ThenInclude(d => d.ProductBrand).ThenInclude(b => b.Product).ThenInclude(t => t.ProductSubUnit)
                .Include(t => t.Details).ThenInclude(d => d.ProductBrand).ThenInclude(b => b.Product).ThenInclude(t => t.ProductMainUnit)
                .Include(t => t.Details).ThenInclude(d => d.ProductBrand).ThenInclude(b => b.Brand)
                .Include(t => t.Details).ThenInclude(d => d.ProductBrand).ThenInclude(b => b.Product)
                .Where(t =>
                    (t.OriginWarehouse.CustomerWarehouses.Any(cw=>cw.CustomerId==validFilter.MarketerId) || validFilter.MarketerId==null) &&
                    (t.OriginWarehouseId == validFilter.OriginWarehouseId || validFilter.OriginWarehouseId==null) &&
                    (t.Id == validFilter.Id || validFilter.Id == null) &&
                    (t.TransferWarehouseInventoryStatusId == validFilter.TransferRemittStatusId || validFilter.TransferRemittStatusId == null) &&
                    ((t.TransferWarehouseInventoryStatusId == 2 && validFilter.IsEntranced == true) ||
                    (t.TransferWarehouseInventoryStatusId != 2 && validFilter.IsEntranced == false) || validFilter.IsEntranced == null) &&
                    (t.DestinationWarehouseId == validFilter.DestinationWarehouseId || validFilter.DestinationWarehouseId == null) &&
                    (t.EntrancePermit != null &&
                    t.EntrancePermit.PermitCode == validFilter.TransferEntransePermitNo
                    || validFilter.TransferEntransePermitNo == null)).ToListAsync();
        }

        public async Task<TransferWarehouseInventory> UpdateTransferWarehouseInventory(TransferWarehouseInventory transRemittance)
        {
            try
            {
                var deletedDetails = _transferWarehouseInventoryDetails
                            .Where(s => s.TransferWarehouseInventoryId == transRemittance.Id &&
                            !transRemittance.Details.Select(d => d.Id).Contains(s.Id));

                _transferWarehouseInventoryDetails.RemoveRange(deletedDetails);

                //--------مواردی که حذف شده اند باید موجودی انبار آنها برگشت داده شود به حالت قبلی-------
                foreach (var item in deletedDetails.ToList())
                {
                    var _delOriginWarehouseInv = await _productInventory.AsNoTracking().FirstOrDefaultAsync(w =>
                        w.WarehouseId == transRemittance.OriginWarehouseId
                        && w.ProductBrandId == item.ProductBrandId);

                    if (_delOriginWarehouseInv == null)
                        throw new ApiException("انبار مبدا یافت نشد !");

                    var _destWarehouseInv = await _productInventory.FirstOrDefaultAsync(w =>
                                        w.WarehouseId == transRemittance.DestinationWarehouseId
                                        && w.ProductBrandId == item.ProductBrandId);

                    if (_destWarehouseInv == null)
                        throw new ApiException("انبار مقصد یافت نشد !");


                    //----موجودی خرید انبار مبدا زیاد می شود
                    _delOriginWarehouseInv.PurchaseInventory += item.TransferAmount;
                    _productInventory.Update(_delOriginWarehouseInv);

                    //----موجودی در راه انبار مقصد کم می شود
                    _destWarehouseInv.OnTransitInventory -= item.TransferAmount;
                    _productInventory.Update(_destWarehouseInv);

                }

                foreach (var item in transRemittance.Details)
                {
                    var prevInfo = await _transferWarehouseInventoryDetails.FirstOrDefaultAsync(td =>
                                    td.TransferWarehouseInventoryId == transRemittance.Id &&
                                    td.ProductBrandId == item.ProductBrandId);

                    var _originWarehouseInv = await _productInventory.FirstOrDefaultAsync(w =>
                                        w.WarehouseId == transRemittance.OriginWarehouseId
                                        && w.ProductBrandId == item.ProductBrandId);

                    if (_originWarehouseInv == null)
                        throw new ApiException("انبار مبدا یافت نشد !");

                    var _destWarehouseInv = await _productInventory.FirstOrDefaultAsync(w =>
                                        w.WarehouseId == transRemittance.DestinationWarehouseId
                                        && w.ProductBrandId == item.ProductBrandId);

                    if (_destWarehouseInv == null)
                        throw new ApiException("انبار مقصد یافت نشد !");

                    //----موجودی خرید انبار مبدا کم می شود
                    _originWarehouseInv.PurchaseInventory = _originWarehouseInv.PurchaseInventory + (prevInfo == null ? 0 : prevInfo.TransferAmount) - item.TransferAmount;
                    _productInventory.Update(_originWarehouseInv);

                    //----موجودی در راه انبار مقصد زیاد می شود
                    _destWarehouseInv.OnTransitInventory = (_destWarehouseInv.OnTransitInventory - (prevInfo == null ? 0 : prevInfo.TransferAmount)) + item.TransferAmount;
                    _productInventory.Update(_destWarehouseInv);
                }

                _transferWarehouseInventorys.Update(transRemittance);
                await _dbContext.SaveChangesAsync();

                return transRemittance;
            }
            catch (Exception e)
            {

                throw;
            }
        }

        public async Task<TransferWarehouseInventory?> GetTransferWarehouseInventoryByIdAsync(int id)
        {
            return await _dbContext.TransferWarehouseInventories
                .Include(c => c.ApplicationUser)
                .Include(t => t.TransferWarehouseInventoryType)
                .Include(t => t.VehicleType)
                .Include(t => t.DestinationWarehouse)
                .Include(t => t.EntrancePermit).ThenInclude(t => t.Attachments)
                .Include(t => t.Details).ThenInclude(d=>d.UnloadingPermitDetail)
                .Include(t => t.EntrancePermit)
                    .ThenInclude(t => t.UnloadingPermits)
                    .ThenInclude(t => t.Attachments)
                .Include(t => t.OriginWarehouse)
                .Include(t => t.TransferWarehouseInventoryStatus)
                .Include(t => t.Details).ThenInclude(d => d.ProductBrand).ThenInclude(b => b.Brand)
                .Include(t => t.Details).ThenInclude(d => d.ProductBrand).ThenInclude(b => b.Product).ThenInclude(t => t.ProductSubUnit)
                .Include(t => t.Details).ThenInclude(d => d.ProductBrand).ThenInclude(b => b.Product).ThenInclude(t => t.ProductMainUnit)
                .FirstOrDefaultAsync(o => o.Id == id);
        }

        public async Task<TransferWarehouseInventory?> GetTransferWarehouseInventoryByPermitCodeAsync(int PermitCode)
        {
            return await _dbContext.TransferWarehouseInventories
                .Include(c => c.ApplicationUser)
                .Include(t => t.TransferWarehouseInventoryType)
                .Include(t => t.VehicleType)
                .Include(t => t.DestinationWarehouse)
                .Include(t => t.EntrancePermit).ThenInclude(t => t.Attachments)
                .Include(t => t.Details).ThenInclude(d => d.UnloadingPermitDetail)
                .Include(t => t.EntrancePermit)
                    .ThenInclude(t => t.UnloadingPermits)
                    .ThenInclude(t => t.Attachments)
                .Include(t => t.OriginWarehouse)
                .Include(t => t.TransferWarehouseInventoryStatus)
                .Include(t => t.Details).ThenInclude(d => d.ProductBrand).ThenInclude(b => b.Brand)
                .Include(t => t.Details).ThenInclude(d => d.ProductBrand).ThenInclude(b => b.Product).ThenInclude(t => t.ProductSubUnit)
                .Include(t => t.Details).ThenInclude(d => d.ProductBrand).ThenInclude(b => b.Product).ThenInclude(t => t.ProductMainUnit)
                .FirstOrDefaultAsync(o => o.EntrancePermit.PermitCode == PermitCode);
        }

        public async Task<EntrancePermit> TransferWarehouseInventoryEntrancePermission(
            TransferWarehouseInventoryEntrancePermissionCommand entrancePermit)
        {
            try
            {
                var transRemit = await _dbContext.TransferWarehouseInventories
                       .AsNoTracking()
                       .FirstOrDefaultAsync(o => o.Id == entrancePermit.TransferWarehouseInventoryId);

                if (transRemit.TransferWarehouseInventoryStatusId == 2)
                    throw new ApiException("مجوز ورود حواله قبلا ثبت شده است !");
                if (transRemit == null)
                    throw new ApiException("حواله یافت نشد !");

                transRemit.TransferWarehouseInventoryStatusId = 2;
                _transferWarehouseInventorys.Update(transRemit);

                var newEntrancePermit = _mapper.Map<EntrancePermit>(entrancePermit);
                //var mappedTransRemittEntrancePermits = new EntrancePermit
                //{
                //    TransferWarehouseInventory = transRemit,
                //    TransferWarehouseInventoryId = id,
                //    IsActive = true,
                //};
                var transRemitEntrancePermit = await _transRemittEntrancePermits.AddAsync(newEntrancePermit);

                await _dbContext.SaveChangesAsync();

                return transRemitEntrancePermit.Entity;
            }
            catch (Exception e)
            {

                throw;
            }
        }

        public async Task<EntrancePermit> PurchaseOrderTransRemittEntrancePermitById
            (Guid purchaseOrderTransferWarehouseInventoryEntrancePermitId)
        {
            var transferPermit = await _transferWarehouseInventorys
                .Include(t => t.EntrancePermit)
                .FirstOrDefaultAsync(o =>
                        o.EntrancePermit.Id == purchaseOrderTransferWarehouseInventoryEntrancePermitId);

            return transferPermit.EntrancePermit;
        }

        public async Task<UnloadingPermit> CreatePOrderUnloadingPermit
            (UnloadingPermit unloadingPermit)
        {
            try
            {
                var transferRemitt = await _transferWarehouseInventorys.AsNoTracking()
                        .Include(t => t.Details).ThenInclude(d=>d.UnloadingPermitDetail).AsNoTracking()
                        .Include(t => t.EntrancePermit).AsNoTracking()
                        .FirstOrDefaultAsync(t =>
                        t.EntrancePermit.Id == unloadingPermit.EntrancePermitId);

                if (transferRemitt == null)
                    throw new ApiException("حواله انتقال یافت نشد !");
                if (transferRemitt.TransferWarehouseInventoryStatusId >= 3)
                    throw new ApiException("تخلیه حواله قبلا بصورت کامل انجام شده !");

                transferRemitt = _mapper.Map(unloadingPermit, transferRemitt);

                #region اگر مجموع اقلام انتقال داده شده با مجموع اقلام خروج خورده یکسان باشد وضعیت حواله انتقال به تخلیه شده تبدیل خواهد شد

                var calcUnloadedSum = await
                    _orderTransferWarehouseInventoryUnloadingPermits.AsNoTracking()
                    .Include(t => t.UnloadingPermitDetails).AsNoTracking()
                    .Where(t => t.EntrancePermitId == unloadingPermit.EntrancePermitId)
                    .FirstOrDefaultAsync();

                var transPermitAmounts = transferRemitt.Details.Sum(d => d.TransferAmount);
                var unloadedAmounts = (calcUnloadedSum == null ? 0 :
                    calcUnloadedSum.UnloadingPermitDetails.Sum(f => f.UnloadedAmount)) +
                    unloadingPermit.UnloadingPermitDetails.Sum(t => t.UnloadedAmount);

                if (transPermitAmounts >= unloadedAmounts)
                    transferRemitt.TransferWarehouseInventoryStatusId = 3;//---تخلیه شده
                #endregion

                #region موجودی کف (واقعی) محصول اضافه می شود 
                foreach (var item in transferRemitt.Details)
                {
                    foreach(var udetail in item.UnloadingPermitDetail)
                    {
                        var inv = await _productInventory
                            .FirstOrDefaultAsync(i => i.ProductBrandId == udetail.TransferWarehouseInventoryDetail.ProductBrandId &&
                            i.WarehouseId == udetail.TransferWarehouseInventoryDetail.TransferWarehouseInventory.DestinationWarehouseId);
                        
                        if (inv == null) {
                            _productInventory.Add(new ProductInventory
                            {
                                ProductBrandId = udetail.TransferWarehouseInventoryDetail.ProductBrandId,
                                WarehouseId = udetail.TransferWarehouseInventoryDetail.TransferWarehouseInventory.DestinationWarehouseId,
                                ApproximateInventory = 0,
                                FloorInventory = (double)udetail.UnloadedAmount,
                                IsActive = true,
                                //CreatedBy = Guid.Parse(_userService.UserId),
                            });

                        }
                        else
                        {
                            inv.FloorInventory += (double)udetail.UnloadedAmount;
                            _productInventory.Update(inv);
                        }
                    }
                }
                #endregion

                _transferWarehouseInventorys.Update(transferRemitt);

                var UnloadingPermit = await _orderTransferWarehouseInventoryUnloadingPermits
                    .AddAsync(unloadingPermit);

                await _dbContext.SaveChangesAsync();
                return UnloadingPermit.Entity;

            }
            catch (Exception e)
            {
                throw;
            }
        }
    }
}