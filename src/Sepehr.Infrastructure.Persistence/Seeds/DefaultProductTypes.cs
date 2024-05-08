using Microsoft.EntityFrameworkCore;
using Sepehr.Domain.Entities;
using Sepehr.Infrastructure.Persistence.Context;

namespace Sepehr.Infrastructure.Persistence.Seeds
{
    public static class DefaultProductTypes
    {
        public static async Task
        SeedAsync(ApplicationDbContext applicationDbContext)
        {
            //Seed Default User
            var pTypes =
                new List<ProductType> {
                    new ProductType{Desc="نبشی",ProductCodeSeedStart=1400},
                    new ProductType{Desc="میلگرد",ProductCodeSeedStart=1000},
                    new ProductType{Desc="ناودانی",ProductCodeSeedStart=1600},
                    new ProductType{Desc="تیرآهن", ProductCodeSeedStart = 2200},
                    new ProductType{Desc="تسمه", ProductCodeSeedStart = 1800},
                    new ProductType{Desc="چهارپهلو", ProductCodeSeedStart = 3600},
                    new ProductType{Desc="سپری", ProductCodeSeedStart = 2000},
                    new ProductType{Desc="قوطی پروفیل", ProductCodeSeedStart = 3200},
                    new ProductType{Desc="لوله", ProductCodeSeedStart = 3400},
                    new ProductType{Desc="مش", ProductCodeSeedStart = 3800},
                    new ProductType{Desc="میلگرد ساده", ProductCodeSeedStart = 1200},
                    new ProductType{Desc="هاش", ProductCodeSeedStart = 2400},
                    new ProductType{Desc="ورق روغنی", ProductCodeSeedStart = 3000},
                    new ProductType{Desc="ورق سیاه", ProductCodeSeedStart = 2600},
                    new ProductType{Desc="ورق گالوانیزه", ProductCodeSeedStart = 2800},
                    new ProductType{Desc="سایر", ProductCodeSeedStart = 4000},
                };

            foreach (var item in pTypes)
            {
                if (!applicationDbContext.ProductTypes.Where(b=>b.Desc.Equals(item.Desc)).Any())
                {
                    applicationDbContext.ProductTypes.Add(item);
                }
                                
                await applicationDbContext.SaveChangesAsync();
            }
        }
    }
}
