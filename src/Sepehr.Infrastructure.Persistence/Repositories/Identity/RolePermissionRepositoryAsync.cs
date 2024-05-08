using Microsoft.EntityFrameworkCore;
using Sepehr.Application.Interfaces.Repositories;
using Sepehr.Domain.Entities.UserEntities;
using Sepehr.Infrastructure.Persistence.Context;

namespace Sepehr.Infrastructure.Persistence.Repositories
{
    public class RolePermissionRepositoryAsync : GenericRepositoryAsync<RolePermission>, IRolePermissionRepositoryAsync
    {
        private readonly DbSet<RolePermission> _rolePermissions;

        public RolePermissionRepositoryAsync(ApplicationDbContext dbContext) : base(dbContext)
        {
            _rolePermissions = dbContext.Set<RolePermission>();
        }

        public async Task<List<RolePermission>> GetAllRolePermissions()
        {
            return await _rolePermissions.ToListAsync();
        }

        public async Task<RolePermission?> GetRolePermissionInfo(Guid Id)
        {
            return await _rolePermissions
                .FirstOrDefaultAsync(p => p.Id == Id);
        }

        public async Task<RolePermission?> GetRolePermissionInfo(Guid roleId,Guid permissionId)
        {
            return await _rolePermissions
                .FirstOrDefaultAsync(p => p.RoleId == roleId && p.PermissionId==permissionId);
        }

    }
}