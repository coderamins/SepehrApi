using AutoMapper;
using Sepehr.Application.Features.PurchaseOrders.Command.TransferPurchaseOrder;
using Sepehr.Application.Features.TransferWarehouseInventories.Command.TransferWarehouseInventoryEntrancePermission;
using Sepehr.Application.Features.TransferWarehouseInventories.Queries.GetAllTransferWarehouseInventories;
using Sepehr.Domain.Entities;

namespace Sepehr.Application.Interfaces.Repositories
{
    public interface ITransferWarehouseInventoryRepositoryAsync : IGenericRepositoryAsync<TransferWarehouseInventory>
    {
        Task<IEnumerable<TransferWarehouseInventory>> GetAllTransferWarehouseInventoriesAsync(GetAllTransferWarehouseInventoriesParameter validFilter);
        Task<TransferWarehouseInventory> UpdateTransferWarehouseInventory(TransferWarehouseInventory transRemittance);
        Task<TransferWarehouseInventory?> GetTransferWarehouseInventoryByIdAsync(int id);
        Task<EntrancePermit> TransferWarehouseInventoryEntrancePermission(
            TransferWarehouseInventoryEntrancePermissionCommand entrancePermit);
        Task<TransferWarehouseInventory> CreateTransferWarehouseInventory(TransferWarehouseInventory transRemittance);
        Task<TransferWarehouseInventory?> GetTransferWarehouseInventoryByPermitCodeAsync(int PermitCode);
        Task<EntrancePermit> PurchaseOrderTransRemittEntrancePermitById(Guid purchaseOrderTransferWarehouseInventoryEntrancePermitId);
        Task<UnloadingPermit> CreatePOrderUnloadingPermit(
            UnloadingPermit purchaseOrderTransferWarehouseInventoryUnloadingPermit);
    }
}