using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Sepehr.Application.Exceptions;
using Sepehr.Application.Features.TransferWarehouseInventories.Queries.GetAllTransferWarehouseInventories;
using Sepehr.Application.Interfaces.Repositories;
using Sepehr.Domain.Entities;
using Sepehr.Domain.Enums;
using Sepehr.Infrastructure.Persistence.Context;
using System.Data;

namespace Sepehr.Infrastructure.Persistence.Repositories
{
    public class TransferWarehouseInventoryRepositoryAsync :
        GenericRepositoryAsync<TransferWarehouseInventory>,
        ITransferWarehouseInventoryRepositoryAsync
    {
        private readonly DbSet<ProductInventory> _productInventory;
        private readonly DbSet<TransferWarehouseInventory> _transferInventories;
        private readonly ApplicationDbContext _dbContext;
        private readonly IMapper _mapper;

        public TransferWarehouseInventoryRepositoryAsync(
            ApplicationDbContext dbContext,
            IMapper mapper
            ) : base(dbContext)
        {
            _transferInventories = dbContext.Set<TransferWarehouseInventory>();
            _dbContext = dbContext;
            _mapper = mapper;
            _productInventory = dbContext.Set<ProductInventory>();
        }

        public async Task<TransferWarehouseInventory> CreateTransferWarehouseInventory(TransferWarehouseInventory transInventory)
        {
            foreach (var item in transInventory.Details)
            {
                var _prodMabadiInventory =await _dbContext.Set<ProductInventory>()
                    .FirstOrDefaultAsync(i => i.ProductBrandId == item.ProductBrandId &&
                                              i.WarehouseId == transInventory.OriginWarehouseId &&
                                              i.Warehouse.WarehouseTypeId == (int)EWarehouseType.Mabadi);
                if (_prodMabadiInventory == null)
                    throw new ApiException("موجودی انبار مبادی یافت نشد !");

                if (item.TransferAmount > _prodMabadiInventory.PurchaseInventory)
                    throw new ApiException("موجودی خرید کافی نمی باشد !");

                _prodMabadiInventory.PurchaseInventory -= item.TransferAmount;
                _prodMabadiInventory.ApproximateInventory += item.TransferAmount;

                _productInventory.Update(_prodMabadiInventory);
            }

            var transInv = await _transferInventories.AddAsync(transInventory);

            await _dbContext.SaveChangesAsync();
            return transInventory;
        }

        public async Task<IEnumerable<TransferWarehouseInventory>> GetAllTransferWarehouseInventoriesAsync(GetAllTransferWarehouseInventoriesParameter validFilter)
        {
            return await _dbContext.TransferWarehouseInventories
                .Include(c => c.ApplicationUser)
                .Include(t => t.Details).ThenInclude(d => d.ProductBrand).ThenInclude(b => b.Product).ThenInclude(t => t.ProductSubUnit)
                .Include(t => t.Details).ThenInclude(d => d.ProductBrand).ThenInclude(b => b.Product).ThenInclude(t => t.ProductMainUnit)
                .Include(t => t.Details).ThenInclude(d => d.ProductBrand).ThenInclude(b => b.Brand)
                .Include(t => t.Details).ThenInclude(d => d.ProductBrand).ThenInclude(b => b.Product)
                .Where(x=>
                        (x.Id==validFilter.Id || validFilter.Id==null) &&
                        (x.OriginWarehouseId==validFilter.OriginWarehouseId || validFilter.OriginWarehouseId == null))
                .ToListAsync();
        }

        public async Task<TransferWarehouseInventory> UpdateTransferWarehouseInventory(TransferWarehouseInventory transInventory)
        {
            foreach (var item in transInventory.Details)
            {
                var _prodMabadiInventory =await _dbContext.Set<ProductInventory>()
                    .FirstOrDefaultAsync(i => i.ProductBrandId == item.ProductBrandId &&
                                              i.WarehouseId == transInventory.OriginWarehouseId &&
                                              i.Warehouse.WarehouseTypeId == (int)EWarehouseType.Mabadi);
                if (_prodMabadiInventory == null)
                    throw new ApiException("موجودی انبار مبادی یافت نشد !");

                if (item.TransferAmount > _prodMabadiInventory.PurchaseInventory)
                    throw new ApiException("موجودی خرید کافی نمی باشد !");

                _prodMabadiInventory.PurchaseInventory -= item.TransferAmount;
                _prodMabadiInventory.ApproximateInventory += item.TransferAmount;

                _productInventory.Update(_prodMabadiInventory);
            }

            var transInv = await _transferInventories.AddAsync(transInventory);

            await _dbContext.SaveChangesAsync();
            return transInventory;
        }

        public async Task<TransferWarehouseInventory?> GetTransferWarehouseInventoryByIdAsync(int id)
        {
            return await _dbContext.TransferWarehouseInventories
                .Include(c => c.ApplicationUser)
                .Include(t => t.Details).ThenInclude(d => d.ProductBrand).ThenInclude(b => b.Brand)
                .Include(t => t.Details).ThenInclude(d => d.ProductBrand).ThenInclude(b => b.Product).ThenInclude(t => t.ProductSubUnit)
                .Include(t => t.Details).ThenInclude(d => d.ProductBrand).ThenInclude(b => b.Product).ThenInclude(t => t.ProductMainUnit)
                .FirstOrDefaultAsync(o => o.Id == id);
        }

        public async Task DeleteTransferInventory(int id)
        {
            var transInventory=await _transferInventories.FirstOrDefaultAsync(x=>x.Id==id);
            if (transInventory == null)
                throw new ApiException("رکورد انتقال یافت نشد !");

            foreach (var item in transInventory.Details)
            {
                var _prodMabadiInventory = await _dbContext.Set<ProductInventory>()
                    .FirstOrDefaultAsync(i => i.ProductBrandId == item.ProductBrandId &&
                                              i.WarehouseId == transInventory.OriginWarehouseId &&
                                              i.Warehouse.WarehouseTypeId == (int)EWarehouseType.Mabadi);

                if (_prodMabadiInventory == null)
                    throw new ApiException("موجودی انبار مبادی یافت نشد !");

                //----به همان اندازه که از موجودی خرید کسر شده بود اضافه می شود
                _prodMabadiInventory.PurchaseInventory += item.TransferAmount;

                //----به همان اندازه که به موجودی تقریبی اضافه شده بود کسر می شود
                _prodMabadiInventory.ApproximateInventory -= item.TransferAmount;

                _productInventory.Update(_prodMabadiInventory);
            }
            
            _transferInventories.Remove(transInventory);
            await _dbContext.SaveChangesAsync();
        }
    }
}