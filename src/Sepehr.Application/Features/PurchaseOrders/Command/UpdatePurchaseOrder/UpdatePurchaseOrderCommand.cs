using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Sepehr.Application.DTOs.Order;
using Sepehr.Application.DTOs.PurchaseOrder;
using Sepehr.Application.Exceptions;
using Sepehr.Application.Features.Products.Command.UpdateProduct;
using Sepehr.Application.Interfaces.Repositories;
using Sepehr.Application.Wrappers;
using Sepehr.Domain.Entities;
using Sepehr.Domain.Enums;

namespace Sepehr.Application.Features.PurchaseOrders.Command.UpdatePurchaseOrder
{
    public class UpdatePurchaseOrderCommand : IRequest<Response<PurchaseOrder>>
    {
        public Guid Id { get; set; }
        public required Guid CustomerId { get; set; }
        public decimal TotalAmount { get; set; }
        public string? Description { get; set; }
        public ExitType ExitType { get; set; }
        public int PurchaseOrderSendTypeId { get; set; }
        public required int ProductBrandId { get; set; }
        public int OriginWarehouseId { get; set; }
        public int DestinationWarehouseId { get; set; }
        public int PaymentTypeId { get; set; }
        public int InvoiceTypeId { get; set; }
        public bool IsTemporary { get; set; }
        public int? CustomerOfficialCompanyId { get; set; }


        public virtual List<UpdatePurchaseOrderDetailRequest> Details { get; set; }
        public virtual List<PurchaseOrderPaymentDto>? OrderPayments { get; set; }
        public virtual List<OrderServiceDto>? OrderServices { get; set; }

        public class UpdatePurchaseOrderCommandHandler : IRequestHandler<UpdatePurchaseOrderCommand, Response<PurchaseOrder>>
        {
            private readonly IPurchaseOrderRepositoryAsync _purchaseOrderRepository;
            private readonly IMapper _mapper;

            public UpdatePurchaseOrderCommandHandler(IPurchaseOrderRepositoryAsync purchaseOrderRepository, IMapper mapper)
            {
                _purchaseOrderRepository = purchaseOrderRepository;
                _mapper = mapper;
            }
            public async Task<Response<PurchaseOrder>> Handle(UpdatePurchaseOrderCommand command, CancellationToken cancellationToken)
            {
                try
                {
                    //var purchaseOrder = await _purchaseOrderRepository.GetPurchaseOrderById(command.Id);
                    var purchaseOrder = await _purchaseOrderRepository.GetByIdAsync(command.Id);

                    if (purchaseOrder == null)
                    {
                        throw new ApiException($"سفارش یاقت نشد !");
                    }
                    else
                    {
                        purchaseOrder = _mapper.Map(command, purchaseOrder);
                        purchaseOrder = await _purchaseOrderRepository.UpdatePurchaseOrder(purchaseOrder);

                        //await _purchaseOrderRepository.UpdateAsync(purchaseOrder);
                        return new Response<PurchaseOrder>(purchaseOrder, "");
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