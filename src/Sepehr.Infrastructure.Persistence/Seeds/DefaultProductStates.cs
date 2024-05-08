using Microsoft.EntityFrameworkCore;
using Sepehr.Domain.Entities;
using Sepehr.Infrastructure.Persistence.Context;

namespace Sepehr.Infrastructure.Persistence.Seeds
{
    public static class ProductStates
    {
        public static async Task
        SeedAsync(ApplicationDbContext applicationDbContext)
        {
            //Seed Default User
            var Standards =
                new List<ProductState> {
                    new ProductState{Desc="12 متری"},
                    new ProductState{Desc="6 متری"},
                };

            foreach (var item in Standards)
            {
                if (!applicationDbContext.ProductStates.Where(b=>b.Desc.Equals(item.Desc)).Any())
                {
                    applicationDbContext.ProductStates.Add(item);
                }
                                
                await applicationDbContext.SaveChangesAsync();
            }
        }
    }
}
