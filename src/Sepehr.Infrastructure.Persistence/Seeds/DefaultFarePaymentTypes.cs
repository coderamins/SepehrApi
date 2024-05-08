using Microsoft.EntityFrameworkCore;
using Sepehr.Domain.Entities;
using Sepehr.Infrastructure.Persistence.Context;

namespace Sepehr.Infrastructure.Persistence.Seeds
{
    public static class DefaultFarePaymentTypes
    {
        public static async Task
        SeedAsync(ApplicationDbContext applicationDbContext)
        {
            //Seed Default User
            var brands =
                new List<FarePaymentType> {
                    new FarePaymentType{Desc="کرایه با خودمان"},
                    new FarePaymentType{Desc="کرایه با مشتری"},
                };

            foreach (var item in brands)
            {
                if (!applicationDbContext.PaymentTypes.Where(b=>b.Desc.Equals(item.Desc)).Any())
                {
                    applicationDbContext.PaymentTypes.Add(item);
                }
                                
                await applicationDbContext.SaveChangesAsync();
            }
        }
    }
}
