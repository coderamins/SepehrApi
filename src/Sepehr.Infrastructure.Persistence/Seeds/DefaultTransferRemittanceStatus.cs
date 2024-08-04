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
                new List<TransferRemittanceStatus> {
                    new TransferRemittanceStatus{Id=1, StatusDesc="در حال بررسی"},
                    new TransferRemittanceStatus{Id=2, StatusDesc="ثبت ورود شده"},
                    new TransferRemittanceStatus{Id=3, StatusDesc="تخلیه شده"},
                    new TransferRemittanceStatus{Id=10, StatusDesc="ابطال شده"},
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
