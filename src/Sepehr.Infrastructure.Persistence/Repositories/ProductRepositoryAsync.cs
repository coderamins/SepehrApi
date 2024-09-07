using Dapper;
using MailKit.Search;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Sepehr.Application.DTOs.Order;
using Sepehr.Application.Exceptions;
using Sepehr.Application.Features.Products.Queries.GetAllProducts;
using Sepehr.Application.Interfaces.Repositories;
using Sepehr.Domain.Entities;
using Sepehr.Domain.Enums;
using Sepehr.Domain.ViewModels;
using Sepehr.Infrastructure.Persistence.Context;
using Stimulsoft.System.Data.Sql;
using System.Text.RegularExpressions;

namespace Sepehr.Infrastructure.Persistence.Repositories
{
    public class ProductRepositoryAsync : GenericRepositoryAsync<Product>, IProductRepositoryAsync
    {
        private readonly DbSet<Product> _products;
        private readonly DbSet<ProductBrand> _productBrands;
        private readonly DbSet<ProductType> _productTypes;
        private readonly DbSet<TransferRemittance> _transRemittances;
        private readonly DbSet<ProductInventory> _productInventory;
        private readonly DapperContext _dapContext;
        private readonly ApplicationDbContext _dbContext;

        public ProductRepositoryAsync(ApplicationDbContext dbContext,
            DapperContext dapContext) : base(dbContext)
        {
            _products = dbContext.Set<Product>();
            _productBrands = dbContext.Set<ProductBrand>();
            _productTypes = dbContext.Set<ProductType>();
            _transRemittances = dbContext.Set<TransferRemittance>();
            _productInventory = dbContext.Set<ProductInventory>();
            _dbContext = dbContext;
            _dapContext = dapContext;
        }

        public async Task<bool> DisableProduct(Guid id)
        {
            var product = await _products.SingleOrDefaultAsync(p => p.Id == id);
            if (product == null) { throw new ApiException("محصول یافت نشد !"); }

            product.IsActive = false;
            return true;
        }

        public async Task<bool> EnableProduct(Guid id)
        {
            var product = await _products.SingleOrDefaultAsync(p => p.Id == id);
            if (product == null) { throw new ApiException("محصول یافت نشد !"); }

            product.IsActive = true;
            return true;
        }

        public async Task<List<Product>> GetAllProducts(GetAllProductsParameter filter)
        {
            var products = _products
                .Include(c => c.ProductPrices).ThenInclude(p => p.ProductBrand)
                .Include(p => p.ProductInventories).ThenInclude(p => p.Warehouse).ThenInclude(p => p.WarehouseType)
                .Include(p => p.ProductType)
                .Include(p => p.ProductMainUnit)
                .Include(p => p.ProductSubUnit)
                .Include(p => p.ProductStandard)
                .Include(p => p.ProductState)
                .Include(p => p.ProductBrands).ThenInclude(b => b.Brand)
                .Where(p => p.IsActive &&
                (p.ProductName.Contains(filter.ProductName) || string.IsNullOrEmpty(filter.ProductName)) &&
                (p.ProductInventories.Any(i => i.WarehouseId == filter.WarehouseId) || filter.WarehouseId == null) &&
                (p.ProductInventories.Any(i => i.Warehouse.WarehouseTypeId == filter.WarehouseTypeId) || filter.WarehouseTypeId == null) &&
                (p.ProductTypeId == filter.ProductTypeId || filter.ProductTypeId == null)
                )
                .OrderByDescending(p => p.Created).AsQueryable();

            var result =
                filter.productSortBase == ProductSortBase.ByLowInventory ?
                            await products.OrderBy(p => p.ProductInventories.Sum(i => i.ApproximateInventory)).ToListAsync() :
                filter.productSortBase == ProductSortBase.ByHighInventory ?
                            await products.OrderByDescending(p => p.ProductInventories.Sum(i => i.ApproximateInventory)).ToListAsync() :
                            await products.ToListAsync();


            return result;

        }

