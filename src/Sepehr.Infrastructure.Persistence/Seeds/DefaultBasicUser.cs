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
                    CreatedBy =Guid.Parse("465C2D61-95DB-4822-9E95-8571247296A6"),
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
                var adminRole= await dbContext.Roles.AddAsync(new ApplicationRole
                {                    
                    Name="Admin",
                    Description ="مدیر سیستم",
                    IsActive = true,
                });
                await dbContext.UserRoles.AddAsync(new UserRole
                {
                    RoleId = adminRole.Entity.Id,
                    UserId = newUser.Entity.Id
                });

                dbContext.SaveChanges();
            }
        }
    }
}
