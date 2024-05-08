using Microsoft.EntityFrameworkCore;
using Sepehr.Domain.Entities;
using Sepehr.Infrastructure.Persistence.Context;

namespace Sepehr.Infrastructure.Persistence.Seeds
{
    public static class DefaultReceivePaymentOrigins
    {
        public static async Task
        SeedAsync(ApplicationDbContext applicationDbContext)
        {
            //Seed Default User
            var receivePaymentOrigins =
                new List<ReceivePaymentOrigin> {
                    new ReceivePaymentOrigin{Id=1,Desc="مشتری"},
                    new ReceivePaymentOrigin{Id=2,Desc="بانک"},
                    new ReceivePaymentOrigin{Id=3,Desc="صندوق"},
                    new ReceivePaymentOrigin{Id=4,Desc="درآمد"},
                    new ReceivePaymentOrigin{Id=5,Desc="تنخواه گردان"},
                    new ReceivePaymentOrigin{Id=6,Desc="هزینه"},
                    new ReceivePaymentOrigin{Id=7,Desc="برداشت نقدی سهامداران"},
                    new ReceivePaymentOrigin{Id=8,Desc="پرداخت خمس سهامداران"},
                };

            foreach (var item in receivePaymentOrigins)
            {
                //if (!applicationDbContext.ReceivePaymentOrigins.Where(b => b.Id.Equals(item.Id)).Any())
                //{
                //    applicationDbContext.ReceivePaymentOrigins.Add(item);
                //}

                //await applicationDbContext.SaveChangesAsync();
            }
        }
    }
}
