using MediatR;
using Sepehr.Application.Events.OrderEvents;
using Sepehr.Application.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sepehr.Application.Features.ReturnOrders.Command
{
    public class ReturnOrderCommand
    {
        public Guid OrderId { get; set; }
        public List<Guid> ItemIdsToReturn { get; set; }
        public string ReturnReason { get; set; }
    }

    //public class ReturnOrderCommandHandler : IRequestHandler<ReturnOrderCommand>
    //{
    //    //private readonly IEventBus _eventBus;
    //    private readonly IOrderRepositoryAsync _orderRepository;

    //    public async Task<Unit> Handle(ReturnOrderCommand request, CancellationToken cancellationToken)
    //    {
    //        var order = await _orderRepository.GetByIdAsync(request.OrderId);
    //        //if (order == null)
    //        //    throw new َApiException("سفارش یافت نشد");
    

    //        //order.ReturnItems(request.ItemIdsToReturn, request.ReturnReason);

    //        //var @event = new OrderReturnedEvent
    //        //{
    //        //    OrderId = order.Id,
    //        //    ReturnedItemIds = request.ItemIdsToReturn,
    //        //    ReturnDate = DateTime.Now,
    //        //    ReturnReason = request.ReturnReason
    //        //};

    //        //await _eventBus.PublishAsync(@event, cancellationToken);

    //        return Unit.Value;
    //    }
    //}
}
