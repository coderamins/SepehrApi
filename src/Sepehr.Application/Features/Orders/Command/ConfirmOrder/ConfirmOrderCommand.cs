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

namespace Sepehr.Application.Features.Orders.Command.ConfirmOrder
{
    public class ConfirmOrderCommand:IRequest<Response<bool>>
    {
        public Guid OrderId { get; set; }
        public class ConfirmOrderCommandHandler : IRequestHandler<ConfirmOrderCommand, Response<bool>>
        {
            private readonly IOrderRepositoryAsync _orderRepository;
            public ConfirmOrderCommandHandler(IOrderRepositoryAsync orderRepository)
            {
                _orderRepository = orderRepository;
            }
            public async Task<Response<bool>> Handle(ConfirmOrderCommand request, CancellationToken cancellationToken)
            {
                return new Response<bool>(await _orderRepository.ConfirmOrder(request.OrderId));

                //return new Response<bool>(true, "سفارش با موفقیت تایید شد .");

            }
        }

    }
}
