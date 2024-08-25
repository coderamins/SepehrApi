using Sepehr.Application.Features.UnloadingPermits.Queries.GetAllUnloadingPermits;
using Sepehr.Domain.Entities;
using Sepehr.Domain.Enums;

namespace Sepehr.Application.Interfaces.Repositories
{
    public interface IUnloadingPermitRepositoryAsync : IGenericRepositoryAsync<UnloadingPermit>
    {
        EFarePaymentType CheckFarePaymentType(Guid id);
        Task<IEnumerable<UnloadingPermit>> GetAllUnloadingPermits(GetAllUnloadingPermitsParameter validFilter);
        Task<UnloadingPermit?> GetUnloadingPermitInfo(Guid Id);
        Task<UnloadingPermit?> GetUnloadingPermitInfo(int unloadingPermitCode);
    }
}