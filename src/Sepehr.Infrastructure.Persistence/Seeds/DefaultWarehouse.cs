using Microsoft.EntityFrameworkCore;
using Sepehr.Domain.Entities;
using Sepehr.Infrastructure.Persistence.Context;

namespace Sepehr.Infrastructure.Persistence.Seeds
{
    public static class DefaultWarehouse
    {
        public static async Task
        SeedAsync(ApplicationDbContext applicationDbContext)
        {
            //Seed Default User
            List<Warehouse> defaultWarehouse =
                new List<Warehouse> {
                new Warehouse { Name = "انبار سپهر",WarehouseTypeId=2 },//1
                new Warehouse { Name = "انبار مهفام",WarehouseTypeId=5 },//2
                new Warehouse { Name = "انبار واسط",WarehouseTypeId=2 },
                new Warehouse { Name = "انبار امانی",WarehouseTypeId=3 },
                new Warehouse { Name = "انبار بازرگانی",WarehouseTypeId=1 },
                new Warehouse { Name = "انبار مبادی",WarehouseTypeId=4 },
                new Warehouse { Name = "انبار خرید(مجازی)",WarehouseTypeId=5 }
                };

            foreach (var item in defaultWarehouse)
            {
                if (applicationDbContext.Warehouses.All(u => u.Name != item.Name))
                {
                    await applicationDbContext.Warehouses.AddAsync(item);
                    await applicationDbContext.SaveChangesAsync();
                }
            }

        }
    }
}
