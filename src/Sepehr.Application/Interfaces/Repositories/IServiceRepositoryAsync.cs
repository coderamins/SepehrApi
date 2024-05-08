using Sepehr.Domain.Entities;

namespace Sepehr.Application.Interfaces.Repositories
{
    public interface IServiceRepositoryAsync : IGenericRepositoryAsync<Service>
    {
        Task<Service?> GetServiceInfo(string ServiceName);
    }
}