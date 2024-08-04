using Sepehr.Domain.Entities;

namespace Sepehr.Application.Interfaces.Repositories
{
    public interface IPuOrderTransRemitUnloadPermitRepositoryAsync : IGenericRepositoryAsync<UnloadingPermit>
    {
        Task<UnloadingPermit?> GetRemittanceUnloadingPermitInfo(Guid Id);
    }
}