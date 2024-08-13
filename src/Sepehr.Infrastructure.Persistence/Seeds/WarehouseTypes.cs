using Microsoft.EntityFrameworkCore;
using Sepehr.Domain.Entities;
using Sepehr.Infrastructure.Persistence.Context;

namespace Sepehr.Infrastructure.Persistence.Seeds
{
    public static class WarehouseTypes
    {
        public static async Task
        SeedAsync(ApplicationDbContext applicationDbContext)
        {
            //Seed Default User
            var warehouses =
                new List<WarehouseType> {
                    new WarehouseType{Id=1,Description="واسطه"},
                    new WarehouseType{Id=2,Description="امانی"},
                    new WarehouseType{Id=3,Description="مبادی"},
                    new WarehouseType{Id=4,Description="رسمی"},
                    new WarehouseType{Id=5,Description="عادی"},
                    new WarehouseType{Id=6,Description="خرید"}
                };

            foreach (var item in warehouses)
            {
                var w = await applicationDbContext.WarehouseTypes.FirstOrDefaultAsync(w => new int[] { 8, 13 }.Contains(w.Id));
                if (w != null)
                    applicationDbContext.WarehouseTypes.Remove(w);

                var whouse =await applicationDbContext.WarehouseTypes.FirstOrDefaultAsync(b => b.Id.Equals(item.Id));
                if (whouse==null)
                    applicationDbContext.WarehouseTypes.Add(item);
                else
                {
                    whouse.Description=item.Description;
                    applicationDbContext.WarehouseTypes.Update(whouse);
                }

                await applicationDbContext.SaveChangesAsync();
            }
        }
    }
}
