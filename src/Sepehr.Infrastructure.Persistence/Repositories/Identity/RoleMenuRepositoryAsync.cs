using Microsoft.EntityFrameworkCore;
using Sepehr.Application.Interfaces;
using Sepehr.Application.Interfaces.Repositories;
using Sepehr.Domain.Entities.UserEntities;
using Sepehr.Infrastructure.Persistence.Context;

namespace Sepehr.Infrastructure.Persistence.Repositories
{
    public class RoleMenuRepositoryAsync : GenericRepositoryAsync<RoleMenu>, IRoleMenuRepositoryAsync
    {

        private readonly DbSet<RoleMenu> _roleMenu;

        public RoleMenuRepositoryAsync(ApplicationDbContext dbContext) : base(dbContext)
        {
            _roleMenu = dbContext.Set<RoleMenu>();
        }

        public async Task<List<RoleMenu>> GetAllRoleMenus()
        {
            return await _roleMenu.ToListAsync();
        }


    }
}