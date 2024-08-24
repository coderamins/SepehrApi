using AutoMapper;
using Sepehr.Application.Features.PurchaseOrders.Command.TransferPurchaseOrder;
using Sepehr.Application.Features.TransferRemittances.Command.TransferRemittanceEntrancePermission;
using Sepehr.Application.Features.TransferRemittances.Queries.GetAllTransferRemittances;
using Sepehr.Domain.Entities;

namespace Sepehr.Application.Interfaces.Repositories
{
    public interface ITransferRemittanceRepositoryAsync : IGenericRepositoryAsync<TransferRemittance>
    {
        Task<IEnumerable<TransferRemittance>> GetAllTransferRemittancesAsync(GetAllTransferRemittancesParameter validFilter);
        Task<TransferRemittance> UpdateTransferRemittance(TransferRemittance transRemittance);
        Task<TransferRemittance?> GetTransferRemittanceByIdAsync(int id);
        Task<EntrancePermit> TransferRemittanceEntrancePermission(
            TransferRemittanceEntrancePermissionCommand entrancePermit);
        Task<TransferRemittance> CreateTransferRemittance(TransferRemittance transRemittance);
        Task<TransferRemittance?> GetTransferRemittanceByPermitCodeAsync(int PermitCode);
        Task<EntrancePermit> TransRemittEntrancePermitById(Guid entrancePermitId);
        Task<UnloadingPermit> CreatePOrderUnloadingPermit( UnloadingPermit unloadingPermit);
    }
}