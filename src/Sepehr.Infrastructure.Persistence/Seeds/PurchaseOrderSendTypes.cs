using Microsoft.EntityFrameworkCore;
using Sepehr.Domain.Entities;
using Sepehr.Infrastructure.Persistence.Context;

namespace Sepehr.Infrastructure.Persistence.Seeds
{
    public static class DefaultPurchaseOrderSendType
    {
        public static async Task SeedAsync(
            ApplicationDbContext applicationDbContext)
        {
            var orderSendTypes =
                new List<PurchaseOrderSendType> {
                    new PurchaseOrderSendType{Id=1,SendTypeDesc="توسط فروشنده"},
                    new PurchaseOrderSendType{Id=2,SendTypeDesc="توسط خودمان"}
                };

            foreach (var item in orderSendTypes)
            {
                if (!applicationDbContext.PurchaseOrderSendTypes.Any(b=>b.Id.Equals(item.Id)))
                {
                    applicationDbContext.PurchaseOrderSendTypes.Add(item);
                }
                                
                await applicationDbContext.SaveChangesAsync();
            }
        }
    }
}
