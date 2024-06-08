using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Sepehr.Application.DTOs.Order;
using Sepehr.Application.Exceptions;
using Sepehr.Application.Features.Products.Command.UpdateProduct;
using Sepehr.Application.Interfaces.Repositories;
using Sepehr.Application.Wrappers;
using Sepehr.Domain.Entities;
using Sepehr.Domain.Enums;

namespace Sepehr.Application.Features.Orders.Command.UpdateOrder
{
    public class UpdateOrderCommand : IRequest<Response<Order>>
    {
        public Guid Id { get; set; }
        public required Guid CustomerId { get; set; }
        public decimal TotalAmount { get; set; }
        public string? Description { get; set; }
        public int OrderExitTypeId { get; set; }
        public int OrderSendTypeId { get; set; }
        public required int ProductBrandId { get; set; }
        public int PaymentTypeId { get; set; }
        public int InvoiceTypeId { get; set; }
        public OrderType OrderTypeId { get; set; }
        public bool IsTemporary { get; set; }
        public int? CustomerOfficialCompanyId { get; set; }
        public string DeliverDate { get; set; }

        public virtual List<OrderDetailRequest> Details { get; set; }
        public virtual List<OrderPaymentDto>? OrderPayments { get; set; }
        public virtual List<OrderServiceDto>? OrderServices { get; set; }

        public class UpdateOrderCommandHandler : IRequestHandler<UpdateOrderCommand, Response<Order>>
        {
            private readonly IOrderRepositoryAsync _orderRepository;
            private readonly IPurchaseOrderRepositoryAsync _purchaseOrderRepository;
            private readonly IProductInventoryRepositoryAsync _productInventory;
            private readonly ICargoAnnouncementRepositoryAsync _cargoAnnouncement;
            private readonly IMapper _mapper;

            public UpdateOrderCommandHandler(IOrderRepositoryAsync orderRepository, 
                IProductInventoryRepositoryAsync productInventory,
                ICargoAnnouncementRepositoryAsync cargoAnnouncement,
                IMapper mapper,
                IPurchaseOrderRepositoryAsync purchaseOrderRepository)
            {
                _orderRepository = orderRepository;
                _productInventory = productInventory;
                _cargoAnnouncement = cargoAnnouncement;
                _mapper = mapper;
                _purchaseOrderRepository = purchaseOrderRepository;

            }
            public async Task<Response<Order>> Handle(UpdateOrderCommand command, CancellationToken cancellationToken)
            {
                try
                {
                    var order = await _orderRepository.GetByIdAsync(command.Id);

                    if (order == null)
                        throw new ApiException($"سفارش یاقت نشد !");
                    else
                    {
                        #region در صورتی که تعدادی از کالاها از انبار واسط باشند یک سفارش خرید هم ثبت می شود
                        List<ProductInventory> productTypes =
                            await _productInventory.GetProductInventory(command.Details);


                        List<PurchaseOrder> _lstPurchaseOrder = new List<PurchaseOrder>();
                        if (productTypes.Count() > 0)
                        {
                            var purchaseOrder = command;
                            foreach (var item in command.Details
                                                .Where(t => productTypes.Select(p => p.ProductBrandId)
                                                .Contains(t.ProductBrandId)).GroupBy(t => new { t.PurchaserCustomerId }))
                            {
                                purchaseOrder.Details = command.Details.Where(x =>
                                        x.PurchaserCustomerId == item.Key.PurchaserCustomerId &&
                                        productTypes.Select(t => t.ProductBrandId)
                                        .Contains(x.ProductBrandId)).ToList();

                                var newPurOrder = _mapper.Map<PurchaseOrder>(purchaseOrder);
                                _lstPurchaseOrder.Add(newPurOrder);
                            }

                            var lstPurchaseOrders = await _purchaseOrderRepository.CreateOrderForIntermediatProducts(_lstPurchaseOrder);
                        }
                        #endregion


                        order = _mapper.Map(command, order);
                        order = await _orderRepository.UpdateOrder(order);

                        return new Response<Order>(order, "");
                    }
                }
                catch (Exception e)
                {

                    throw;
                }
            }
        }
    }
}