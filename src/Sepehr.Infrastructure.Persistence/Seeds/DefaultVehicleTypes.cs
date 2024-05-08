using Microsoft.EntityFrameworkCore;
using Sepehr.Domain.Entities;
using Sepehr.Infrastructure.Persistence.Context;

namespace Sepehr.Infrastructure.Persistence.Seeds
{
    public static class DefaultVehicleTypes
    {
        public static async Task
        SeedAsync(ApplicationDbContext applicationDbContext)
        {
            //Seed Default User
            var VehicleTypes =
                new List<VehicleType> {
                    new VehicleType{Name="کفی"},
                    new VehicleType{Name="نیسان"},
                    new VehicleType{Name="کامیون"},
                };

            foreach (var item in VehicleTypes)
            {
                if (!applicationDbContext.VehicleTypes.Where(b=>b.Name.Equals(item.Name)).Any())
                {
                    applicationDbContext.VehicleTypes.Add(item);
                }
                                
                await applicationDbContext.SaveChangesAsync();
            }
        }
    }
}
