using AutoMapper;
using MediatR;
using Sepehr.Application.Features.CargoAnnouncements.Queries.GetAllCargoAnncs;
using Sepehr.Application.Features.CargoAnnouncements.Queries.GetAllNotSendedOrders;
using Sepehr.Application.Interfaces.Repositories;
using Sepehr.Application.Wrappers;
using Sepehr.Domain.Entities;
using Sepehr.Domain.ViewModels;

namespace Sepehr.Application.Features.CargoAnnouncements.Queries.GetAllCargoAnncs
{
    public class GetAllNotSendedOrdersQuery : IRequest<IEnumerable<OrderViewModel>>
    {
        public int? OrderCode { get; set; }
    }
    public class GetAllNotSendedOrdersQueryHandler :
         IRequestHandler<GetAllNotSendedOrdersQuery, IEnumerable<OrderViewModel>>
    {
        private readonly IOrderRepositoryAsync _orderRepository;
        private readonly IMapper _mapper;
        public GetAllNotSendedOrdersQueryHandler(IOrderRepositoryAsync orderRepository, IMapper mapper)
        {
            _orderRepository = orderRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<OrderViewModel>> Handle(GetAllNotSendedOrdersQuery request, CancellationToken cancellationToken)
        {
            var validParam = _mapper.Map<GetNotAnnouncedOrdersParameter>(request);
            var order = await _orderRepository.GetAllNotSendedOrders(validParam);

            var result = _mapper.Map<IEnumerable<OrderViewModel>>(order);
            return result;           
        }
    }
}