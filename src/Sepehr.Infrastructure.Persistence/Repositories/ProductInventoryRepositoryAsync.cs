using AutoMapper;
using Dapper;
using Microsoft.EntityFrameworkCore;
using Sepehr.Application.DTOs.Order;
using Sepehr.Application.DTOs.Product;
using Sepehr.Application.DTOs.PurchaseOrder;
using Sepehr.Application.Interfaces.Repositories;
using Sepehr.Domain.Entities;
using Sepehr.Domain.Enums;
using Sepehr.Domain.ViewModels;
using Sepehr.Infrastructure.Persistence.Context;
using System.Runtime.CompilerServices;

namespace Sepehr.Infrastructure.Persistence.Repositories
{
    public class ProductInventoryRepositoryAsync : GenericRepositoryAsync<ProductInventory>, IProductInventoryRepositoryAsync
    {
        private readonly DbSet<ProductInventory> _productInventory;
        private readonly DbSet<Order> _order;
        private readonly DbSet<Warehouse> _warehouses;
        private readonly DbSet<OfficialWarehoseInventory> _offWHouseInventory;
        private readonly DbSet<CargoAnnounce> _cargoAnnounces;
        private readonly DbSet<ProductInventoryHistory> _inventoryHistories;
        private readonly DbSet<Product> _products;
        private readonly DbSet<ProductBrand> _productBrands;
        public readonly DapperContext _dapperContext;
        public readonly IMapper _mapper;

        public ProductInventoryRepositoryAsync(
            IMapper mapper,
            ApplicationDbContext dbContext,
            DapperContext dapContext) : base(dbContext)
        {
            _productBrands = dbContext.Set<ProductBrand>();
            _products = dbContext.Set<Product>();
            _offWHouseInventory = dbContext.Set<OfficialWarehoseInventory>();
            _productInventory = dbContext.Set<ProductInventory>();
            _order = dbContext.Set<Order>();
            _warehouses = dbContext.Set<Warehouse>();
            _cargoAnnounces = dbContext.Set<CargoAnnounce>();
            _inventoryHistories = dbContext.Set<ProductInventoryHistory>();
            _dapperContext = dapContext;
            _mapper = mapper;
        }

        public async Task<ProductInventory?> GetProductInventory(int productBrandId, int warehouseId)
        {
            var inv = await _productInventory.FirstOrDefaultAsync(p =>
                    p.ProductBrandId == productBrandId &&
                    p.WarehouseId == warehouseId);

            return inv;
        }

        public async Task<List<ProductInventory>?> GetProductsByInventory()
        {
            var res = await _productInventory
                .Include(i => i.ProductBrand).ThenInclude(b => b.Product).ThenInclude(b => b.ProductState)
                .Include(i => i.ProductBrand).ThenInclude(b => b.Brand)
                .Include(i => i.ProductBrand).ThenInclude(pb => pb.Product)
                    .ThenInclude(c => c.ProductPrices.Where(p => p.IsActive)).ThenInclude(p => p.ProductBrand)
                .Include(i => i.ProductBrand).ThenInclude(pb => pb.Product)
                    .ThenInclude(p => p.ProductInventories).ThenInclude(p => p.Warehouse).ThenInclude(p => p.WarehouseType)
                .Include(i => i.ProductBrand).ThenInclude(pb => pb.Product).ThenInclude(p => p.ProductType)
                .Include(i => i.ProductBrand).ThenInclude(pb => pb.Product).ThenInclude(p => p.ProductMainUnit)
                .Include(i => i.ProductBrand).ThenInclude(pb => pb.Product).ThenInclude(p => p.ProductSubUnit)
                .Include(i => i.ProductBrand).ThenInclude(pb => pb.Product).ThenInclude(p => p.ProductStandard)
                .Include(i => i.Warehouse).ThenInclude(p => p.WarehouseType)
                .Where(p => p.IsActive)
                .OrderByDescending(p => p.ProductBrand.Product.Created).ToListAsync();

            return res;
        }

