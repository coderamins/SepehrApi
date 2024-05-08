using Microsoft.EntityFrameworkCore;
using Sepehr.Application.Interfaces.Repositories;
using Sepehr.Domain.Entities.UserEntities;
using Sepehr.Infrastructure.Persistence.Context;

namespace Sepehr.Infrastructure.Persistence.Repositories
{
    public class ApplicationRoleRepositoryAsync : GenericRepositoryAsync<ApplicationRole>, IApplicationRoleRepositoryAsync
    {
        private readonly DbSet<ApplicationRole> _applicationRoles;
        private readonly ApplicationDbContext _dbContext;

        public ApplicationRoleRepositoryAsync(ApplicationDbContext dbContext) : base(dbContext)
        {
            _applicationRoles = dbContext.Set<ApplicationRole>();
            _dbContext= dbContext;
        }

        public async Task<List<ApplicationRole>> GetAllApplicationRoles()
        {
            return await _applicationRoles.ToListAsync();
        }

        public async Task<ApplicationRole?> GetApplicationRoleInfo(string Id)
        {
            return await _applicationRoles
                .Include(x=>x.RolePermissions).ThenInclude(x=>x.Permission)
                .FirstOrDefaultAsync(p => p.Id.ToString() == Id);
        }

        public async Task<ApplicationRole> UpdateApplicationRoleAsync(ApplicationRole applicationRole)
        {
            _dbContext.RolePermissions.RemoveRange(_dbContext.RolePermissions
                .Where(s => s.RoleId == applicationRole.Id && 
                            !applicationRole.RolePermissions.Select(d => d.Id).Contains(s.Id)));

            var result=_applicationRoles.Update(applicationRole);
            await _dbContext.SaveChangesAsync();

            return result.Entity;
        }

    }
}