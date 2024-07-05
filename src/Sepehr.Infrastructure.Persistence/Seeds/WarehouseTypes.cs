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
                    new WarehouseType{Description="عادی"},
                    new WarehouseType{Description="واسطه"},
                    new WarehouseType{Description="امانی"},
                    new WarehouseType{Description="مبادی"},
                    new WarehouseType{Description="رسمی"},
                    new WarehouseType{Description="خرید"}
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
