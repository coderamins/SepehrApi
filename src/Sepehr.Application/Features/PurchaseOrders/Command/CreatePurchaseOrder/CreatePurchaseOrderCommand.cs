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

namespace Sepehr.Application.Features.PurchaseOrders.Command.CreatePurchaseOrder
{
    public partial class CreatePurchaseOrderCommand : IRequest<Response<PurchaseOrder>>
    {
        public Guid? Id { get; set; }
        public required Guid CustomerId { get; set; }
        public decimal TotalAmount { get; set; }
        public string? Description { get; set; }
        public ExitType ExitType { get; set; }
        public int PurchaseOrderSendTypeId { get; set; }
        public int PaymentTypeId { get; set; }
        public int? CustomerOfficialCompanyId { get; set; }
        public int InvoiceTypeId { get; set; }
        public int OriginWarehouseId { get; set; }
        public int DestinationWarehouseId { get; set; }
        public bool IsTemporary { get; set; }

        public virtual List<CreatePurchaseOrderDetailRequest> Details { get; set; }
        public virtual List<PurchaseOrderPaymentDto> OrderPayments { get; set; }
        public virtual List<OrderServiceDto>? OrderServices { get; set; }

    }
    public class CreatePurchaseOrderCommandHandler : IRequestHandler<CreatePurchaseOrderCommand, Response<PurchaseOrder>>
    {   
        private readonly IPurchaseOrderRepositoryAsync _purchaseOrderRepository;
        private readonly IProductInventoryRepositoryAsync _productInventory;
        //private readonly IProductRepositoryAsync _productRepository;
        private readonly IMapper _mapper;
        private readonly ISmsService _smsService;
        public CreatePurchaseOrderCommandHandler(
            IPurchaseOrderRepositoryAsync purchaseOrderRepository,
            IProductRepositoryAsync productRepository,
            IProductInventoryRepositoryAsync productInventory,
            IMapper mapper, ISmsService smsService)
        {
            _purchaseOrderRepository = purchaseOrderRepository;
            _productInventory = productInventory;
            //_productRepository = productRepository;
            _mapper = mapper;
            _smsService = smsService;
        }

        public async Task<Response<PurchaseOrder>> Handle(CreatePurchaseOrderCommand request, CancellationToken cancellationToken)
        {
            try
            {
                List<PurchaseOrder> createdPurchaseOrders = new List<PurchaseOrder>();
                var purchaseOrder = _mapper.Map<PurchaseOrder>(request);
                var newOrder =await _purchaseOrderRepository.CreateOrder(purchaseOrder);

                //await _smsService.SendAsync(new SmsRequest
                //{
                //    Mobile = purchaseOrder.Customer.Mobile,
                //    Message = $"مشتری گرامی \n سفارش شما به شماره {newOrder.OrderCode} دریافت شد . \n  شرکت فولاد سپهر ایرانیان"
                //});

                return new Response<PurchaseOrder>(newOrder, $"سفارش جدید با شناسه {purchaseOrder.OrderCode} موفقیت ثبت شد .");
            }
            catch (Exception e)
            {
                throw;
            }
        }



    }
}