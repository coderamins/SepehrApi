using Microsoft.EntityFrameworkCore;
using Sepehr.Domain.Entities;
using Sepehr.Infrastructure.Persistence.Context;

namespace Sepehr.Infrastructure.Persistence.Seeds
{
    public static class DefaultBrands
    {
        public static async Task
        SeedAsync(ApplicationDbContext applicationDbContext)
        {
            //Seed Default User
            var brands =
                new List<Brand> {
                    new Brand{Name="برند 1"},
                    new Brand{Name="برند 2"},
                };

            foreach (var item in brands)
            {
                if (!applicationDbContext.Brands.Where(b=>b.Name.Equals(item.Name)).Any())
                {
                    applicationDbContext.Brands.Add(item);
                }
                                
                await applicationDbContext.SaveChangesAsync();
            }
        }
    }
}
