using AutoMapper;
using Sepehr.Application.Features.PurchaseOrders.Command.TransferPurchaseOrder;
using Sepehr.Application.Features.TransferRemittances.Command.TransferRemittanceEntrancePermission;
using Sepehr.Application.Features.TransferRemittances.Queries.GetAllTransferRemittances;
using Sepehr.Domain.Entities;

namespace Sepehr.Application.Interfaces.Repositories
{
    public interface IPurchaseOrderTransferRemittanceRepositoryAsync : IGenericRepositoryAsync<PurchaseOrderTransferRemittance>
    {
        Task<IEnumerable<PurchaseOrderTransferRemittance>> GetAllTransferRemittancesAsync(GetAllTransferRemittancesParameter validFilter);
        Task<PurchaseOrderTransferRemittance> UpdateTransferRemittance(PurchaseOrderTransferRemittance transRemittance);
        Task<PurchaseOrderTransferRemittance?> GetTransferRemittanceByIdAsync(int id);
        Task<PurchaseOrderTransferRemittanceEntrancePermit> TransferRemittanceEntrancePermission(
            TransferRemittanceEntrancePermissionCommand entrancePermit);
        Task<PurchaseOrderTransferRemittance> CreateTransferRemittance(PurchaseOrderTransferRemittance transRemittance);
        Task<PurchaseOrderTransferRemittance?> GetTransferRemittanceByPermitCodeAsync(int PermitCode);
        Task<PurchaseOrderTransferRemittanceEntrancePermit> PurchaseOrderTransRemittEntrancePermitById(Guid purchaseOrderTransferRemittanceEntrancePermitId);
        Task<PurchaseOrderTransferRemittanceUnloadingPermit> CreatePOrderUnloadingPermit(
            PurchaseOrderTransferRemittanceUnloadingPermit purchaseOrderTransferRemittanceUnloadingPermit);
    }
}