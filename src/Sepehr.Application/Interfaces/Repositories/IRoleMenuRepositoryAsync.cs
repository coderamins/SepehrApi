using Sepehr.Domain.Entities.UserEntities;

namespace Sepehr.Application.Interfaces.Repositories
{
    public interface IRoleMenuRepositoryAsync : IGenericRepositoryAsync<RoleMenu>
    {
        Task<List<RoleMenu>> GetAllRoleMenus();
    }
}