        public async Task<ProductInventory> UpdateProductInventory(int productBrandId, int amount)
        {
            var prodInventory = await _productInventory.FirstOrDefaultAsync(i => i.ProductBrandId == productBrandId);
            if (prodInventory == null)
            {
                foreach (var wh in _warehouses.Where(w => w.WarehouseTypeId != 5).ToList())
                {
                    await _productInventory.AddAsync(new ProductInventory
                    {
                        ApproximateInventory = 0,
                        ProductBrandId = productBrandId,
                        OrderPoint = 0,
                        MinInventory = 0,
                        MaxInventory = 0,
                        IsActive = true,
                        FloorInventory = 0,
                        WarehouseId = wh.Id,
                        Created = DateTime.Now,
                        //CreatedBy = "sys",
                    });
                }
            }

            prodInventory.ApproximateInventory += amount;
            return prodInventory;
        }

        public async Task<bool> UpdateProductInventory(List<OrderDetailRequest> details)
        {
            foreach (var prodBrand in details)
            {
                var prodInventory = await _productInventory
                    .FirstOrDefaultAsync(i => i.ProductBrandId == prodBrand.ProductBrandId &&
                                i.WarehouseId == prodBrand.WarehouseId);

                if (prodInventory == null)
                {
                    foreach (var wh in _warehouses.Where(w => w.WarehouseTypeId != 5).ToList())
                    {
                        await _productInventory.AddAsync(new ProductInventory
                        {
                            ApproximateInventory = 0 - prodBrand.ProximateAmount,
                            ProductBrandId = prodBrand.ProductBrandId,
                            OrderPoint = 0,
                            MinInventory = 0,
                            MaxInventory = 0,
                            IsActive = true,
                            FloorInventory = 0,
                            WarehouseId = wh.Id,
                            Created = DateTime.Now,
                            //CreatedBy = "sys",
                        });
                    }
                }
                else
                {
                    prodInventory.ApproximateInventory -= prodBrand.ProximateAmount;
                    _productInventory.Update(prodInventory);
                }
            }

            return true;
        }

        public async Task<bool> UpdateProductInventory(Guid orderId, List<OrderDetailRequest> details, InventoryActionType actionType)
        {
            var order = await _order.Include(d => d.Details)
                .FirstAsync(o => o.Id == orderId);

            if (actionType == InventoryActionType.DeleteOrder || actionType == InventoryActionType.ReturnOrder)
            {
                foreach (var prodBrand in order.Details)
                {
                    var prodInventory = _productInventory.First(i => i.ProductBrandId == prodBrand.ProductBrandId);
                    prodInventory.ApproximateInventory += prodBrand.ProximateAmount;

                    _productInventory.Update(prodInventory);
                }
            }
            else
            {
                foreach (var prodBrand in details)
                {
                    if (!_productInventory.Any(i => i.ProductBrandId == prodBrand.ProductBrandId))
                    {
                        foreach (var wh in _warehouses.Where(w => w.WarehouseTypeId != 5).ToList())
                        {
                            await _productInventory.AddAsync(new ProductInventory
                            {
                                ApproximateInventory = 0 - prodBrand.ProximateAmount,
                                ProductBrandId = prodBrand.ProductBrandId,
                                OrderPoint = 0,
                                MinInventory = 0,
                                MaxInventory = 0,
                                IsActive = true,
                                FloorInventory = 0,
                                WarehouseId = wh.Id,
                                Created = DateTime.Now,
                                //CreatedBy = "sys",
                            });
                        }
                    }
                    else
                    {
                        var prodInventory = await _productInventory
                            .FirstOrDefaultAsync(i => i.ProductBrandId == prodBrand.ProductBrandId &&
                                i.WarehouseId == prodBrand.WarehouseId);

                        //---اگه محصول ویرایش شده باشه ابتدا مقدار قبلی به موجودی اضافه شده و سپس از مقدار جدید کسر خواهد شد .

                        var prevProd = order.Details.FirstOrDefault(d => d.Id == prodBrand.ProductBrandId);
                        if (prodInventory != null)
                        {
                            prodInventory.ApproximateInventory = prevProd == null ?
                                prodInventory.ApproximateInventory - prodBrand.ProximateAmount :
                                prodInventory.ApproximateInventory + prevProd.ProximateAmount - prodBrand.ProximateAmount;

                            _productInventory.Update(prodInventory);
                        }
                    }
                }
            }

            return true;
        }

