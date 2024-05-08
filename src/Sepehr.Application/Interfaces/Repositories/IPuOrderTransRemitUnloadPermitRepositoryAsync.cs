using Sepehr.Domain.Entities;

namespace Sepehr.Application.Interfaces.Repositories
{
    public interface IPuOrderTransRemitUnloadPermitRepositoryAsync : IGenericRepositoryAsync<PurchaseOrderTransferRemittanceUnloadingPermit>
    {
        Task<PurchaseOrderTransferRemittanceUnloadingPermit?> GetRemittanceUnloadingPermitInfo(Guid Id);
    }
}