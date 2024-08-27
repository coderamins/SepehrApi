using Microsoft.EntityFrameworkCore;
using Sepehr.Domain.Entities;
using Sepehr.Domain.Enums;
using Sepehr.Infrastructure.Persistence.Context;

namespace Sepehr.Infrastructure.Persistence.Seeds
{
    public static class DefaultCustomerTypes
    {
        public static async Task
        SeedAsync(ApplicationDbContext applicationDbContext)
        {
            //Seed Default User
            var brands =
                new List<CustomerValidity> {
                    new CustomerValidity{Id=1,ValidityDesc="عادی"},
                    new CustomerValidity{Id=2,ValidityDesc="نامطلوب"},
                    new CustomerValidity{Id=3,ValidityDesc="طلائی"},
                    new CustomerValidity{Id=4,ValidityDesc="VIP"},
                    new CustomerValidity{Id=5,ValidityDesc="لیست سیاه"},
                };

            foreach (var item in brands)
            {
                //if (!applicationDbContext.CustomerValidities.Where(b=>b.ValidityDesc.Equals(item.ValidityDesc)).Any())
                //{
                //    applicationDbContext.CustomerValidities.Add(item);
                //}
                                
                await applicationDbContext.SaveChangesAsync();
            }
        }
    }
}
