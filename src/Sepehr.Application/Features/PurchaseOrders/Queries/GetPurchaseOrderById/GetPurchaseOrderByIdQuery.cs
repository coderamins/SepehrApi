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
    public class GetPurchaseOrderByIdQuery : IRequest<Response<PurchaseOrderViewModel>>
    {
        public Guid Id { get; set; }

        public class GetPurchaseOrderByIdQueryHandler : IRequestHandler<GetPurchaseOrderByIdQuery, Response<PurchaseOrderViewModel>>
        {
            private readonly IPurchaseOrderRepositoryAsync _PurchaseOrderRepository;
            private readonly IMapper _mapper;
            public GetPurchaseOrderByIdQueryHandler(
                IPurchaseOrderRepositoryAsync PurchaseOrderRepository,
                IMapper mapper
            )
            {
                _PurchaseOrderRepository = PurchaseOrderRepository;
                _mapper= mapper;
            }

            public async Task<Response<PurchaseOrderViewModel>>
            Handle(
                GetPurchaseOrderByIdQuery query,
                CancellationToken cancellationToken
            )
            {
                var purchaseOrder = await _PurchaseOrderRepository.GetPurchaseOrderById(query.Id);
                if (purchaseOrder == null)
                    throw new ApiException($"سفارش یافت نشد !");

                var purchaseOrderVM=_mapper.Map<PurchaseOrderViewModel>(purchaseOrder);

                return new Response<PurchaseOrderViewModel>(purchaseOrderVM);
            }
        }
    }
}
