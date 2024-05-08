using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Sepehr.Application.Exceptions;
using Sepehr.Application.Features.TransferRemittances.Command.TransferRemittanceEntrancePermission;
using Sepehr.Application.Features.TransferRemittances.Queries.GetAllTransferRemittances;
using Sepehr.Application.Interfaces.Repositories;
using Sepehr.Domain.Entities;
using Sepehr.Infrastructure.Persistence.Context;
using System.Data;

namespace Sepehr.Infrastructure.Persistence.Repositories
{
    public class PurchaseOrderTransferRemittanceRepositoryAsync :
        GenericRepositoryAsync<PurchaseOrderTransferRemittance>,
        IPurchaseOrderTransferRemittanceRepositoryAsync
    {
        private readonly DbSet<ProductInventory> _productInventory;
        private readonly DbSet<PurchaseOrderTransferRemittance> _transferRemittances;
        private readonly DbSet<PurchaseOrderTransferRemittanceDetail> _transferRemittanceDetails;
        private readonly DbSet<PurchaseOrderTransferRemittanceEntrancePermit> _transRemittEntrancePermits;
        private readonly DbSet<PurchaseOrderTransferRemittanceUnloadingPermit> _orderTransferRemittanceUnloadingPermits;
        private readonly ApplicationDbContext _dbContext;
        private readonly IMapper _mapper;

        public PurchaseOrderTransferRemittanceRepositoryAsync(
            ApplicationDbContext dbContext,
            IMapper mapper
            ) : base(dbContext)
        {
            _transRemittEntrancePermits = dbContext.Set<PurchaseOrderTransferRemittanceEntrancePermit>();
            _transferRemittances = dbContext.Set<PurchaseOrderTransferRemittance>();
            _transferRemittanceDetails = dbContext.Set<PurchaseOrderTransferRemittanceDetail>();
            _orderTransferRemittanceUnloadingPermits = dbContext.Set<PurchaseOrderTransferRemittanceUnloadingPermit>();
            _dbContext = dbContext;
            _mapper = mapper;
            _productInventory = dbContext.Set<ProductInventory>();
        }

        public async Task<PurchaseOrderTransferRemittance> CreateTransferRemittance(PurchaseOrderTransferRemittance transRemittance)
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

            var transRemitt = await _transferRemittances.AddAsync(transRemittance);
            await _dbContext.SaveChangesAsync();

            return transRemittance;
        }

        public async Task<IEnumerable<PurchaseOrderTransferRemittance>> GetAllTransferRemittancesAsync(GetAllTransferRemittancesParameter validFilter)
        {
            return await _dbContext.TransferRemittances
                .Include(t => t.TransferRemittanceType)
                .Include(t => t.VehicleType)
                .Include(t => t.DestinationWarehouse)
                .Include(t => t.OriginWarehouse)
                .Include(t => t.Details).ThenInclude(d => d.PurOTransRemittUnloadingPermitDetail)
                .Include(t => t.PurchaseOrderTransferRemittanceEntrancePermit).ThenInclude(t => t.PurchaseOrderTransferRemittanceUnloadingPermits)
                .Include(t => t.TransferRemittanceStatus)
                .Include(t => t.Details).ThenInclude(d => d.ProductBrand).ThenInclude(b => b.Product).ThenInclude(t => t.ProductSubUnit)
                .Include(t => t.Details).ThenInclude(d => d.ProductBrand).ThenInclude(b => b.Product).ThenInclude(t => t.ProductMainUnit)
                .Include(t => t.Details).ThenInclude(d => d.ProductBrand).ThenInclude(b => b.Brand)
                .Include(t => t.Details).ThenInclude(d => d.ProductBrand).ThenInclude(b => b.Product)
                .Where(t =>
                (t.Id == validFilter.Id || validFilter.Id == null) &&
                (t.TransferRemittanceStatusId == validFilter.TransferRemittStatusId || validFilter.TransferRemittStatusId == null) &&
                ((t.TransferRemittanceStatusId == 2 && validFilter.IsEntranced == true) ||
                (t.TransferRemittanceStatusId != 2 && validFilter.IsEntranced == false) || validFilter.IsEntranced == null) &&
                (t.DestinationWarehouseId == validFilter.DestinationWarehouseId || validFilter.DestinationWarehouseId == null) &&
                (t.PurchaseOrderTransferRemittanceEntrancePermit != null &&
                t.PurchaseOrderTransferRemittanceEntrancePermit.PermitCode == validFilter.TransferEntransePermitNo
                || validFilter.TransferEntransePermitNo == null)).ToListAsync();
        }

