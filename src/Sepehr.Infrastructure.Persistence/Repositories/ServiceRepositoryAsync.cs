using Microsoft.EntityFrameworkCore;
using Sepehr.Application.Interfaces.Repositories;
using Sepehr.Domain.Entities;
using Sepehr.Infrastructure.Persistence.Context;

namespace Sepehr.Infrastructure.Persistence.Repositories
{
    public class ServiceRepositoryAsync : GenericRepositoryAsync<Service>, IServiceRepositoryAsync
    {
        private readonly DbSet<Service> _Services;

        public ServiceRepositoryAsync(ApplicationDbContext dbContext) : base(dbContext)
        {
            _Services = dbContext.Set<Service>();
        }

        public async Task<List<Service>> GetAllServices()
        {
            return await _Services
                .OrderByDescending(p => p.Id).ToListAsync();
        }

        public async Task<Service?> GetServiceInfo(string serviceName)
        {
            return await _Services
                .FirstOrDefaultAsync(p => p.Description== serviceName);
        }
    }
}