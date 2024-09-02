using Sepehr.Application.Interfaces;
using Sepehr.Infrastructure.Persistence.Context;

namespace Sepehr.Infrastructure.Persistence
{
    public class PermissionInitializerService: IPermissionInitializerService
    {
        private readonly ApplicationDbContext _context;
        public PermissionInitializerService(ApplicationDbContext context)
        {
            _context = context;
        }
    }
}
