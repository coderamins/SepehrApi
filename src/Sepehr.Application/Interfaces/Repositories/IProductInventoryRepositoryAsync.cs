using Sepehr.Application.DTOs.Order;
using Sepehr.Application.DTOs.Product;
using Sepehr.Application.DTOs.PurchaseOrder;
using Sepehr.Domain.Entities;
using Sepehr.Domain.Enums;
using Sepehr.Domain.ViewModels;

namespace Sepehr.Application.Interfaces.Repositories
{
    public interface IProductInventoryRepositoryAsync : IGenericRepositoryAsync<ProductInventory>
    {
        Task<ProductInventoryHistory?> CheckInventoryUploadHsitory(int productBrandId);
        Task CreateInventoryHistory(List<ProductInventoryHistory> inventoryHistoryList);
        Task<IEnumerable<DapperProduct>> GetProductInventories(int? warehouseTypeId,int? WarehouseId);
        Task<ProductInventory?> GetProductInventory(int productBrandId, int warehouseId);
        Task<List<ProductInventory>> GetProductInventory(List<OrderDetailRequest> detailDtos);
        Task<List<ProductInventory>?> GetProductsByInventory();
        Task<List<ProductInventoryHistory>?> GetUploadedInventoryFromHistory(string uploadedDate);
        Task<ProductInventory> UpdateProductInventory(int productBrandId, int amount);
        Task<bool> UpdateProductInventory(Guid CargoAnnounceId);        
        Task<bool> UpdateProductInventory(List<OrderDetailRequest> details);
        Task<bool> UpdateProductInventory(List<CreatePurchaseOrderDetailRequest> details);
        Task<bool> UpdateProductInventory(Guid orderId, List<OrderDetailRequest> details,InventoryActionType actionType);
    }
}