using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Sepehr.Application.Exceptions;
using Sepehr.Application.Interfaces.Repositories;
using Sepehr.Application.Wrappers;
using Sepehr.Domain.Entities;
using Sepehr.Domain.ViewModels;

namespace Sepehr.Application.Features.PurchaseOrders.Queries.GetPurchaseOrderById
{
    public class GetPurchaseOrderByCodeQuery : IRequest<Response<PurchaseOrderViewModel>>
    {
        public long purchaseOrderCode { get; set; }

        public class GetPurchaseOrderByCodeQueryHandler : IRequestHandler<GetPurchaseOrderByCodeQuery, Response<PurchaseOrderViewModel>>
        {
            private readonly IPurchaseOrderRepositoryAsync _purchaseOrderRepository;
            private readonly IMapper _mapper;
            public GetPurchaseOrderByCodeQueryHandler(
                IPurchaseOrderRepositoryAsync purchaseOrderRepository,
                IMapper mapper
            )
            {
                _purchaseOrderRepository = purchaseOrderRepository;
                _mapper= mapper;
            }

            public async Task<Response<PurchaseOrderViewModel>>
            Handle(
                GetPurchaseOrderByCodeQuery query,
                CancellationToken cancellationToken
            )
            {
                try
                {
                    var purchaseOrder = await _purchaseOrderRepository.GetPurchaseOrderInfo(query.purchaseOrderCode);
                    if (purchaseOrder == null)
                        throw new ApiException($"سفارش یافت نشد !");

                    var purchaseOrderVM = _mapper.Map<PurchaseOrderViewModel>(purchaseOrder);

                    return new Response<PurchaseOrderViewModel>(purchaseOrderVM);
                }
                catch (Exception e)
                {

                    throw;
                }
            }
        }
    }
}
