using Microsoft.EntityFrameworkCore;
using Sepehr.Domain.Entities;
using Sepehr.Infrastructure.Persistence.Context;

namespace Sepehr.Infrastructure.Persistence.Seeds
{
    public static class DefaultBanks
    {
        public static async Task
        SeedAsync(ApplicationDbContext applicationDbContext)
        {
            //Seed Default User
            var Banks =
                new List<Bank> {
                    new Bank{Id=1,BankName="بانک ملی"},
                    new Bank{Id=2,BankName="بانک سپه"},
                    new Bank{Id=3,BankName="بانک صادرات"},
                    new Bank{Id=4,BankName="بانک تجارت"},
                    new Bank{Id=5,BankName="بانک ملت"},
                    new Bank{Id=6,BankName="بانک شهر"},
                    new Bank{Id=7,BankName="بانک سینا"},
                    new Bank{Id=8,BankName="بانک پاسارگاد"},
                    new Bank{Id=9,BankName="بانک پارسیان"},
                    new Bank{Id=10,BankName="بانک رفاه"},
                    new Bank{Id=11,BankName="بانک توسعه تعاون"},
                    new Bank{Id=12,BankName="بانک مهر ایران"},
                };

            foreach (var item in Banks)
            {
                if (!applicationDbContext.Banks.Where(b=>b.BankName.Equals(item.BankName)).Any())
                {
                    applicationDbContext.Banks.Add(item);
                }
                                
                await applicationDbContext.SaveChangesAsync();
            }
        }
    }
}
