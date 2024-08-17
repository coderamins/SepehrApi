using Microsoft.EntityFrameworkCore;
using Sepehr.Domain.Entities;
using Sepehr.Infrastructure.Persistence.Context;

namespace Sepehr.Infrastructure.Persistence.Seeds
{
    public static class DefaultReceivePaymentTypes
    {
        public static async Task
        SeedAsync(ApplicationDbContext applicationDbContext)
        {
            //Seed Default User
            var receivePaymentOrigins =
                new List<PaymentOriginType> {
                    new PaymentOriginType{Id=1,Desc="مشتری"},
                    new PaymentOriginType{Id=2,Desc="بانک"},
                    new PaymentOriginType{Id=3,Desc="صندوق"},
                    new PaymentOriginType{Id=4,Desc="درآمد"},
                    new PaymentOriginType{Id=5,Desc="تنخواه گردان"},
                    new PaymentOriginType{Id=6,Desc="هزینه"},
                    new PaymentOriginType{Id=7,Desc="برداشت نقدی سهامداران"},
                    new PaymentOriginType{Id=8,Desc="پرداخت خمس سهامداران"},
                };

            foreach (var item in receivePaymentOrigins)
            {
                if (!applicationDbContext.PaymentOriginTypes.Where(b => b.Id.Equals(item.Id)).Any())
                {
                    applicationDbContext.PaymentOriginTypes.Add(item);
                }

                await applicationDbContext.SaveChangesAsync();
            }
        }
    }
}
