using Sepehr.Domain.Entities;

namespace Sepehr.Application.Interfaces.Repositories
{
    public interface IBrandRepositoryAsync : IGenericRepositoryAsync<Brand>
    {
        Task<Brand?> GetBrandInfo( string Name);
    }
}