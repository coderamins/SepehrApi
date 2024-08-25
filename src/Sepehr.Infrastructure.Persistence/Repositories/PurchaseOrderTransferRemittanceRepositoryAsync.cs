using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Sepehr.Application.Exceptions;
using Sepehr.Application.Features.TransferRemittances.Command.TransferRemittanceEntrancePermission;
using Sepehr.Application.Features.TransferRemittances.Queries.GetAllTransferRemittances;
using Sepehr.Application.Interfaces.Repositories;
using Sepehr.Domain.Entities;
using Sepehr.Domain.Enums;
using Sepehr.Infrastructure.Persistence.Context;
using System.Data;

namespace Sepehr.Infrastructure.Persistence.Repositories
{
    public class TransferRemittanceRepositoryAsync :
        GenericRepositoryAsync<TransferRemittance>,
        ITransferRemittanceRepositoryAsync
    {
        private readonly DbSet<ProductInventory> _productInventory;
        private readonly DbSet<OfficialWarehoseInventory> _productOffInventory;
        private readonly DbSet<TransferRemittance> _transferRemittances;
        private readonly DbSet<TransferRemittanceDetail> _transferRemittanceDetails;
        private readonly DbSet<EntrancePermit> _transRemittEntrancePermits;
        private readonly DbSet<UnloadingPermit> _orderTransferRemittanceUnloadingPermits;
        private readonly ApplicationDbContext _dbContext;
        private readonly IMapper _mapper;

        public TransferRemittanceRepositoryAsync(
            ApplicationDbContext dbContext,
            IMapper mapper
            ) : base(dbContext)
        {
            _transRemittEntrancePermits = dbContext.Set<EntrancePermit>();
            _transferRemittances = dbContext.Set<TransferRemittance>();
            _transferRemittanceDetails = dbContext.Set<TransferRemittanceDetail>();
            _orderTransferRemittanceUnloadingPermits = dbContext.Set<UnloadingPermit>();
            _productOffInventory = dbContext.Set<OfficialWarehoseInventory>();
            _dbContext = dbContext;
            _mapper = mapper;
            _productInventory = dbContext.Set<ProductInventory>();
        }

        public async Task<TransferRemittance> CreateTransferRemittance(TransferRemittance transRemittance)
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

                //----موجودی تقریبی انبار مبدا کم می شود
                var prodInvEntry1 = _productInventory.Entry(_originWarehouse);
                prodInvEntry1.State = EntityState.Modified;

                _originWarehouse.ApproximateInventory -= item.TransferAmount;
                prodInvEntry1.CurrentValues.SetValues(_originWarehouse);

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
                    var prodInvEntry = _productInventory.Entry(_destWarehouse);
                    prodInvEntry.State = EntityState.Modified;

                    _destWarehouse.OnTransitInventory += item.TransferAmount;
                    prodInvEntry.CurrentValues.SetValues(_destWarehouse);
                }
            }

            var transRemitt = await _transferRemittances.AddAsync(transRemittance);
            await _dbContext.SaveChangesAsync();

            return transRemittance;
        }

        public async Task<IEnumerable<TransferRemittance>> GetAllTransferRemittancesAsync(GetAllTransferRemittancesParameter validFilter)
        {
            return await _dbContext.TransferRemittances
                .Include(c => c.ApplicationUser)
                .Include(t => t.TransferRemittanceType)
                .Include(t => t.VehicleType)
                .Include(t => t.DestinationWarehouse)
                .Include(t => t.OriginWarehouse)
                .Include(t => t.Details).ThenInclude(d => d.UnloadingPermitDetail)
                .Include(t => t.EntrancePermit).ThenInclude(t => t.UnloadingPermits)
                .Include(t => t.TransferRemittanceStatus)
                .Include(t => t.Details).ThenInclude(d => d.ProductBrand).ThenInclude(b => b.Product).ThenInclude(t => t.ProductSubUnit)
                .Include(t => t.Details).ThenInclude(d => d.ProductBrand).ThenInclude(b => b.Product).ThenInclude(t => t.ProductMainUnit)
                .Include(t => t.Details).ThenInclude(d => d.ProductBrand).ThenInclude(b => b.Brand)
                .Include(t => t.Details).ThenInclude(d => d.ProductBrand).ThenInclude(b => b.Product)
                .Where(t =>
                    (t.OriginWarehouse.CustomerWarehouses.Any(cw => cw.CustomerId == validFilter.MarketerId) || validFilter.MarketerId == null) &&
                    (t.OriginWarehouseId == validFilter.OriginWarehouseId || validFilter.OriginWarehouseId == null) &&
                    (t.Id == validFilter.Id || validFilter.Id == null) &&
                    (t.TransferRemittanceStatusId == validFilter.TransferRemittStatusId || validFilter.TransferRemittStatusId == null) &&
                    ((t.TransferRemittanceStatusId == 2 && validFilter.IsEntranced == true) ||
                    (t.TransferRemittanceStatusId != 2 && validFilter.IsEntranced == false) || validFilter.IsEntranced == null) &&
                    (t.DestinationWarehouseId == validFilter.DestinationWarehouseId || validFilter.DestinationWarehouseId == null) &&
                    (t.EntrancePermit != null &&
                    t.EntrancePermit.PermitCode == validFilter.TransferEntransePermitNo
                    || validFilter.TransferEntransePermitNo == null)).ToListAsync();
        }

        public async Task<TransferRemittance> UpdateTransferRemittance(TransferRemittance transRemittance)
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

        public async Task<TransferRemittance?> GetTransferRemittanceByIdAsync(int id)
        {
            return await _dbContext.TransferRemittances
                .Include(c => c.ApplicationUser)
                .Include(t => t.TransferRemittanceType)
                .Include(t => t.VehicleType)
                .Include(t => t.DestinationWarehouse)
                .Include(t => t.EntrancePermit).ThenInclude(t => t.Attachments)
                .Include(t => t.Details).ThenInclude(d => d.UnloadingPermitDetail)
                .Include(t => t.EntrancePermit)
                    .ThenInclude(t => t.UnloadingPermits)
                    .ThenInclude(t => t.Attachments)
                .Include(t => t.OriginWarehouse)
                .Include(t => t.TransferRemittanceStatus)
                .Include(t => t.Details).ThenInclude(d => d.ProductBrand).ThenInclude(b => b.Brand)
                .Include(t => t.Details).ThenInclude(d => d.ProductBrand).ThenInclude(b => b.Product).ThenInclude(t => t.ProductSubUnit)
                .Include(t => t.Details).ThenInclude(d => d.ProductBrand).ThenInclude(b => b.Product).ThenInclude(t => t.ProductMainUnit)
                .FirstOrDefaultAsync(o => o.Id == id);
        }

        public async Task<TransferRemittance?> GetTransferRemittanceByPermitCodeAsync(int PermitCode)
        {
            return await _dbContext.TransferRemittances
                .Include(c => c.ApplicationUser)
                .Include(t => t.TransferRemittanceType)
                .Include(t => t.VehicleType)
                .Include(t => t.DestinationWarehouse)
                .Include(t => t.EntrancePermit).ThenInclude(t => t.Attachments)
                .Include(t => t.Details).ThenInclude(d => d.UnloadingPermitDetail)
                .Include(t => t.EntrancePermit)
                    .ThenInclude(t => t.UnloadingPermits)
                    .ThenInclude(t => t.Attachments)
                .Include(t => t.OriginWarehouse)
                .Include(t => t.TransferRemittanceStatus)
                .Include(t => t.Details).ThenInclude(d => d.ProductBrand).ThenInclude(b => b.Brand)
                .Include(t => t.Details).ThenInclude(d => d.ProductBrand).ThenInclude(b => b.Product).ThenInclude(t => t.ProductSubUnit)
                .Include(t => t.Details).ThenInclude(d => d.ProductBrand).ThenInclude(b => b.Product).ThenInclude(t => t.ProductMainUnit)
                .FirstOrDefaultAsync(o => o.EntrancePermit.PermitCode == PermitCode);
        }

        public async Task<EntrancePermit> TransferRemittanceEntrancePermission(
            TransferRemittanceEntrancePermissionCommand entrancePermit)
        {
            try
            {
                var transRemit = await _dbContext.TransferRemittances
                       .AsNoTracking()
                       .FirstOrDefaultAsync(o => o.Id == entrancePermit.TransferRemittanceId);

                if (transRemit.TransferRemittanceStatusId == 2)
                    throw new ApiException("مجوز ورود حواله قبلا ثبت شده است !");
                if (transRemit == null)
                    throw new ApiException("حواله یافت نشد !");

                transRemit.TransferRemittanceStatusId = 2;
                _transferRemittances.Update(transRemit);

                var newEntrancePermit = _mapper.Map<EntrancePermit>(entrancePermit);
                //var mappedTransRemittEntrancePermits = new EntrancePermit
                //{
                //    TransferRemittance = transRemit,
                //    TransferRemittanceId = id,
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

        public async Task<EntrancePermit> TransRemittEntrancePermitById
            (Guid purchaseOrderTransferRemittanceEntrancePermitId)
        {
            var transferPermit = await _transferRemittances
                .Include(t => t.EntrancePermit)
                .FirstOrDefaultAsync(o =>
                        o.EntrancePermit.Id == purchaseOrderTransferRemittanceEntrancePermitId);

            return transferPermit.EntrancePermit;
        }

        public async Task<UnloadingPermit> CreatePOrderUnloadingPermit(UnloadingPermit unloadingPermit)
        {
            try
            {
                var transferRemitt = await _transferRemittances.AsNoTracking()
                        .Include(t => t.Details).ThenInclude(d => d.UnloadingPermitDetail).AsNoTracking()
                        .Include(t => t.EntrancePermit).AsNoTracking()
                        .FirstOrDefaultAsync(t =>
                        t.EntrancePermit.Id == unloadingPermit.EntrancePermitId);

                if (transferRemitt == null)
                    throw new ApiException("حواله انتقال یافت نشد !");
                if (transferRemitt.TransferRemittanceStatusId >= 3)
                    throw new ApiException("تخلیه حواله قبلا بصورت کامل انجام شده !");

                transferRemitt = _mapper.Map(unloadingPermit, transferRemitt);

                #region موجودی انبار رسمی و عادی بروزرسانی می شود 
                foreach (var udetail in unloadingPermit.UnloadingPermitDetails)
                {
                    var _tRemitt = await _transferRemittanceDetails
                        .Include(x => x.TransferRemittance).ThenInclude(x=>x.PurchaseOrder)
                        .FirstOrDefaultAsync(d => d.Id == udetail.TransferRemittanceDetailId);
                    if (_tRemitt == null)
                        throw new ApiException("خطا در اجرای درخواست !");


                    //---------اگر فاکتور رسمی باشد باید موجودی انبار رسمی بروزرسانی شود------
                    if (_tRemitt.TransferRemittance.PurchaseOrder.InvoiceTypeId == (int)EInvoiceType.Formal)
                    {
                        #region در این قسمت از سفارش خرید مربوطه جزئیات کالا واکشی شده و به موجودی انبار رسمی باید اضافه شود
                        var po_Details = await _dbContext.Set<PurchaseOrderDetail>()
                            .FirstAsync(x => x.OrderId == _tRemitt.TransferRemittance.PurchaseOrderId &&
                            (x.AlternativeProductBrandId == _tRemitt.ProductBrandId ||
                            x.ProductBrandId == _tRemitt.ProductBrandId));

                        var _off_Inv = await _productOffInventory
                            .FirstOrDefaultAsync(i => i.ProductId == _dbContext.Set<ProductBrand>()
                            .First(x => x.Id == (po_Details.AlternativeProductBrandId ?? po_Details.ProductBrandId)).ProductId &&
                            i.WarehouseId == _tRemitt.TransferRemittance.DestinationWarehouseId);

                        if (_off_Inv == null)
                            throw new ApiException("موجودی رسمی یافت نشد !");

                        var _off_whEntry = _productOffInventory.Entry(_off_Inv);
                        _off_whEntry.State = EntityState.Modified;

                        _off_Inv.FloorInventory += (double)udetail.UnloadedAmount;
                        _off_Inv.ApproximateInventory += (double)(udetail.UnloadedAmount - _tRemitt.TransferAmount);

                        _off_whEntry.CurrentValues.SetValues(_off_Inv);
                        #endregion
                    }

                    #region موجودی واقعی و تقریبی کالا اضافه می شود
                    var inv = await _productInventory
                        .FirstOrDefaultAsync(i => i.ProductBrandId == _tRemitt.ProductBrandId &&
                        i.WarehouseId == _tRemitt.TransferRemittance.DestinationWarehouseId);

                    if (inv == null)
                        throw new ApiException("انبار یافت نشد !");
                    else
                    {
                        var invEntry = _productInventory.Entry(inv);
                        invEntry.State = EntityState.Modified;

                        inv.FloorInventory += (double)udetail.UnloadedAmount;
                        inv.ApproximateInventory += (udetail.UnloadedAmount - _tRemitt.TransferAmount);

                        invEntry.CurrentValues.SetValues(inv);
                    }
                    #endregion
                }
                #endregion

                #region اگر مجموع اقلام انتقال داده شده با مجموع اقلام خروج خورده یکسان باشد وضعیت حواله انتقال به تخلیه شده تبدیل خواهد شد

                var calcUnloadedSum = await
                    _orderTransferRemittanceUnloadingPermits.AsNoTracking()
                    .Include(t => t.UnloadingPermitDetails).AsNoTracking()
                    .Where(t => t.EntrancePermitId == unloadingPermit.EntrancePermitId)
                    .FirstOrDefaultAsync();

                var transPermitAmounts = transferRemitt.Details.Sum(d => d.TransferAmount);
                var unloadedAmounts = (calcUnloadedSum == null ? 0 :
                    calcUnloadedSum.UnloadingPermitDetails.Sum(f => f.UnloadedAmount)) +
                    unloadingPermit.UnloadingPermitDetails.Sum(t => t.UnloadedAmount);

                if (transPermitAmounts >= unloadedAmounts)
                    transferRemitt.TransferRemittanceStatusId = (int)ETransferRemittanceStatus.Unloaded;//---تخلیه شده
                #endregion

                var transRemittEntry = _transferRemittances.Entry(transferRemitt);
                transRemittEntry.State = EntityState.Modified;

                transRemittEntry.CurrentValues.SetValues(transferRemitt);


                var UnloadingPermit = await _orderTransferRemittanceUnloadingPermits.AddAsync(unloadingPermit);

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