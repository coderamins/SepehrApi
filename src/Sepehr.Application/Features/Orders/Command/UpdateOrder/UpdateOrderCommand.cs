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
            private readonly IProductInventoryRepositoryAsync _productInventory;
            private readonly ICargoAnnouncementRepositoryAsync _cargoAnnouncement;
            private readonly IMapper _mapper;

            public UpdateOrderCommandHandler(IOrderRepositoryAsync orderRepository, 
                IProductInventoryRepositoryAsync productInventory,
                ICargoAnnouncementRepositoryAsync cargoAnnouncement,
                IMapper mapper)
            {
                _orderRepository = orderRepository;
                _productInventory = productInventory;
                _cargoAnnouncement=cargoAnnouncement;
                _mapper = mapper;
            }
            public async Task<Response<Order>> Handle(UpdateOrderCommand command, CancellationToken cancellationToken)
            {
                try
                {
                    var order = await _orderRepository.GetOrderById(command.Id);
                    var inventoryUpdate = await _productInventory.UpdateProductInventory(order.Id, command.Details,InventoryActionType.None);
                    if (!inventoryUpdate)
                        throw new ApiException("مشکلی در بروزرسانی انبار بوجود آمد !");

                    if (order == null)
                    {
                        throw new ApiException($"سفارش یاقت نشد !");
                    }
                    else
                    {
                        #region بررسی می شود که کالای مورد ویرایش اگر دارای بارگیری باشد، مقدار بارگیری شده از مقدار اصلی کمتر نباشد
                        foreach (var oitem in order.Details)
                        {
                            var od = command.Details.FirstOrDefault(d => d.Id == oitem.Id);
                            if (od != null)
                            {
                                List<CargoAnnounceDetail> od_cAnncs = await _cargoAnnouncement.GetCargoAnnouncesByOrderDetailId(od.Id);
                                if (od.ProximateAmount < od_cAnncs.Sum(c => c.LadingAmount))
                                    throw new ApiException(
                                        string.Format(@"مقدار اصلی نمی تواند از مقدار بارگیری شده کمتر باشد !"+"({0} {1}) ",
                                        od_cAnncs.First(d=>d.OrderDetailId==od.Id).OrderDetail.ProductBrand.Product.ProductName,
                                        od_cAnncs.First(d=>d.OrderDetailId==od.Id).OrderDetail.ProductBrand.Brand.Name
                                        ));
                            }
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