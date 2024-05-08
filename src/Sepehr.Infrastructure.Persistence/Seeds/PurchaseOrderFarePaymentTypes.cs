using Microsoft.EntityFrameworkCore;
using Sepehr.Domain.Entities;
using Sepehr.Infrastructure.Persistence.Context;

namespace Sepehr.Infrastructure.Persistence.Seeds
{
    public static class DefaultPurchaseOrderFarePaymentTypes
    {
        public static async Task
        SeedAsync(ApplicationDbContext applicationDbContext)
        {
            //Seed Default User
            var brands =
                new List<PurchaseOrderFarePaymentType> {
                    new PurchaseOrderFarePaymentType{Id=1,TypeDesc="کرایه با خودمان"},
                    new PurchaseOrderFarePaymentType{Id=2,TypeDesc="کرایه با فروشنده"},
                };

            foreach (var item in brands)
            {
                if (!applicationDbContext.PurchaseOrderFarePaymentTypes.Where(b=>b.Id.Equals(item.Id)).Any())
                {
                    applicationDbContext.PurchaseOrderFarePaymentTypes.Add(item);
                }
                                
                await applicationDbContext.SaveChangesAsync();
            }
        }
    }
}
