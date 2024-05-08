using AutoMapper;
using MediatR;
using Sepehr.Application.Features.PurchaseOrders.Queries.GetAllPurchaseOrders;
using Sepehr.Application.Interfaces.Repositories;
using Sepehr.Application.Wrappers;
using Sepehr.Domain.ViewModels;

namespace Sepehr.Application.Features.TransferRemittances.Queries.GetTransferRemittanceById
{
    public class GetTransferRemittanceByPermitCodeQuery : IRequest<Response<TransferRemittanceViewModel>>
    {
        public int Id { get; set; }
    }
    public class GetTransferRemittanceByPermitCodeQueryHandler :
         IRequestHandler<GetTransferRemittanceByPermitCodeQuery, Response<TransferRemittanceViewModel>>
    {
        private readonly IPurchaseOrderTransferRemittanceRepositoryAsync _purchaseOrderRepository;
        private readonly IMapper _mapper;
        public GetTransferRemittanceByPermitCodeQueryHandler(IPurchaseOrderTransferRemittanceRepositoryAsync purchaseOrderRepository, IMapper mapper)
        {
            _purchaseOrderRepository = purchaseOrderRepository;
            _mapper = mapper;
        }

        public async Task<Response<TransferRemittanceViewModel>> Handle
            (GetTransferRemittanceByPermitCodeQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var transferRemittances = await _purchaseOrderRepository.GetTransferRemittanceByIdAsync(request.Id);

                var transferRemittanceViewModel = _mapper.Map<TransferRemittanceViewModel>(transferRemittances);

                return new Response<TransferRemittanceViewModel>(transferRemittanceViewModel);
            }
            catch (Exception e)
            {

                throw;
            }
        }
    }
}