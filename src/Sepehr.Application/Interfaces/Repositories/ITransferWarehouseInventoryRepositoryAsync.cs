using AutoMapper;
using Sepehr.Application.Features.PurchaseOrders.Command.TransferPurchaseOrder;
using Sepehr.Application.Features.TransferWarehouseInventories.Queries.GetAllTransferWarehouseInventories;
using Sepehr.Domain.Entities;

namespace Sepehr.Application.Interfaces.Repositories
{
    public interface ITransferWarehouseInventoryRepositoryAsync : IGenericRepositoryAsync<TransferWarehouseInventory>
    {
        Task<IEnumerable<TransferWarehouseInventory>> GetAllTransferWarehouseInventoriesAsync(GetAllTransferWarehouseInventoriesParameter validFilter);
        Task<TransferWarehouseInventory> UpdateTransferWarehouseInventory(TransferWarehouseInventory transRemittance);
        Task<TransferWarehouseInventory> CreateTransferWarehouseInventory(TransferWarehouseInventory transRemittance);
        Task DeleteTransferInventory(int id);
    }
}