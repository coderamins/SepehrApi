using AutoMapper;
using MediatR;
using Sepehr.Application.Features.PurchaseOrders.Queries.GetAllPurchaseOrders;
using Sepehr.Application.Interfaces.Repositories;
using Sepehr.Application.Wrappers;
using Sepehr.Domain.ViewModels;

namespace Sepehr.Application.Features.TransferWarehouseInventories.Queries.GetTransferWarehouseInventoryById
{
    public class GetTransferWarehouseInventoryByIdQuery : IRequest<Response<TransferWarehouseInventoryViewModel>>
    {
        public int Id { get; set; }
    }
    public class GetTransferWarehouseInventoryByIdQueryQueryHandler :
         IRequestHandler<GetTransferWarehouseInventoryByIdQuery, Response<TransferWarehouseInventoryViewModel>>
    {
        private readonly ITransferWarehouseInventoryRepositoryAsync _purchaseOrderRepository;
        private readonly IMapper _mapper;
        public GetTransferWarehouseInventoryByIdQueryQueryHandler(ITransferWarehouseInventoryRepositoryAsync purchaseOrderRepository, IMapper mapper)
        {
            _purchaseOrderRepository = purchaseOrderRepository;
            _mapper = mapper;
        }

        public async Task<Response<TransferWarehouseInventoryViewModel>> Handle
            (GetTransferWarehouseInventoryByIdQuery request, CancellationToken cancellationToken)
        {
            var transferWarehouseInventorys = await _purchaseOrderRepository.GetTransferWarehouseInventoryByIdAsync(request.Id);
            var transferWarehouseInventoryViewModel = _mapper.Map<TransferWarehouseInventoryViewModel>(transferWarehouseInventorys);

            return new Response<TransferWarehouseInventoryViewModel>(transferWarehouseInventoryViewModel);
        }
    }
}