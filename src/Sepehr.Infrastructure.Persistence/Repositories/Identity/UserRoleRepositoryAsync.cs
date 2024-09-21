using DocumentFormat.OpenXml.Spreadsheet;
using Microsoft.EntityFrameworkCore;
using Sepehr.Application.Interfaces.Repositories;
using Sepehr.Domain.Entities.UserEntities;
using Sepehr.Infrastructure.Persistence.Context;

namespace Sepehr.Infrastructure.Persistence.Repositories
{
    public class UserRoleRepositoryAsync : GenericRepositoryAsync<UserRole>, IUserRoleRepositoryAsync
    {
        private readonly DbSet<UserRole> _userRoles;

        public UserRoleRepositoryAsync(ApplicationDbContext dbContext) : base(dbContext)
        {
            _userRoles = dbContext.Set<UserRole>();
        }

        public async Task<List<UserRole>> GetAllUserRoles(Guid userId)
        {
            return await _userRoles.Where(u=>u.UserId==userId).OrderByDescending(p => p.Created.ToString()).ToListAsync();
        }

        public HashSet<string> GetAllUserRoles(string userId)
        {
            return _userRoles.Where(u => u.UserId.ToString() == userId).Include(r=>r.Role)
                .Select(p => p.Role.Name).ToHashSet();

        }

        public async Task<UserRole?> GetUserRoleInfo(Guid userId,Guid roleId)
        {
            return await _userRoles
                .FirstOrDefaultAsync(p => p.UserId == userId && p.RoleId==roleId);
        }

    }
}