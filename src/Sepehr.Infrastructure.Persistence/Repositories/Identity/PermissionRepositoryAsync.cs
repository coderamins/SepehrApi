using Microsoft.EntityFrameworkCore;
using Sepehr.Application.Interfaces.Repositories;
using Sepehr.Domain.Entities.UserEntities;
using Sepehr.Infrastructure.Persistence.Context;

namespace Sepehr.Infrastructure.Persistence.Repositories
{
    public class PermissionRepositoryAsync : GenericRepositoryAsync<Permission>, IPermissionRepositoryAsync
    {
        private readonly DbSet<Permission> _permissions;
        private readonly DbSet<ApplicationMenu> _applicationMenus;
        private readonly ApplicationDbContext _dbContext;

        public PermissionRepositoryAsync(ApplicationDbContext dbContext) : base(dbContext)
        {
            _permissions = dbContext.Set<Permission>();
            _applicationMenus = dbContext.Set<ApplicationMenu>();
            _dbContext = dbContext;
        }

        public async Task<List<Permission>> GetAllPermissions()
        {
            return await _permissions
                .Include(p => p.ApplicationMenu)
                .ToListAsync();
        }

        public async Task<List<ApplicationMenu>> GetAllPermissionsByMenu()
        {
            return await _applicationMenus
                .Include(p => p.Permissions)
                .Where(p => p.Permissions.Count() > 0)
                .ToListAsync();
        }

        public async Task<Permission?> GetPermissionInfo(Guid Id)
        {
            return await _permissions
                .Include(p => p.ApplicationMenu)
                .FirstOrDefaultAsync(p => p.Id == Id);
        }

        public async Task<Permission?> GetPermissionInfo(string title)
        {
            return await _permissions
                .Include(p => p.ApplicationMenu)
                .FirstOrDefaultAsync(p => p.Name == title);
        }

        public async Task<HashSet<string>> GetPermissionsAsync(Guid parsedUserId)
        {
            ICollection<UserRole>[] roles =
                await _dbContext.Set<ApplicationUser>()
                .Include(x => x.Roles).ThenInclude(x => x.Role).ThenInclude(x => x.RolePermissions).ThenInclude(x => x.Permission)
                .Where(u => u.Id == parsedUserId)
                .Select(x => x.Roles).ToArrayAsync();

            var r = roles
                .SelectMany(x => x)
                .SelectMany(x => x.Role.RolePermissions)
                .Select(x => x.Permission.Name);

            return r.ToHashSet();
        }
    }
}