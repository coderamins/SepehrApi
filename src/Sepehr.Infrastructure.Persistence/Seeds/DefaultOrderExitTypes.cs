using Sepehr.Application.Helpers;
using Sepehr.Domain.Entities;
using Sepehr.Domain.Entities.UserEntities;
using Sepehr.Infrastructure.Persistence.Context;

namespace Sepehr.Infrastructure.Persistence.Seeds
{
    public static class DefaultOrderExitTypes
    {
        public static async Task
        SeedAsync(
            ApplicationDbContext dbContext)
        {
            var defaultExitTypes = new OrderExitType[] {
                new OrderExitType { Id=1, ExitTypeDesc="عادی",},
                new OrderExitType { Id=2, ExitTypeDesc="اعلام بار بعد از تسویه",},
                new OrderExitType { Id=3, ExitTypeDesc="خروج بعد از تسویه",},
            };


            foreach (var item in defaultExitTypes)
            {
                if (!dbContext.OrderExitTypes.Where(b => b.Id.Equals(item.Id)).Any())
                {
                    dbContext.OrderExitTypes.Add(item);
                }

                await dbContext.SaveChangesAsync();
            }
        }
    }
}
