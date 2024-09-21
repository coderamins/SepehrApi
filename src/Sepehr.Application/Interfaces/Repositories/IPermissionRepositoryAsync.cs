using Sepehr.Domain.Entities;
using Sepehr.Domain.Entities.UserEntities;

namespace Sepehr.Application.Interfaces.Repositories
{
    public interface IPermissionRepositoryAsync : IGenericRepositoryAsync<Permission>
    {
        Task<List<Permission>> GetAllPermissions();
        Task<List<ApplicationMenu>> GetAllPermissionsByMenu();
        Task<Permission?> GetPermissionInfo(Guid Id);
        Task<Permission?> GetPermissionInfo(string Title);
        Task<HashSet<string>> GetPermissionsAsync(Guid parsedUserId);
        Task<HashSet<string>> GetRequirementMappedPermissions(IEnumerable<string> permission);
    }
}