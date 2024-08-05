using Microsoft.EntityFrameworkCore;
using Sepehr.Domain.Entities;
using Sepehr.Infrastructure.Persistence.Context;

namespace Sepehr.Infrastructure.Persistence.Seeds
{
    public static class DefaultCustomerLabelTypes
    {
        public static async Task
        SeedAsync(ApplicationDbContext applicationDbContext)
        {
            //Seed Default User
            var pTypes =
                new List<CustomerLabelType> {
                    new CustomerLabelType{Id=1,LabelTypeDesc="نوع کالا"},
                    new CustomerLabelType{Id=2,LabelTypeDesc="نام کالا"},
                    new CustomerLabelType{Id=3,LabelTypeDesc="نام برند"},
                    new CustomerLabelType{Id=4,LabelTypeDesc="کالا برند"},
                    new CustomerLabelType{Id=5,LabelTypeDesc="سایر"},
                };

            foreach (var item in pTypes)
            {
                if (!applicationDbContext.CustomerLabelTypes.Where(b => b.Id.Equals(item.Id)).Any())
                {
                    applicationDbContext.CustomerLabelTypes.Add(item);
                }

                await applicationDbContext.SaveChangesAsync();
            }
        }
    }
}
