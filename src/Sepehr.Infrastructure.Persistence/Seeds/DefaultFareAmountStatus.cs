using Sepehr.Domain.Entities;
using Sepehr.Infrastructure.Persistence.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sepehr.Infrastructure.Persistence.Seeds
{
    public static class DefaultFareAmountStatus
    {
        public static async Task
        SeedAsync(ApplicationDbContext applicationDbContext)
        {
            //Seed Default User
            var statuses =
                new List<FareAmountStatus> {
                    new FareAmountStatus{Id=1,StatusDesc="در حال بررسی"},
                    new FareAmountStatus{Id=2,StatusDesc="تایید شده"},
                    new FareAmountStatus{Id=3,StatusDesc="پرداخت شده"},
                };

            foreach (var item in statuses)
            {
                if (!applicationDbContext.ReceivePayStatus.Where(b => b.Id.Equals(item.Id)).Any())
                {
                    applicationDbContext.ReceivePayStatus.Add(item);
                }

                await applicationDbContext.SaveChangesAsync();
            }
        }
    }
}
