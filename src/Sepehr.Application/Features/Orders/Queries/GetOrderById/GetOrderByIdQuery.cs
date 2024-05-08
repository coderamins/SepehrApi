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

namespace Sepehr.Application.Features.Orders.Queries.GetOrderById
{
    public class GetOrderByIdQuery : IRequest<Response<OrderViewModel>>
    {
        public Guid Id { get; set; }

        public class GetOrderByIdQueryHandler : IRequestHandler<GetOrderByIdQuery, Response<OrderViewModel>>
        {
            private readonly IOrderRepositoryAsync _OrderRepository;
            private readonly IMapper _mapper;
            public GetOrderByIdQueryHandler(
                IOrderRepositoryAsync OrderRepository,
                IMapper mapper
            )
            {
                _OrderRepository = OrderRepository;
                _mapper= mapper;
            }

            public async Task<Response<OrderViewModel>>
            Handle(
                GetOrderByIdQuery query,
                CancellationToken cancellationToken
            )
            {
                var order = await _OrderRepository.GetOrderById(query.Id);
                if (order == null)
                    throw new ApiException($"سفارش یافت نشد !");

                var orderVM=_mapper.Map<OrderViewModel>(order);

                return new Response<OrderViewModel>(orderVM);
            }
        }
    }
}
