using Microsoft.EntityFrameworkCore;
using Sepehr.Domain.Entities;
using Sepehr.Infrastructure.Persistence.Context;

namespace Sepehr.Infrastructure.Persistence.Seeds
{
    public static class OrderSendTypes
    {
        public static async Task
        SeedAsync(ApplicationDbContext applicationDbContext)
        {
            //Seed Default User
            var brands =
                new List<OrderSendType> {
                    new OrderSendType{Description="توسط مشتری"},
                    new OrderSendType{Description="توسط بازرگانی"}
                };

            foreach (var item in brands)
            {
                if (!applicationDbContext.OrderSendTypes.Where(b=>b.Description.Equals(item.Description)).Any())
                {
                    applicationDbContext.OrderSendTypes.Add(item);
                }
                                
                await applicationDbContext.SaveChangesAsync();
            }
        }
    }
}
