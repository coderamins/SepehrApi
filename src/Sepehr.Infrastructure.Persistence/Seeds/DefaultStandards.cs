using Microsoft.EntityFrameworkCore;
using Sepehr.Domain.Entities;
using Sepehr.Infrastructure.Persistence.Context;

namespace Sepehr.Infrastructure.Persistence.Seeds
{
    public static class DefaultStandards
    {
        public static async Task
        SeedAsync(ApplicationDbContext applicationDbContext)
        {
            //Seed Default User
            var Standards =
                new List<ProductStandard> {
                    new ProductStandard{Desc="A1"},
                    new ProductStandard{Desc="A2"},
                };

            foreach (var item in Standards)
            {
                if (!applicationDbContext.ProductStandards.Where(b=>b.Desc.Equals(item.Desc)).Any())
                {
                    applicationDbContext.ProductStandards.Add(item);
                }
                                
                await applicationDbContext.SaveChangesAsync();
            }
        }
    }
}
