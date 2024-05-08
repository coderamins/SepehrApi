using Sepehr.Application.Helpers;
using Sepehr.Domain.Entities.UserEntities;
using Sepehr.Infrastructure.Persistence.Context;

namespace Sepehr.Infrastructure.Persistence.Seeds
{
    public static class DefaultBasicUser
    {
        public static async Task
        SeedAsync(
            ApplicationDbContext dbContext)
        {
            //Seed Default User
            var defaultUser =
                new ApplicationUser
                {
                    CreatedBy =Guid.Parse("6A1CE884-F17A-4870-B919-BC0D41A27755"),
                    UserName = "sepehruser",
                    Email = "sepehrofficial@info.com",
                    FirstName = "sepehr",
                    LastName = "sepehr",
                    EmailConfirmed = true,
                    PasswordHash = new PasswordHelper().Encrypt("Sep@u123456")
                    //PhoneNumberConfirmed = true
                };


            if (!dbContext.Users.Any(u => u.UserName == defaultUser.UserName))
            {
                var newUser = await dbContext.Users.AddAsync(defaultUser);
                await dbContext.UserRoles.AddAsync(new UserRole
                {
                    RoleId = Guid.Parse("274d65d8-bdaf-4e50-a859-5e44b54b302d"),
                    UserId = newUser.Entity.Id
                });

                dbContext.SaveChanges();
            }
        }
    }
}
