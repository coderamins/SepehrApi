using Sepehr.Domain.Entities;
using Sepehr.Infrastructure.Persistence.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sepehr.Infrastructure.Persistence.Seeds
{
    public static class DefaultPaymentRequestStatus
    {
        public static async Task
        SeedAsync(ApplicationDbContext applicationDbContext)
        {
            //Seed Default User
            var paymentRequestStatus =
                new List<PaymentRequestStatus> {
                    new PaymentRequestStatus{Id=1, StatusDesc="در حال بررسی"},
                    new PaymentRequestStatus{Id=2, StatusDesc="تایید شده"},
                    new PaymentRequestStatus{Id=3, StatusDesc="پرداخت شده"},
                    new PaymentRequestStatus{Id=4, StatusDesc="رد شده"},
                };

            foreach (var item in paymentRequestStatus)
            {
                if (!applicationDbContext.PaymentRequestStatus.Where(b => b.Id.Equals(item.Id)).Any())
                {
                    applicationDbContext.PaymentRequestStatus.Add(item);
                }

                await applicationDbContext.SaveChangesAsync();
            }
        }
    }

}
