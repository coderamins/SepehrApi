using Microsoft.EntityFrameworkCore;
using Sepehr.Domain.Entities;
using Sepehr.Infrastructure.Persistence.Context;

namespace Sepehr.Infrastructure.Persistence.Seeds
{
    public static class InvoiceTypes
    {
        public static async Task
        SeedAsync(ApplicationDbContext applicationDbContext)
        {
            //Seed Default User
            var brands =
                new List<InvoiceType> {
                    new InvoiceType{Id=1,TypeDesc="رسمی مهفام"},
                    new InvoiceType{Id=2,TypeDesc="رسمی سپهر"},
                    new InvoiceType{Id=3,TypeDesc="غیر رسمی بازرگانی"}

                };

            foreach (var item in brands)
            {
                if (!applicationDbContext.InvoiceTypes.Where(b=>b.TypeDesc.Equals(item.TypeDesc)).Any())
                {
                    applicationDbContext.InvoiceTypes.Add(item);
                }
                                
                await applicationDbContext.SaveChangesAsync();
            }
        }
    }
}
