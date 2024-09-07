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
                    new InvoiceType{Id=1,TypeDesc="فاکتور مهفام"},
                    new InvoiceType{Id=2,TypeDesc="بازرگانی"},
                    new InvoiceType{Id=3,TypeDesc="فاکتور سپهر"}
                };

            foreach (var item in brands)
            {
                var inv =await applicationDbContext.InvoiceTypes.FirstOrDefaultAsync(b => b.Id.Equals(item.Id));
                if (inv==null)
                {
                    applicationDbContext.InvoiceTypes.Add(item);
                }else
                {
                    inv.TypeDesc=item.TypeDesc;
                    applicationDbContext.InvoiceTypes.Update(inv);
                }
                                
                await applicationDbContext.SaveChangesAsync();
            }
        }
    }
}
