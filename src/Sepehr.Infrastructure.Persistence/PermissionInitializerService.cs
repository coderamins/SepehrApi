using Sepehr.Application.Interfaces;
using Sepehr.Domain.Entities.UserEntities;
using Sepehr.Infrastructure.Persistence.Context;
using Sepehr.Infrastructure.Persistence.Repositories;

namespace Sepehr.Infrastructure.Persistence
{
    public class PermissionInitializerService : GenericRepositoryAsync<Permission>, IPermissionInitializerService
    {
        public PermissionInitializerService(ApplicationDbContext dbContext) : base(dbContext)
        {
        }
    }
}
