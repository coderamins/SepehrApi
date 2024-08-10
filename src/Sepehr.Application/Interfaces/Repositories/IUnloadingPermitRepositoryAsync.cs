using Sepehr.Application.Features.UnloadingPermits.Queries.GetAllUnloadingPermits;
using Sepehr.Domain.Entities;

namespace Sepehr.Application.Interfaces.Repositories
{
    public interface IUnloadingPermitRepositoryAsync : IGenericRepositoryAsync<UnloadingPermit>
    {
        Task<IEnumerable<UnloadingPermit>> GetAllUnloadingPermits(GetAllUnloadingPermitsParameter validFilter);
        Task<UnloadingPermit?> GetUnloadingPermitInfo(Guid Id);
        Task<UnloadingPermit?> GetUnloadingPermitInfo(int unloadingPermitCode);
    }
}