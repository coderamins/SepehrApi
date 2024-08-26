using AutoMapper;
using MediatR;
using Sepehr.Application.Features.PurchaseOrders.Queries.GetAllPurchaseOrders;
using Sepehr.Application.Interfaces.Repositories;
using Sepehr.Application.Wrappers;
using Sepehr.Domain.ViewModels;

namespace Sepehr.Application.Features.TransferRemittances.Queries.GetTransferRemittanceById
{
    public class GetTransferRemittanceByIdQuery : IRequest<Response<TransferRemittanceViewModel>>
    {
        public int Id { get; set; }
    }
    public class GetTransferRemittanceByIdQueryQueryHandler :
         IRequestHandler<GetTransferRemittanceByIdQuery, Response<TransferRemittanceViewModel>>
    {
        private readonly ITransferRemittanceRepositoryAsync _purchaseOrderRepository;
        private readonly IMapper _mapper;
        public GetTransferRemittanceByIdQueryQueryHandler(ITransferRemittanceRepositoryAsync purchaseOrderRepository, IMapper mapper)
        {
            _purchaseOrderRepository = purchaseOrderRepository;
            _mapper = mapper;
        }

        public async Task<Response<TransferRemittanceViewModel>> Handle
            (GetTransferRemittanceByIdQuery request, CancellationToken cancellationToken)
        {
            var transferRemittances = await _purchaseOrderRepository.GetTransferRemittanceByIdAsync(request.Id);
            var transferRemittanceViewModel = _mapper.Map<TransferRemittanceViewModel>(transferRemittances);

            return new Response<TransferRemittanceViewModel>(transferRemittanceViewModel);
        }
    }
}