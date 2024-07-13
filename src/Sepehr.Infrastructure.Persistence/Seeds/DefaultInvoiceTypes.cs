using Microsoft.EntityFrameworkCore;
using Sepehr.Domain.Entities;
using Sepehr.Infrastructure.Persistence.Context;

namespace Sepehr.Infrastructure.Persistence.Seeds
{
    public static class DefaultInvoiceTypes
    {
        public static async Task
        SeedAsync(ApplicationDbContext applicationDbContext)
        {
            //Seed Default User
            var brands =
                new List<InvoiceType> {
                    new InvoiceType{Id=1,TypeDesc="غیر رسمی"},
                    new InvoiceType{Id=2,TypeDesc="رسمی"}
                    //new InvoiceType{Id=3,TypeDesc="غیررسمی بازرگانی"},
                    //new InvoiceType{Id=4,TypeDesc="غیررسمی"},
                };

            foreach (var item in brands)
            {
                if (!applicationDbContext.InvoiceTypes.Where(b=>b.Id.Equals(item.Id)).Any())
                {
                    applicationDbContext.InvoiceTypes.Add(item);
                }
                                
                await applicationDbContext.SaveChangesAsync();
            }
        }
    }
}
