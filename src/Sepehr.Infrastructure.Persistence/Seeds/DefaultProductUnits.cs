using Microsoft.EntityFrameworkCore;
using Sepehr.Domain.Entities;
using Sepehr.Infrastructure.Persistence.Context;

namespace Sepehr.Infrastructure.Persistence.Seeds
{
    public static class DefaultProductUnits
    {
        public static async Task
        SeedAsync(ApplicationDbContext applicationDbContext)
        {
            //Seed Default User
            var pTypes =
                new List<ProductUnit> {
                    new ProductUnit{UnitName="بسته"},
                    new ProductUnit{UnitName="عدد"},
                    new ProductUnit{UnitName="شاخه"},
                    new ProductUnit{UnitName="کیلو"},
                };

            foreach (var item in pTypes)
            {
                if (!applicationDbContext.ProductUnits.Where(b=>b.UnitName.Equals(item.UnitName)).Any())
                {
                    applicationDbContext.ProductUnits.Add(item);
                }
                                
                await applicationDbContext.SaveChangesAsync();
            }
        }
    }
}
