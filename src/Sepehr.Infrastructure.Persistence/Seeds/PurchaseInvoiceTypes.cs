using Microsoft.EntityFrameworkCore;
using Sepehr.Domain.Entities;
using Sepehr.Infrastructure.Persistence.Context;

namespace Sepehr.Infrastructure.Persistence.Seeds
{
    public static class PurchaseInvoiceTypes
    {
        public static async Task
        SeedAsync(ApplicationDbContext applicationDbContext)
        {
            //Seed Default User
            var brands =
                new List<PurchaseInvoiceType> {
                    new PurchaseInvoiceType{Desc="فاکتور رسمی"},
                    new PurchaseInvoiceType{Desc="فاکتور غیررسمی "}
                };

            foreach (var item in brands)
            {
                if (!applicationDbContext.PurchaseInvoiceTypes.Where(b=>b.Desc.Equals(item.Desc)).Any())
                {
                    applicationDbContext.PurchaseInvoiceTypes.Add(item);
                }
                                
                await applicationDbContext.SaveChangesAsync();
            }
        }
    }
}
