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
                new List<ReceivePaymentType> {
                    new ReceivePaymentType{Id=1,Desc="مشتری"},
                    new ReceivePaymentType{Id=2,Desc="بانک"},
                    new ReceivePaymentType{Id=3,Desc="صندوق"},
                    new ReceivePaymentType{Id=4,Desc="درآمد"},
                    new ReceivePaymentType{Id=5,Desc="تنخواه گردان"},
                    new ReceivePaymentType{Id=6,Desc="هزینه"},
                    new ReceivePaymentType{Id=7,Desc="برداشت نقدی سهامداران"},
                    new ReceivePaymentType{Id=8,Desc="پرداخت خمس سهامداران"},
                };

            foreach (var item in receivePaymentOrigins)
            {
                if (!applicationDbContext.ReceivePaymentTypes.Where(b => b.Id.Equals(item.Id)).Any())
                {
                    applicationDbContext.ReceivePaymentTypes.Add(item);
                }

                await applicationDbContext.SaveChangesAsync();
            }
        }
    }
}
