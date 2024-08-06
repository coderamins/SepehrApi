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
            var brands =
                new List<WarehouseType> {
                    new WarehouseType{Id=1,Description="واسطه"},
                    new WarehouseType{Id=2,Description="امانی"},
                    new WarehouseType{Id=3,Description="مبادی"},
                    new WarehouseType{Id=4,Description="رسمی"},
                    new WarehouseType{Id=5,Description="عادی"},
                    new WarehouseType{Id=6,Description="خرید"}
                };

            foreach (var item in brands)
            {
                if (!applicationDbContext.WarehouseTypes.Where(b=>b.Description.Equals(item.Description)).Any())
                {
                    applicationDbContext.WarehouseTypes.Add(item);
                }
                                
                await applicationDbContext.SaveChangesAsync();
            }
        }
    }
}