        public async Task<bool> UpdateProductInventory(List<CreatePurchaseOrderDetailRequest> details)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// موقع خروج سفارش از 
        /// </summary>
        /// <param name="CargoAnnounceId"></param>
        /// <returns></returns>
        public async Task<bool> UpdateProductInventory(Guid CargoAnnounceId)
        {
            //var cargoAnnc = await _cargoAnnounces
            //.Include(c => c.LadingPermits).ThenInclude(c => c.LadingLicenseDetails).ThenInclude(c => c.OrderDetail)
            //.FirstOrDefaultAsync(c => c.Id == CargoAnnounceId);

            //foreach (var item in cargoAnnc.LadingLicenses)
            //{
            //    foreach (var od in item.LadingLicenseDetails)
            //    {
            //        var productInventory = await _productInventory.FirstOrDefaultAsync(i => i.ProductBrandId == od.OrderDetail.ProductBrandId &&
            //        i.WarehouseId == od.OrderDetail.WarehouseId);

            //        if (productInventory != null)
            //        {
            //            productInventory.FloorInventory -= (double)od.RealAmount;
            //            _productInventory.Update(productInventory);
            //        }
            //    }
            //}

            return true;
        }


        public async Task<IEnumerable<DapperProduct>> GetProductInventories(int? warehouseTypeId, int? WarehouseId)
        {
            var query = $@"select t1.Id as ProductId,
	                            ProductCode,
                                ProductName,
                                ProductTypeId,
                                t11.[Desc] as ProductTypeDesc,
                                ProductSize,
                                ProductThickness,
                                ApproximateWeight,
                                NumberInPackage,
                                ProductStandardId,
                                ProductStateId,
                                ProductMainUnitId,
                                t9.UnitName as ProductMainUnitDesc,
                                ProductSubUnitId,
                                t10.UnitName as productSubUnitDesc,
                                ExchangeRate,
                                Description,
                                t1.Created,
                                t1.IsActive,
                                0 as BrandId,
                                0 as ProductBrandId,
                                t3.WarehouseId,
                                t3.ApproximateInventory,
                                t3.FloorInventory,
                                t3.MaxInventory,
                                t3.MinInventory,
                                t3.OrderPoint,
                                '' as ProductBrandName,
								t5.Name as WarehouseName,
								t6.[Desc] as ProductStateDesc,
								t7.[Desc] as ProductStandardDesc,
								0 as ProductPrice

                            from sepdb.Products as t1
                            left join sepdb.OfficialWarehoseInventories as t3 on t3.ProductId=t1.Id
							left join sepdb.Warehouses as t5 on t3.WarehouseId=t5.Id 
							left join sepdb.ProductStates as t6 on t6.Id=t1.ProductStateId
							left join sepdb.ProductStandards as t7 on t7.Id=t1.ProductStandardId
							left join sepdb.ProductUnits as t9 on t9.Id=t1.ProductMainUnitId 
							left join sepdb.ProductUnits as t10 on t10.Id=t1.ProductSubUnitId 
                            left join sepdb.ProductTypes as t11 on t11.Id=t1.ProductTypeId
							where t1.IsActive=1 and t5.WarehouseTypeId=5 and
                                  (t5.WarehouseTypeId={warehouseTypeId ?? -1} or {warehouseTypeId ?? -1}=-1) 
                                  and (t5.Id={WarehouseId ?? -1} or {WarehouseId ?? -1}=-1) 
                            union
                            select t1.Id as ProductId,
	                            ProductCode,
                                ProductName,
                                ProductTypeId,
                                t11.[Desc] as ProductTypeDesc,
                                ProductSize,
                                ProductThickness,
                                ApproximateWeight,
                                NumberInPackage,
                                ProductStandardId,
                                ProductStateId,
                                ProductMainUnitId,
                                t9.UnitName as ProductMainUnitDesc,
                                ProductSubUnitId,
                                t10.UnitName as productSubUnitDesc,
                                ExchangeRate,
                                Description,
                                t1.Created,
                                t1.IsActive,
                                BrandId,
                                t3.ProductBrandId,
                                t3.WarehouseId,
                                t3.ApproximateInventory,
                                t3.FloorInventory,
                                t3.MaxInventory,
                                t3.MinInventory,
                                t3.OrderPoint,
                                t4.Name as ProductBrandName,
								t5.Name as WarehouseName,
								t6.[Desc] as ProductStateDesc,
								t7.[Desc] as ProductStandardDesc,
								t8.Price as ProductPrice

                            from sepdb.Products as t1
                            join sepdb.ProductBrands as t2 on t1.Id=t2.ProductId
                            left join sepdb.ProductInventories as t3 on t2.Id=t3.ProductBrandId
                            left join sepdb.Brands as t4 on t4.Id=t2.BrandId
							left join sepdb.Warehouses as t5 on t3.WarehouseId=t5.Id 
							left join sepdb.ProductStates as t6 on t6.Id=t1.ProductStateId
							left join sepdb.ProductStandards as t7 on t7.Id=t1.ProductStandardId
							left join sepdb.ProductPrices as t8 on t8.ProductBrandId=t2.Id and t8.IsActive=1
							left join sepdb.ProductUnits as t9 on t9.Id=t1.ProductMainUnitId 
							left join sepdb.ProductUnits as t10 on t10.Id=t1.ProductSubUnitId 
                            left join sepdb.ProductTypes as t11 on t11.Id=t1.ProductTypeId
							where t1.IsActive=1 and
                                  (t5.WarehouseTypeId={warehouseTypeId ?? -1} or {warehouseTypeId ?? -1}=-1)
                                  and (t5.Id={WarehouseId ?? -1} or {WarehouseId ?? -1}=-1) ";

            using (var connection = _dapperContext.CreateConnection())
            {
                var products = await connection.QueryAsync<DapperProduct>(query);
                return products.OrderByDescending(p => p.ApproximateInventory).ToList();
            }
        }