        public async Task<IEnumerable<DapperProduct>> GetAllProductsByInventory(GetAllProductsParameter filter)
        {
            var words = Regex.Split(filter.Keyword ?? "", @"\W+").Where(w => !string.IsNullOrEmpty(w));


            var query = $@"select t1.Id as ProductId,
	                            ProductCode,
                                ProductName,
                                ProductTypeId,
                                t11.[Desc] as ProductTypeDesc,
                                ProductSize,
                                ProductThickness,
                                ApproximateWeight,
                                t1.ProductStandardId,
                                t1.ProductStateId,
                                t1.ProductMainUnitId,
                                t9.UnitName as ProductMainUnitDesc,
                                ProductSubUnitId,
                                t10.UnitName as productSubUnitDesc,
                                ExchangeRate,
                                t1.Description,
                                t1.Created,
                                t1.IsActive,
                                0 as BrandId,
                                0 as ProductBrandId,
                                t3.WarehouseId,
                                t3.ApproximateInventory,
                                0 as OnTransitInventory,
                                t3.FloorInventory,
                                t3.MaxInventory,
                                t3.MinInventory,
                                t3.OrderPoint,
                                '' as ProductBrandName,
								t5.Name as WarehouseName,
								t6.[Desc] as ProductStateDesc,
								t7.[Desc] as ProductStandardDesc,
								0 as ProductPrice,
                                t5.WarehouseTypeId,
                                0 as PurchaseInventory
                            from sepdb.Products as t1
                            left join sepdb.OfficialWarehoseInventories as t3 on t3.ProductId=t1.Id
							left join sepdb.Warehouses as t5 on t3.WarehouseId=t5.Id 
							left join sepdb.ProductStates as t6 on t6.Id=t1.ProductStateId
							left join sepdb.ProductStandards as t7 on t7.Id=t1.ProductStandardId
							left join sepdb.ProductUnits as t9 on t9.Id=t1.ProductMainUnitId 
							left join sepdb.ProductUnits as t10 on t10.Id=t1.ProductSubUnitId 
                            left join sepdb.ProductTypes as t11 on t11.Id=t1.ProductTypeId
							where t1.IsActive=1 and t5.WarehouseTypeId={(int)EWarehouseType.Rasmi} and
                                  (t1.ProductName like N'%{filter.ProductName}%' or {(string.IsNullOrEmpty(filter.ProductName) ? '1' : '0')}=1) and
                                  (t5.Id={filter.WarehouseId ?? -1} or {filter.WarehouseId ?? -1}=-1) and
                                  (t5.WarehouseTypeId={filter.WarehouseTypeId ?? -1} or {filter.WarehouseTypeId ?? -1}=-1) and
                                  (t1.ProductTypeId={filter.ProductTypeId ?? -1} or {filter.ProductTypeId ?? -1}=-1) 
                                  {(filter.HasPurchaseInventory == true ? " and 1=-1" : "")} /*اگر محصولات دارای موجودی خرید درخواست شود از قسمت اول یونیون چشم پوشی می شود*/ ";

            var whereClauses = new List<string>();
            foreach (var word in words)
            {
                whereClauses.Add($" t1.ProductName LIKE N'%{word}%'");
            }

            string finalWhereClause = "";
            if (!string.IsNullOrEmpty(filter.Keyword))
            {
                finalWhereClause = string.Join(" OR ", whereClauses);
                query = string.Concat(query, " OR ", finalWhereClause);
            }

            query = string.Concat(query, " union ");

            query += $@" select  t1.Id as ProductId,
                                    t1.ProductCode,
                                    ProductName,
                                    ProductTypeId,
                                    t11.[Desc] as ProductTypeDesc,
                                    ProductSize,
                                    ProductThickness,
                                    ApproximateWeight,
                                    t1.ProductStandardId,
                                    t1.ProductStateId,
                                    t1.ProductMainUnitId,
                                    t9.UnitName as ProductMainUnitDesc,
                                    t1.ProductSubUnitId,
                                    t10.UnitName as productSubUnitDesc,
                                    ExchangeRate,                                
                                    t1.Description,
                                    t1.Created,
                                    t1.IsActive,
                                    BrandId,
                                    t3.ProductBrandId,
                                    t3.WarehouseId,
                                    t3.ApproximateInventory,
                                    t3.OnTransitInventory,
                                    t3.FloorInventory,
                                    t3.MaxInventory,
                                    t3.MinInventory,
                                    t3.OrderPoint,
                                    t4.Name as ProductBrandName,
				                                t5.Name as WarehouseName,
				                                t6.[Desc] as ProductStateDesc,
				                                t7.[Desc] as ProductStandardDesc,
				                                t8.Price as ProductPrice,
                                    t5.WarehouseTypeId,
                                    t3.PurchaseInventory
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
                            {(filter.OrderCode == null ? "" :
                            $@"join sepdb.PurchaseOrderDetails as t12 on t12.ProductBrandId=t2.Id
                              join sepdb.PurchaseOrder as t13 on t13.Id=t12.OrderId and OrderCode={filter.OrderCode ?? -1}")}
							where t1.IsActive=1 and
                                  (t1.ProductName like N'%{filter.ProductName}%' or {(string.IsNullOrEmpty(filter.ProductName) ? '1' : '0')}=1) and
                                  (t5.Id={filter.WarehouseId ?? -1} or {filter.WarehouseId ?? -1}=-1) and
                                  (t5.WarehouseTypeId={filter.WarehouseTypeId ?? -1} or {filter.WarehouseTypeId ?? -1}=-1) and
                                  (t1.ProductTypeId={filter.ProductTypeId ?? -1} or {filter.ProductTypeId ?? -1}=-1)                                   
                                  {(filter.HasPurchaseInventory == true ? " and t3.PurchaseInventory>0" : "")}";

            whereClauses = new List<string>();
            foreach (var word in words)
            {
                whereClauses.Add($" t1.ProductName LIKE N'%{word}%' OR t4.Name LIKE N'%{word}%' OR t11.[Desc] LIKE N'%{word}%'");
            }


            finalWhereClause = "";
            if (!string.IsNullOrEmpty(filter.Keyword))
            {
                finalWhereClause = string.Join(" OR ", whereClauses);
                query = string.Concat(query, " OR ", finalWhereClause);
            }

            using (var connection = _dapContext.CreateConnection())
            {
                //var products = await connection.QueryAsync<DapperProduct>(query);
                //return products.OrderBy(x=>x.ProductName).OrderBy(p => p.ProductBrandName).OrderBy(x=>x.ProductTypeDesc).ToList();

                var products = await connection.QueryAsync<DapperProduct>(query);
                var results = products.Select(r => new
                {
                    Result = r,
                    Score = words.Count(w => r.ProductName.Contains(w) || r.ProductBrandName.Contains(w) || r.ProductTypeDesc.Contains(w))
                });

                // مرتب‌سازی نتایج بر اساس امتیاز
                return results.OrderByDescending(r => r.Score).Select(r => r.Result);

            }

        }

        public async Task<List<ProductType>> GetProductTypes()
        {
            return await _productTypes.ToListAsync();
        }

        public async Task<Product?> GetProductInfoAsync(long productCode)
        {
            return await _products
                .Include(c => c.ProductPrices).ThenInclude(p => p.ProductBrand)
                .Include(p => p.ProductInventories).ThenInclude(p => p.Warehouse).ThenInclude(p => p.WarehouseType)
                .Include(p => p.ProductType)
                .Include(p => p.ProductMainUnit)
                .Include(p => p.ProductSubUnit)
                .Include(p => p.ProductStandard)
                .Include(p => p.ProductState)
                .Include(p => p.ProductBrands).ThenInclude(b => b.Brand)
                .Where(p => p.IsActive)
                .FirstOrDefaultAsync(p => p.ProductCode == productCode);
        }

        public Task<bool> IsUniqueBarcodeAsync(string barcode)
        {
            return _products
                .AllAsync(p => p.Barcode != barcode);
        }

        public async Task<int> GenerateNewProductCode(int? productTypeId)
        {
            var pType = await _productTypes.FirstOrDefaultAsync(p => p.Id == productTypeId);
            if (pType == null)
                throw new ApiException("نوع محصول یافت نشد !");

            var lastProduct = await _products.Where(p => p.ProductTypeId == productTypeId)
                .OrderByDescending(p => p.ProductCode).FirstOrDefaultAsync();

            if (lastProduct == null)
                return pType.ProductCodeSeedStart + 1;
            else
                return (lastProduct.ProductCode > pType.ProductCodeSeedStart ? lastProduct.ProductCode + 1 : pType.ProductCodeSeedStart + 1);

        }

        public Task<List<ProductBrand>> GetProductBrands(List<OrderDetailRequest> details)
        {
            return _productBrands
                .Include(b => b.ProductInventories).ThenInclude(i => i.Warehouse)
                .Where(p => details.Select(d => d.ProductBrandId).Contains(p.Id))
                .ToListAsync();
        }
    }
}