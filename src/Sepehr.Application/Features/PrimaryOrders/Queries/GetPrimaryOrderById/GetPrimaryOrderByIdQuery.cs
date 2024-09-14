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

namespace Sepehr.Application.Features.DraftOrders.Queries.GetDraftOrderById
{
    public class GetDraftOrderByIdQuery : IRequest<Response<DraftOrderViewModel>>
    {
        public int Id { get; set; }

        public class GetDraftOrderByIdQueryHandler : IRequestHandler<GetDraftOrderByIdQuery, Response<DraftOrderViewModel>>
        {
            private readonly IDraftOrderRepositoryAsync _draftOrderRepository;
            private readonly IMapper _mapper;

            public GetDraftOrderByIdQueryHandler(
                IDraftOrderRepositoryAsync draftOrderRepository
                , IMapper mapper)
            {
                _draftOrderRepository = draftOrderRepository;
                _mapper = mapper;
            }

            public async Task<Response<DraftOrderViewModel>>
            Handle(
                GetDraftOrderByIdQuery query,
                CancellationToken cancellationToken
            )
            {
                var draftOrder = await _draftOrderRepository.GetDraftOrderById(query.Id);
                if (draftOrder == null)
                    throw new ApiException($"پیش نویس سفارش یافت نشد !");

                var _draftOrder=_mapper.Map<DraftOrderViewModel>( draftOrder); 

                return new Response<DraftOrderViewModel>(_draftOrder);
            }
        }
    }
}
