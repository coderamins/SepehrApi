using Microsoft.EntityFrameworkCore;
using Sepehr.Domain.Entities;
using Sepehr.Domain.Enums;
using Sepehr.Infrastructure.Persistence.Context;

namespace Sepehr.Infrastructure.Persistence.Seeds
{
    public static class DefaultTransferRemittanceTypes
    {
        public static async Task
        SeedAsync(ApplicationDbContext applicationDbContext)
        {
            //Seed Default User
            var brands =
                new List<TransferRemittanceType> {
                    new TransferRemittanceType{Id=1,RemittanceTypeDesc="طبق برنامه"},
                    new TransferRemittanceType{Id=2,RemittanceTypeDesc="برای رزور"},
                    new TransferRemittanceType{Id=3,RemittanceTypeDesc="برای انبار"},
                };

            foreach (var item in brands)
            {
                if (!applicationDbContext.TransferRemittanceTypes
                    .Where(b=>b.Id.Equals(item.Id)).Any())
                {
                    applicationDbContext.TransferRemittanceTypes.Add(item);
                }
                                
                await applicationDbContext.SaveChangesAsync();
            }
        }
    }
}
