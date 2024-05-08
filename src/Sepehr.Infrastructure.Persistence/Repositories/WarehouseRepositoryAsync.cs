using Microsoft.EntityFrameworkCore;
using Sepehr.Application.Interfaces.Repositories;
using Sepehr.Domain.Entities;
using Sepehr.Infrastructure.Persistence.Context;

namespace Sepehr.Infrastructure.Persistence.Repositories
{
    public class WarehouseRepositoryAsync : GenericRepositoryAsync<Warehouse>, IWarehouseRepositoryAsync
    {
        private readonly DbSet<Warehouse> _warehouses;

        public WarehouseRepositoryAsync(ApplicationDbContext dbContext) : base(dbContext)
        {
            _warehouses = dbContext.Set<Warehouse>();
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