using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Sepehr.Application.Interfaces.Repositories;
using Sepehr.Domain.Entities;
using Sepehr.Infrastructure.Persistence.Context;

namespace Sepehr.Infrastructure.Persistence.Repositories
{
    public class WarehouseRepositoryAsync : GenericRepositoryAsync<Warehouse>, IWarehouseRepositoryAsync
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly DbSet<Warehouse> _warehouses;
        private readonly DbSet<ProductBrand> _productBrandRepo;
        private readonly DbSet<Product> _productRepo;
        private readonly DbSet<ProductInventory> _productInventoryRepo;
        private readonly DbSet<OfficialWarehoseInventory> _offProductInventoryRepo;
        private readonly IMapper _mapper;

        public WarehouseRepositoryAsync(ApplicationDbContext dbContext,
            IMapper mapper) : base(dbContext)
        {
            _warehouses = dbContext.Set<Warehouse>();
            _productBrandRepo = dbContext.Set<ProductBrand>();
            _productRepo = dbContext.Set<Product>();
            _productInventoryRepo = dbContext.Set<ProductInventory>();
            _offProductInventoryRepo = dbContext.Set<OfficialWarehoseInventory>();
            _mapper = mapper;
            _dbContext = dbContext;
        }

        public async Task<Warehouse> CreateWarehouse(Warehouse warehouse)
        {
            try
            {
                var newWhouse = await _warehouses.AddAsync(warehouse);
                await _dbContext.SaveChangesAsync();

                var allProds = await _productRepo.ToListAsync();
                var allProdBrans = await _productBrandRepo.ToListAsync();

                foreach (var item in allProdBrans)
                {
                    var newInv = _mapper.Map<ProductInventory>(item);
                    newInv.WarehouseId = newWhouse.Entity.Id;
                    await _productInventoryRepo.AddAsync(newInv);
                }

                foreach (var item in allProds)
                {
                    var newInv = _mapper.Map<OfficialWarehoseInventory>(item);
                    newInv.WarehouseId = newWhouse.Entity.Id;
                    await _offProductInventoryRepo.AddAsync(newInv);
                }

                await _dbContext.SaveChangesAsync();

                return newWhouse.Entity;
            }
            catch (Exception e)
            {

                throw;
            }
        }

        public async Task<List<Warehouse>> GetAllWarehousesAsync(int? WarehouseTypeId, Guid? CustomerId)
        {
            return await _warehouses
                .Include(w => w.WarehouseType)
                .Include(w=>w.CustomerWarehouses).ThenInclude(w => w.Customer)
                .Where(w=> w.WarehouseTypeId==WarehouseTypeId || WarehouseTypeId==null) 
                .OrderByDescending(p => p.Id).ToListAsync();
        }

        public async Task<Warehouse?> GetWarehouseInfo(string warehousename)
        {
            return await _warehouses
                .Include(w => w.CustomerWarehouses).ThenInclude(w => w.Customer)
                .Include(w => w.WarehouseType)
                .FirstOrDefaultAsync(p => p.Name == warehousename);
        }

        public async Task<Warehouse?> GetWarehouseInfo(int Id)
        {
            return await _warehouses
                .Include(w => w.CustomerWarehouses).ThenInclude(w => w.Customer)
                .Include(w => w.WarehouseType)
                .FirstOrDefaultAsync(p => p.Id == Id);
        }
    }
}