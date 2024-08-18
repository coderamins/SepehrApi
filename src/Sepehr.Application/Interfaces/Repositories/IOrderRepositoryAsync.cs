using AutoMapper;
using Sepehr.Application.Features.CargoAnnouncements.Queries.GetAllNotSendedOrders;
using Sepehr.Application.Features.Orders.Queries.GetAllOrders;
using Sepehr.Domain.Entities;

namespace Sepehr.Application.Interfaces.Repositories
{
    public interface IOrderRepositoryAsync : IGenericRepositoryAsync<Order>
    {
        Task<bool> AddAttachmnets(List<Attachment> orderAttachments, Guid orderId);

        //void AddNewAttachments(ICollection<Attachment>? attachments);
        Task<bool> ApproveInvoiceType(Guid orderId);
        Task<bool> ApproveOrderInvoiceType(Order order);
        Task<bool> ConfirmOrder(Guid orderId);
        Task ConvertPreSaleOrderToUrgant(Order order);
        Task<Order> CreateOrder(Order order);
        Task<IEnumerable<Order>> GetAllNotSendedOrders(GetNotAnnouncedOrdersParameter param);
        Task<IQueryable<Order>> GetAllOrdersAsync(GetAllOrdersParameter parameter);
        Task<Order> GetOrderById(Guid id);
        Task<OrderDetail?> GetOrderDetailInfo(int orderDetailId);
        Task<Order> GetOrderForUpdateInvoiceType(Guid orderId);
        Task<Order> GetOrderInfo(long orderCode);
        Task<bool> IsUniqueBarcodeAsync(string barcode);
        void RemoveRelatedDetails(Guid id);
        Task<bool> ReturnOrder(Guid orderId);
        Task<Order> UpdateOrder(Order order);
    }
}