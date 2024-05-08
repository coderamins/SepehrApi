using Sepehr.Domain.Entities;
using Sepehr.Domain.Entities.UserEntities;

namespace Sepehr.Application.Interfaces.Repositories
{
    public interface IUserRoleRepositoryAsync : IGenericRepositoryAsync<UserRole>
    {
        Task<List<UserRole>> GetAllUserRoles(Guid userId);
        HashSet<string> GetAllUserRoles(string userId);
        Task<UserRole?> GetUserRoleInfo(Guid userId, Guid roleId);
    }
}