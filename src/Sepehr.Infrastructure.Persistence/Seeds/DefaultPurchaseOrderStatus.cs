using Microsoft.EntityFrameworkCore;
using Sepehr.Domain.Entities;
using Sepehr.Infrastructure.Persistence.Context;

namespace Sepehr.Infrastructure.Persistence.Seeds
{
    public static class DefaultPurchaseOrderStatus
    {
        public static async Task
        SeedAsync(ApplicationDbContext applicationDbContext)
        {
            //Seed Default User
            var brands =
                new List<PurchaseOrderStatus> {
                    new PurchaseOrderStatus{Id=1,StatusDesc="جدید"},
                    new PurchaseOrderStatus{Id=2,StatusDesc="تایید شده حسابداری"},
                    new PurchaseOrderStatus{Id=3,StatusDesc="تایید نشده حسابداری"},
                    new PurchaseOrderStatus{Id=4,StatusDesc="انتقال داده شده به انبار"},
                    new PurchaseOrderStatus{Id=5,StatusDesc="ابطال شده"},
                    new PurchaseOrderStatus{Id=6,StatusDesc="برگشت خورده"},
                };

            foreach (var item in brands)
            {
                if (!applicationDbContext.PurchaseOrderStatus.Where(b=>b.StatusDesc.Equals(item.StatusDesc)).Any())
                {
                    applicationDbContext.PurchaseOrderStatus.Add(item);
                }
                                
                await applicationDbContext.SaveChangesAsync();
            }
        }
    }
}
