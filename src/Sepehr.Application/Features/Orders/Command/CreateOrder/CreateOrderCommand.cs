using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Sepehr.Application.DTOs.Order;
using Sepehr.Application.DTOs.PurchaseOrder;
using Sepehr.Application.DTOs.Sms;
using Sepehr.Application.Exceptions;
using Sepehr.Application.Interfaces;
using Sepehr.Application.Interfaces.Repositories;
using Sepehr.Application.Wrappers;
using Sepehr.Domain.Entities;
using Sepehr.Domain.Enums;
using Sepehr.Domain.ViewModels;

namespace Sepehr.Application.Features.Orders.Command.CreateOrder
{
    public partial class CreateOrderCommand : IRequest<Response<Order>>
    {
        public required Guid CustomerId { get; set; }
        public decimal TotalAmount { get; set; }
        public string? Description { get; set; }
        public int OrderExitTypeId { get; set; }
        public int OrderSendTypeId { get; set; }
        public int PaymentTypeId { get; set; }
        public required OrderType OrderTypeId { get; set; }
        public string? CustomerOfficialName { get; set; }
        public int? CustomerOfficialCompanyId { get; set; }
        public int InvoiceTypeId { get; set; }
        public string? FreightName { get; set; }
        public bool IsTemporary { get; set; }
        public string DeliverDate { get; set; }

        /// <summary>
        /// در صورتی که ارسال توسط مهفام باشد
        /// </summary>
        public string? DischargePlaceAddress { get; set; }
        /// <summary>
        /// در صورتی که ارسال به عهده مشتری باشد
        /// </summary>
        public string? FreightDriverName { get; set; }
        /// <summary> در صورتی که ارسال به عهده مشتری باشد </summary>
        public string? CarPlaque { get; set; }
        public virtual List<OrderDetailRequest> Details { get; set; }
        public virtual List<OrderPaymentDto> OrderPayments { get; set; }
        public virtual List<OrderServiceDto>? OrderServices { get; set; }


    }
    public class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand, Response<Order>>
    {
        private readonly IOrderRepositoryAsync _orderRepository;
        private readonly IPurchaseOrderRepositoryAsync _purOrderRepository;
        private readonly IProductInventoryRepositoryAsync _productInventory;
        private readonly IProductRepositoryAsync _productRepository;
        private readonly IMapper _mapper;
        private readonly ISmsService _smsService;
        public CreateOrderCommandHandler(
            IOrderRepositoryAsync orderRepository,
            IPurchaseOrderRepositoryAsync purOrderRepository,
            IProductRepositoryAsync productRepository,
            IProductInventoryRepositoryAsync productInventory,
            IMapper mapper, ISmsService smsService)
        {
            _orderRepository = orderRepository;
            _purOrderRepository = purOrderRepository;
            _productInventory = productInventory;
            _productRepository = productRepository;
            _mapper = mapper;
            _smsService = smsService;
        }

        public async Task<Response<Order>> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
        {
            try
            {
                List<Order> createdOrders = new List<Order>();

                var order = _mapper.Map<Order>(request);

                #region در صورتی که تعدادی از کالاها از انبار واسط باشند یک سفارش خرید هم ثبت می شود
                List<ProductInventory> productTypes =
                    await _productInventory.GetProductInventory(request.Details);

                if (productTypes.Count() > 0)
                {
                    var buyOrder = request;

                    buyOrder.Details = request.Details.Where(x =>
                                productTypes.Select(t => t.ProductBrandId).Contains(x.ProductBrandId)).ToList();
                    //.Add(
                    //request.Details
                    //.First(d => d.ProductBrandId == item.ProductBrandId
                    //       && d.WarehouseId == item.WarehouseId));

                    var newPurOrder = _mapper.Map<PurchaseOrder>(buyOrder);
                    await _purOrderRepository.CreateOrderForIntermediatProducts(newPurOrder);
                }
                #endregion

                Order newOrder = await _orderRepository.CreateOrder(order);

                //await _smsService.SendAsync(new SmsRequest
                //{
                //    Mobile = order.Customer.Mobile,
                //    Message = $"مشتری گرامی \n سفارش شما به شماره {order.OrderCode} دریافت شد . \n  شرکت فولاد سپهر ایرانیان"
                //});

                return new Response<Order>(newOrder, $"سفارش جدید با شناسه {newOrder.OrderCode} موفقیت ثبت شد .");
            }
            catch (Exception e)
            {

                throw;
            }
        }



    }
}