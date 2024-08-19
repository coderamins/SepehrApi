using Microsoft.EntityFrameworkCore;
using Sepehr.Domain.Entities;
using Sepehr.Domain.Enums;
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
                new Warehouse { Name = "انبار سپهر",WarehouseTypeId=(int)EWarehouseType.Factory },
                new Warehouse { Name = "انبار مهفام",WarehouseTypeId=(int)EWarehouseType.Rasmi },
                new Warehouse { Name = "انبار واسط",WarehouseTypeId=(int)EWarehouseType.Vaseteh },
                new Warehouse { Name = "انبار امانی",WarehouseTypeId=(int)EWarehouseType.Amani },
                new Warehouse { Name = "انبار بازرگانی",WarehouseTypeId=(int)EWarehouseType.Addi },
                new Warehouse { Name = "انبار مبادی",WarehouseTypeId=(int)EWarehouseType.Mabadi },
                };

            foreach (var item in defaultWarehouse)
            {
                var wh = await applicationDbContext.Warehouses.FirstOrDefaultAsync(u => u.Name == item.Name);
                if (wh == null)
                    await applicationDbContext.Warehouses.AddAsync(item);
                else
                {
                    wh.WarehouseTypeId = item.WarehouseTypeId;
                    applicationDbContext.Warehouses.Update(wh);
                }

                await applicationDbContext.SaveChangesAsync();

            }

        }
    }
}
