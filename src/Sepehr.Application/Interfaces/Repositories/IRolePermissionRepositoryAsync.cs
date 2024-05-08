using Sepehr.Domain.Entities;
using Sepehr.Domain.Entities.UserEntities;

namespace Sepehr.Application.Interfaces.Repositories
{
    public interface IRolePermissionRepositoryAsync : IGenericRepositoryAsync<RolePermission>
    {
        Task<List<RolePermission>> GetAllRolePermissions();
        Task<RolePermission?> GetRolePermissionInfo(Guid roleId, Guid permissionId);
        Task<RolePermission?> GetRolePermissionInfo(Guid id);
    }
}