        public async Task<PurchaseOrderTransferRemittance> UpdateTransferRemittance(PurchaseOrderTransferRemittance transRemittance)
        {
            try
            {
                var deletedDetails = _transferRemittanceDetails
                            .Where(s => s.TransferRemittanceId == transRemittance.Id &&
                            !transRemittance.Details.Select(d => d.Id).Contains(s.Id));

                _transferRemittanceDetails.RemoveRange(deletedDetails);

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
                    var prevInfo = await _transferRemittanceDetails.FirstOrDefaultAsync(td =>
                                    td.TransferRemittanceId == transRemittance.Id &&
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

                _transferRemittances.Update(transRemittance);
                await _dbContext.SaveChangesAsync();

                return transRemittance;
            }
            catch (Exception e)
            {

                throw;
            }
        }

        public async Task<PurchaseOrderTransferRemittance?> GetTransferRemittanceByIdAsync(int id)
        {
            return await _dbContext.TransferRemittances
                .Include(t => t.TransferRemittanceType)
                .Include(t => t.VehicleType)
                .Include(t => t.DestinationWarehouse)
                .Include(t => t.PurchaseOrderTransferRemittanceEntrancePermit).ThenInclude(t => t.Attachments)
                .Include(t => t.Details).ThenInclude(d=>d.PurOTransRemittUnloadingPermitDetail)
                .Include(t => t.PurchaseOrderTransferRemittanceEntrancePermit)
                    .ThenInclude(t => t.PurchaseOrderTransferRemittanceUnloadingPermits)
                    .ThenInclude(t => t.Attachments)
                .Include(t => t.OriginWarehouse)
                .Include(t => t.TransferRemittanceStatus)
                .Include(t => t.Details).ThenInclude(d => d.ProductBrand).ThenInclude(b => b.Brand)
                .Include(t => t.Details).ThenInclude(d => d.ProductBrand).ThenInclude(b => b.Product).ThenInclude(t => t.ProductSubUnit)
                .Include(t => t.Details).ThenInclude(d => d.ProductBrand).ThenInclude(b => b.Product).ThenInclude(t => t.ProductMainUnit)
                .FirstOrDefaultAsync(o => o.Id == id);
        }

        public async Task<PurchaseOrderTransferRemittance?> GetTransferRemittanceByPermitCodeAsync(int PermitCode)
        {
            return await _dbContext.TransferRemittances
                .Include(t => t.TransferRemittanceType)
                .Include(t => t.VehicleType)
                .Include(t => t.DestinationWarehouse)
                .Include(t => t.PurchaseOrderTransferRemittanceEntrancePermit).ThenInclude(t => t.Attachments)
                .Include(t => t.Details).ThenInclude(d => d.PurOTransRemittUnloadingPermitDetail)
                .Include(t => t.PurchaseOrderTransferRemittanceEntrancePermit)
                    .ThenInclude(t => t.PurchaseOrderTransferRemittanceUnloadingPermits)
                    .ThenInclude(t => t.Attachments)
                .Include(t => t.OriginWarehouse)
                .Include(t => t.TransferRemittanceStatus)
                .Include(t => t.Details).ThenInclude(d => d.ProductBrand).ThenInclude(b => b.Brand)
                .Include(t => t.Details).ThenInclude(d => d.ProductBrand).ThenInclude(b => b.Product).ThenInclude(t => t.ProductSubUnit)
                .Include(t => t.Details).ThenInclude(d => d.ProductBrand).ThenInclude(b => b.Product).ThenInclude(t => t.ProductMainUnit)
                .FirstOrDefaultAsync(o => o.PurchaseOrderTransferRemittanceEntrancePermit.PermitCode == PermitCode);
        }

        public async Task<PurchaseOrderTransferRemittanceEntrancePermit> TransferRemittanceEntrancePermission(
            TransferRemittanceEntrancePermissionCommand entrancePermit)
        {
            try
            {
                var transRemit = await _dbContext.TransferRemittances
                       .AsNoTracking()
                       .FirstOrDefaultAsync(o => o.Id == entrancePermit.PurchaseOrderTransferRemittanceId);

                if (transRemit.TransferRemittanceStatusId == 2)
                    throw new ApiException("مجوز ورود حواله قبلا ثبت شده است !");
                if (transRemit == null)
                    throw new ApiException("حواله یافت نشد !");

                transRemit.TransferRemittanceStatusId = 2;
                _transferRemittances.Update(transRemit);

                var newEntrancePermit = _mapper.Map<PurchaseOrderTransferRemittanceEntrancePermit>(entrancePermit);
                //var mappedTransRemittEntrancePermits = new PurchaseOrderTransferRemittanceEntrancePermit
                //{
                //    PurchaseOrderTransferRemittance = transRemit,
                //    PurchaseOrderTransferRemittanceId = id,
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

        public async Task<PurchaseOrderTransferRemittanceEntrancePermit> PurchaseOrderTransRemittEntrancePermitById
            (Guid purchaseOrderTransferRemittanceEntrancePermitId)
        {
            var transferPermit = await _transferRemittances
                .Include(t => t.PurchaseOrderTransferRemittanceEntrancePermit)
                .FirstOrDefaultAsync(o =>
            o.PurchaseOrderTransferRemittanceEntrancePermit.Id == purchaseOrderTransferRemittanceEntrancePermitId);

            return transferPermit.PurchaseOrderTransferRemittanceEntrancePermit;
        }

        public async Task<PurchaseOrderTransferRemittanceUnloadingPermit> CreatePurchaseOrderTransferRemittanceUnloadingPermit
            (PurchaseOrderTransferRemittanceUnloadingPermit purchaseOrderTransferRemittanceUnloadingPermit)
        {
            try
            {
                var transferRemitt = await _transferRemittances.AsNoTracking()
                        .Include(t => t.Details).AsNoTracking()
                        .Include(t => t.PurchaseOrderTransferRemittanceEntrancePermit).AsNoTracking()
                        .FirstOrDefaultAsync(t =>
                        t.PurchaseOrderTransferRemittanceEntrancePermit.Id == purchaseOrderTransferRemittanceUnloadingPermit.PurchaseOrderTransferRemittanceEntrancePermitId);

                if (transferRemitt == null)
                    throw new ApiException("حواله انتقال یافت نشد !");
                if (transferRemitt.TransferRemittanceStatusId >= 3)
                    throw new ApiException("تخلیه حواله قبلا بصورت کامل انجام شده !");

                #region اگر مجموع اقلام انتقال داده شده با مجموع اقلام خروج خورده یکسان باشد وضعیت حواله انتقال به تخلیه شده تبدیل خواهد شد
                transferRemitt = _mapper.Map(purchaseOrderTransferRemittanceUnloadingPermit, transferRemitt);

                var calcUnloadedSum = await
                    _orderTransferRemittanceUnloadingPermits.AsNoTracking()
                    .Include(t => t.PurchaseOrderTransferRemittanceUnloadingPermitDetails).AsNoTracking()
                    .Where(t => t.PurchaseOrderTransferRemittanceEntrancePermitId == purchaseOrderTransferRemittanceUnloadingPermit.PurchaseOrderTransferRemittanceEntrancePermitId)
                    .FirstOrDefaultAsync();

                var transPermitAmounts = transferRemitt.Details.Sum(d => d.TransferAmount);
                var unloadedAmounts = (calcUnloadedSum == null ? 0 :
                    calcUnloadedSum.PurchaseOrderTransferRemittanceUnloadingPermitDetails.Sum(f => f.UnloadedAmount)) +
                    purchaseOrderTransferRemittanceUnloadingPermit.PurchaseOrderTransferRemittanceUnloadingPermitDetails.Sum(t => t.UnloadedAmount);

                if (transPermitAmounts >= unloadedAmounts)
                    transferRemitt.TransferRemittanceStatusId = 3;//---تخلیه شده
                #endregion

                _transferRemittances.Update(transferRemitt);

                var UnloadingPermit = await _orderTransferRemittanceUnloadingPermits
                    .AddAsync(purchaseOrderTransferRemittanceUnloadingPermit);

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