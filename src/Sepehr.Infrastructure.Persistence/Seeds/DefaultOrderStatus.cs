﻿using Microsoft.EntityFrameworkCore;
using Sepehr.Domain.Entities;
using Sepehr.Infrastructure.Persistence.Context;

namespace Sepehr.Infrastructure.Persistence.Seeds
{
    public static class DefaultOrderStatus
    {
        public static async Task
        SeedAsync(ApplicationDbContext applicationDbContext)
        {
            //Seed Default User
            var orderstatus =
                new List<OrderStatus> {
                    new OrderStatus{Id=1,StatusDesc="جدید"},
                    new OrderStatus{Id=2,StatusDesc="تایید شده"},
                    new OrderStatus{Id=3,StatusDesc="تایید شده حسابداری"},
                    new OrderStatus{Id=4,StatusDesc="عدم تایید حسابداری"},
                    new OrderStatus{Id=5,StatusDesc="در حال ارسال"},
                    new OrderStatus{Id=6,StatusDesc="ارسال شده"},
                    new OrderStatus{Id=7,StatusDesc="برگشت خورده"},
                    new OrderStatus{Id=8,StatusDesc="ابطال شده"},
                    new OrderStatus{Id=9,StatusDesc="مقداری از بار برگشت خورده"},
                };

            foreach (var item in orderstatus)
            {
                var ostat =await applicationDbContext.OrderStatuses.FirstOrDefaultAsync(b => b.Id.Equals(item.Id));
                if(ostat==null)
                {
                    applicationDbContext.OrderStatuses.Add(item);
                }
                else if(!ostat.StatusDesc.Equals(item.StatusDesc))
                {
                    ostat.StatusDesc = item.StatusDesc;
                    applicationDbContext.Update(item);
                }
                                
                await applicationDbContext.SaveChangesAsync();
            }
        }
    }
}
