using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Sepehr.Application.Features.Orders.Queries.GetAllOrders;
using Sepehr.Application.Interfaces.Repositories;
using Sepehr.Application.Wrappers;
using Sepehr.Domain.Entities;
using Sepehr.Domain.Enums;
using Sepehr.Domain.ViewModels;

namespace Sepehr.Application.Features.Orders.Queries.GetAllOrders
{
    public class GetAllOrdersQuery : IRequest<PagedResponse<IEnumerable<OrderViewModel>>>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public IEnumerable<int>? InvoiceTypeId { get; set; }
        public int? OrderStatusId { get; set; }
        public long? OrderCode { get; set; }
        public OrderType? OrderType { get; set; }
    }
    public class GetAllOrdersQueryHandler :
         IRequestHandler<GetAllOrdersQuery, PagedResponse<IEnumerable<OrderViewModel>>>
    {
        private readonly IOrderRepositoryAsync _orderRepository;
        private readonly IMapper _mapper;
        public GetAllOrdersQueryHandler(IOrderRepositoryAsync orderRepository, IMapper mapper)
        {
            _orderRepository = orderRepository;
            _mapper = mapper;
        }

        public async Task<PagedResponse<IEnumerable<OrderViewModel>>> Handle(GetAllOrdersQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var validFilter = _mapper.Map<GetAllOrdersParameter>(request);
                var order = await _orderRepository.GetAllOrdersAsync(validFilter);

                var orderViewModel = _mapper.Map<IEnumerable<OrderViewModel>>(
                    order.Skip((validFilter.PageNumber - 1) * validFilter.PageSize)
                    .Take(validFilter.PageSize).ToList());

                return new PagedResponse<IEnumerable<OrderViewModel>>(orderViewModel,
                    validFilter.PageNumber,
                    validFilter.PageSize, order.Count());
            }
            catch (Exception e)
            {

                throw;
            }         
        }   
    }
}