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

namespace Sepehr.Application.Features.Orders.Command.ConfirmPurchaseOrder
{
    public class ConfirmPurchaseOrderCommand:IRequest<Response<bool>>
    {
        public Guid OrderId { get; set; }
        public class ConfirmPurchaseOrderCommandHandler : IRequestHandler<ConfirmPurchaseOrderCommand, Response<bool>>
        {
            private readonly IPurchaseOrderRepositoryAsync _purchaseOrderRepository;
            public ConfirmPurchaseOrderCommandHandler(IPurchaseOrderRepositoryAsync purchaseOrderRepository)
            {
                _purchaseOrderRepository = purchaseOrderRepository;
            }
            public async Task<Response<bool>> Handle(ConfirmPurchaseOrderCommand request, CancellationToken cancellationToken)
            {
                return new Response<bool>(await _purchaseOrderRepository.ConfirmPurchaseOrder(request.OrderId)
                    , "سفارش خرید با موفقیت تایید شد");

                //return new Response<bool>(true, "سفارش با موفقیت تایید شد .");
            }
        }

    }
}
