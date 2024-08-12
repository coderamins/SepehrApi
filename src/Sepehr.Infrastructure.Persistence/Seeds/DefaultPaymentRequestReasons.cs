using Sepehr.Domain.Entities;
using Sepehr.Infrastructure.Persistence.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sepehr.Infrastructure.Persistence.Seeds
{
    public static class DefaultPaymentRequestReasons
    {
        public static async Task
        SeedAsync(ApplicationDbContext applicationDbContext)
        {
            //Seed Default User
            var paymentRequestReasons =
                new List<PaymentRequestReason> {
                    new PaymentRequestReason{Id=1, ReasonDesc="حقوق"},
                };

            foreach (var item in paymentRequestReasons)
            {
                if (!applicationDbContext.PaymentRequestReasons.Where(b => b.Id.Equals(item.Id)).Any())
                {
                    applicationDbContext.PaymentRequestReasons.Add(item);
                }

                await applicationDbContext.SaveChangesAsync();
            }
        }
    }

}