        public async Task CreateInventoryHistory(List<ProductInventoryHistory> inventoryHistoryList)
        {
            await _inventoryHistories.AddRangeAsync(inventoryHistoryList);
        }

        public async Task<ProductInventoryHistory?> CheckInventoryUploadHsitory(int productBrandId)
        {
            return await _inventoryHistories
                .FirstOrDefaultAsync(i => i.productBrandId == productBrandId);
        }

        public async Task<List<ProductInventoryHistory>?> GetUploadedInventoryFromHistory(string uploadedDate)
        {
            return _inventoryHistories.Where(p =>
            p.priceDate == uploadedDate).ToList();
        }

        public async Task<List<ProductInventory>?> GetProductInventory(List<OrderDetailRequest> detailDtos)
        {
            List<ProductInventory> _prodInvents = new List<ProductInventory>();
            foreach (var item in detailDtos)
            {
                var p = await _productInventory
                    .FirstOrDefaultAsync(i => i.ProductBrandId == item.ProductBrandId &&
                i.WarehouseId == item.WarehouseId && i.Warehouse.WarehouseTypeId == 2);

                if (p != null)
                    _prodInvents.Add(p);
            }
            return _prodInvents;
        }

        /// <summary>
        /// ایجاد رکورد موجودی به ازای همه انبارهای رسمی برای محصول جدید
        /// </summary>
        public async Task CreateInventoryToNewProduct(Guid productId)
        {
            var all_offWHouses = await _warehouses.Where(w=>w.WarehouseTypeId==(int)EWarehouseType.Rasmi).ToListAsync();
            var prod = await _products.FindAsync(productId);
            foreach (var item in all_offWHouses)
            {
                var newInv = _mapper.Map<OfficialWarehoseInventory>(prod);
                newInv.WarehouseId = item.Id;
                await _offWHouseInventory.AddAsync(newInv);
            }
        }

        /// <summary>
        /// ایجاد رکورد موجودی به ازای همه انبارها به جز انبارهای رسمی برای محصول برند جدید
        /// </summary>
        public async Task CreateInventoryToNewProductBrand(int brandId)
        {
            try
            {
                List<ProductInventory> productInventories = new List<ProductInventory>();

                var all_warehouses = await _warehouses.Where(w => w.WarehouseTypeId != (int)EWarehouseType.Rasmi).ToListAsync();
                var prod = await _productBrands.FindAsync(brandId);
                foreach (var item in all_warehouses)
                {
                    var newInv = _mapper.Map<ProductInventory>(prod);
                    //newInv.Id = null;
                    newInv.WarehouseId = item.Id;
                    productInventories.Add(newInv);
                }

                await _productInventory.AddRangeAsync(productInventories);
            }
            catch (Exception e)
            {

                throw;
            }
        }
    }
}