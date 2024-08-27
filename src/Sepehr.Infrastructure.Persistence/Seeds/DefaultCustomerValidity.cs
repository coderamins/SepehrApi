using Microsoft.EntityFrameworkCore;
using Sepehr.Domain.Entities;
using Sepehr.Infrastructure.Persistence.Context;

namespace Sepehr.Infrastructure.Persistence.Seeds
{
    public static class DefaultCustomerValidity
    {
        public static async Task
        SeedAsync(ApplicationDbContext applicationDbContext)
        {
            //Seed Default User
            var brands =
                new List<CustomerValidity> {
                     new CustomerValidity{
                        Id = 1,
                        ValidityDesc= "عادی",
                        ColorCode= "F59AA1",
                        IsActive= true
                      },
                      new CustomerValidity{
                          Id = 2,
                        ValidityDesc= "نامطلوب",
                        ColorCode= "D12A37",
                        IsActive= true
                      },
                     new CustomerValidity {
                         Id = 3,
                        ValidityDesc= "طلائی",
                        ColorCode= "E6DD21",
                        IsActive= true
                      },
                      new CustomerValidity{
                          Id = 4,
                        ValidityDesc= "VIP",
                        ColorCode= "0EA31A",
                        IsActive= true
                      },
                      new CustomerValidity{
                          Id = 5,
                        ValidityDesc= "لیست سیاه",
                        ColorCode= "2A080B",
                        IsActive= true
  }
                };

            foreach (var item in brands)
            {
                if (!applicationDbContext.CustomerValidities.Where(b => b.ValidityDesc.Equals(item.ValidityDesc)).Any())
                {
                    applicationDbContext.CustomerValidities.Add(item);
                }

                await applicationDbContext.SaveChangesAsync();
            }
        }
    }
}
