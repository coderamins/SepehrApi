using AutoMapper;
using MediatR;
using Sepehr.Application.DTOs.PurchaseOrder;
using Sepehr.Application.Interfaces.Repositories;
using Sepehr.Application.Wrappers;
using Sepehr.Domain.Entities;

namespace Sepehr.Application.Features.PurchaseOrders.Command.TransferPurchaseOrder
{
    public class TransferPurchaseOrderCommand : IRequest<Response<PurchaseOrderTransfer>>
    {
        public Guid OrderId { get; set; }
        public required int WarehouseId { get; set; }

        public required List<TransferPurchaseOrderDetailDto> TransferDetails { get; set; }

        public class TransferPurchaseOrderCommandHandler : IRequestHandler<TransferPurchaseOrderCommand, Response<PurchaseOrderTransfer>>
        {
            private readonly IPurchaseOrderRepositoryAsync _purchaseOrderRepository;
            private readonly IMapper _mapper;

            public TransferPurchaseOrderCommandHandler(
                IPurchaseOrderRepositoryAsync purchaseOrderRepository,
                ITransferRemittanceRepositoryAsync purchaseOrderTransferRepository,
                IWarehouseRepositoryAsync warehouseRepository,
                IMapper mapper)
            {
                _purchaseOrderRepository = purchaseOrderRepository;
                _mapper = mapper;
            }
            public async Task<Response<PurchaseOrderTransfer>> Handle(TransferPurchaseOrderCommand command, CancellationToken cancellationToken)
            {
                var transerOrder = await _purchaseOrderRepository.TranserPurchaseOrder(command);

                return new Response<PurchaseOrderTransfer>(transerOrder, "ثبت انتقال سفارش خرید با موفقیت انجام شد !");
            }
        }
    }
}