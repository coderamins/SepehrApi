using Microsoft.EntityFrameworkCore;
using Sepehr.Domain.Entities;
using Sepehr.Domain.Enums;
using Sepehr.Infrastructure.Persistence.Context;

namespace Sepehr.Infrastructure.Persistence.Seeds
{
    public static class DefaultPhoneNumberTypes
    {
        public static async Task
        SeedAsync(ApplicationDbContext applicationDbContext)
        {
            //Seed Default User
            var pTypes =
                new List<PhoneNumberType> {
                    new PhoneNumberType {Id=(int)EPhoneNoType.Mobile,TypeDescription="تلفن همراه",IsActive=true},
                    new PhoneNumberType {Id=(int)EPhoneNoType.Home,TypeDescription="تلفن منزل",IsActive=true},
                    new PhoneNumberType {Id=(int)EPhoneNoType.Office,TypeDescription="تلفن محل کار",IsActive=true},
                };

            foreach (var item in pTypes)
            {
                if (!applicationDbContext.PhoneNumberTypes.Where(b => b.Id.Equals(item.Id)).Any())
                {
                    applicationDbContext.PhoneNumberTypes.Add(item);
                }

                await applicationDbContext.SaveChangesAsync();
            }
        }
    }
}
