using AutoMapper;
using Sepehr.Application.Features.PurchaseOrders.Command.TransferPurchaseOrder;
using Sepehr.Application.Features.PurchaseOrders.Queries.GetAllPurchaseOrders;
using Sepehr.Domain.Entities;

namespace Sepehr.Application.Interfaces.Repositories
{
    public interface IPurchaseOrderRepositoryAsync : IGenericRepositoryAsync<PurchaseOrder>
    {
        Task<bool> AddAttachmnets(List<Attachment> purchaseOrderAttachments, Guid purchaseOrderId);

        //void AddNewAttachments(ICollection<Attachment>? attachments);
        Task<bool> ApproveInvoiceType(Guid purchaseOrderId);
        Task<bool> ConfirmPurchaseOrder(Guid purchaseOrderId);
        Task<IEnumerable<PurchaseOrder>> GetAllNotSendedPurchaseOrders();
        Task<IQueryable<PurchaseOrder>> GetAllPurchaseOrdersAsync(GetAllPurchaseOrdersParameter parameter);
        Task<PurchaseOrder> GetPurchaseOrderById(Guid id);
        Task<PurchaseOrder> GetPurchaseOrderInfo(long purchaseOrderCode);
        Task<bool> IsUniqueBarcodeAsync(string barcode);
        void RemoveRelatedDetails(Guid id);
        Task<bool> ReturnPurchaseOrder(Guid purchaseOrderId);
        Task<PurchaseOrder> UpdatePurchaseOrder(PurchaseOrder purchaseOrder);
        Task<PurchaseOrder> CreateOrder(PurchaseOrder purchaseOrder);
        Task<List<PurchaseOrder>> CreateOrderForIntermediatProducts(List<PurchaseOrder> orders);
        Task<bool> DeletePurchaseOrder(Guid id);
        Task<bool> CancelPurchaseOrder(Guid orderId);
        Task UpdatePurchaseOrderDetail(ICollection<PurchaseOrderDetail> details, int warehouseId);
        Task<PurchaseOrder?> GetPurchaseOrderByIdAsQueryble(Guid purchaseOrderId);
        Task<PurchaseOrderTransfer> TranserPurchaseOrder(TransferPurchaseOrderCommand command);
    }
}