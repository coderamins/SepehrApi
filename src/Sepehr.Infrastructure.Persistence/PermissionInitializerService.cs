using Microsoft.EntityFrameworkCore;
using Sepehr.Application.Interfaces;
using Sepehr.Domain.Entities.UserEntities;
using Sepehr.Infrastructure.Persistence.Context;
using Sepehr.Infrastructure.Persistence.Repositories;

namespace Sepehr.Infrastructure.Persistence
{
    public class PermissionInitializerService : GenericRepositoryAsync<Permission>, IPermissionInitializerService
    {
        private readonly ApplicationDbContext _context;
        public PermissionInitializerService(ApplicationDbContext dbContext, ApplicationDbContext context) : base(dbContext)
        {
            _context = context;
        }

        public async Task<bool> CheckPermissionExists(string policy)
        {
            return await _context.Permissions.AnyAsync(x=>x.Name.Trim().ToLower() == policy.Trim().ToLower());
        }
    }
}
