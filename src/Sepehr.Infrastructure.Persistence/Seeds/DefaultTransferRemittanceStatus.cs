using Microsoft.EntityFrameworkCore;
using Sepehr.Domain.Entities;
using Sepehr.Infrastructure.Persistence.Context;

namespace Sepehr.Infrastructure.Persistence.Seeds
{
    public static class DefaultTransferRemittanceStatus
    {
        public static async Task
        SeedAsync(ApplicationDbContext applicationDbContext)
        {
            //Seed Default User
            var trstatus =
                new List<PurchaseOrderTransferRemittanceStatus> {
                    new PurchaseOrderTransferRemittanceStatus{Id=1, StatusDesc="در حال بررسی"},
                    new PurchaseOrderTransferRemittanceStatus{Id=2, StatusDesc="ثبت ورود شده"},
                    new PurchaseOrderTransferRemittanceStatus{Id=3, StatusDesc="تخلیه شده"},
                    new PurchaseOrderTransferRemittanceStatus{Id=10, StatusDesc="ابطال شده"},
                };

            foreach (var item in trstatus)
            {
                var en = applicationDbContext.TransferRemittanceStatus.Where(b => b.Id.Equals(item.Id)).AsQueryable();
                if (!en.Any())
                {
                    applicationDbContext.TransferRemittanceStatus.Add(item);
                }
                //if (item.Id == 3 && en!=null && en.First().StatusDesc != "تخلیه شده")
                //{
                //    en.First().StatusDesc = "تخلیه شده";
                //    applicationDbContext.TransferRemittanceStatus.Update(en.First());
                //}

                await applicationDbContext.SaveChangesAsync();
            }
        }
    }
}
