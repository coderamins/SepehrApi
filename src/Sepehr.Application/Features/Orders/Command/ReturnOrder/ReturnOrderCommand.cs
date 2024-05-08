using MediatR;
using Microsoft.Extensions.Caching.Distributed;
using Sepehr.Application.Features.Captcha.Command;
using Sepehr.Application.Features.Captcha;
using Sepehr.Application.Wrappers;
using Sepehr.Domain.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sepehr.Application.Interfaces.Repositories;
using Sepehr.Application.DTOs.Order;
using Sepehr.Domain.Entities;

namespace Sepehr.Application.Features.Orders.Command.ReturnOrder
{
    public class ReturnOrderCommand:IRequest<Response<bool>>
    {
        public Guid OrderId { get; set; }
       
        public class ReturnOrderCommandHandler : IRequestHandler<ReturnOrderCommand, Response<bool>>
        {
            private readonly IOrderRepositoryAsync _orderRepository;
            private readonly IProductInventoryRepositoryAsync _productInventory;
            public ReturnOrderCommandHandler(
                IOrderRepositoryAsync orderRepository,
                IProductInventoryRepositoryAsync productInventory)
            {
                _orderRepository = orderRepository;
                _productInventory = productInventory;
            }
            public async Task<Response<bool>> Handle(ReturnOrderCommand request, CancellationToken cancellationToken)
            {
                await _productInventory.UpdateProductInventory(request.OrderId,
                    new List<OrderDetailRequest>(), Domain.Enums.InventoryActionType.DeleteOrder);

                return new Response<bool>(await _orderRepository.ReturnOrder(request.OrderId));

            }
        }

    }
}
