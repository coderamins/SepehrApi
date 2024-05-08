using Sepehr.Domain.Entities;
using Sepehr.Domain.Entities.UserEntities;

namespace Sepehr.Application.Interfaces.Repositories
{
    public interface IApplicationRoleRepositoryAsync : IGenericRepositoryAsync<ApplicationRole>
    {
        Task<List<ApplicationRole>> GetAllApplicationRoles();
        Task<ApplicationRole?> GetApplicationRoleInfo(string Id);
        Task<ApplicationRole> UpdateApplicationRoleAsync(ApplicationRole applicationRole);
    }
}