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
    public class GetOrderByCodeQuery : IRequest<Response<OrderViewModel>>
    {
        public long orderCode { get; set; }

        public class GetOrderByCodeQueryHandler : IRequestHandler<GetOrderByCodeQuery, Response<OrderViewModel>>
        {
            private readonly IOrderRepositoryAsync _orderRepository;
            private readonly IMapper _mapper;
            public GetOrderByCodeQueryHandler(
                IOrderRepositoryAsync orderRepository,
                IMapper mapper
            )
            {
                _orderRepository = orderRepository;
                _mapper= mapper;
            }

            public async Task<Response<OrderViewModel>>
            Handle(
                GetOrderByCodeQuery query,
                CancellationToken cancellationToken
            )
            {
                try
                {
                    var order = await _orderRepository.GetOrderInfo(query.orderCode);
                    if (order == null)
                        throw new ApiException($"سفارش یافت نشد !");

                    var orderVM = _mapper.Map<OrderViewModel>(order);

                    return new Response<OrderViewModel>(orderVM);
                }
                catch (Exception e)
                {

                    throw;
                }
            }
        }
    }
}
