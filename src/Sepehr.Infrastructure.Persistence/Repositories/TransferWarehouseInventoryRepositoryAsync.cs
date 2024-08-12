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
                                              i.Warehouse.WarehouseTypeId == (int)EWarehouseType.MabadiWarehouse);
                if (_prodMabadiInventory == null)
                    throw new ApiException("موجودی یافت نشد !");

                var _prodPurchaseiInventory =await _dbContext.Set<ProductInventory>()
                    .FirstOrDefaultAsync(i => i.ProductBrandId == item.ProductBrandId &&
                                              i.WarehouseId == transInventory.OriginWarehouseId &&
                                              i.Warehouse.WarehouseTypeId == (int)EWarehouseType.VirtualWarehouse);
                if (_prodPurchaseiInventory == null)
                    throw new ApiException("موجودی یافت نشد !");

                if (item.TransferAmount > _prodPurchaseiInventory.PurchaseInventory)
                    throw new ApiException("موجودی خرید کافی نمی باشد !");

                _prodPurchaseiInventory.PurchaseInventory -= item.TransferAmount;
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
                .ToListAsync();
        }

        public async Task<TransferWarehouseInventory> UpdateTransferWarehouseInventory(TransferWarehouseInventory transRemittance)
        {
            throw new Exception();
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


    }
}