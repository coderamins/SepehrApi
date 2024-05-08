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
using Sepehr.Application.Features.Orders.Command.UpdateOrder;
using Sepehr.Domain.Entities;
using AutoMapper;
using Sepehr.Application.Exceptions;
using Sepehr.Domain.Enums;

namespace Sepehr.Application.Features.Orders.Command.ConfirmOrder
{
    public class CompleteAnnouncementCommand:IRequest<Response<bool>>
    {
        public Guid OrderId { get; set; }
        public bool IsCompletlySended { get; set; }
        public class CompleteAnnouncementCommandHandler : IRequestHandler<CompleteAnnouncementCommand, Response<bool>>
        {
            private readonly IOrderRepositoryAsync _orderRepository;
            private readonly IMapper _mapper;
            public CompleteAnnouncementCommandHandler(IOrderRepositoryAsync orderRepository,IMapper mapper)
            {
                _orderRepository = orderRepository;
                _mapper=mapper; 
            }
            public async Task<Response<bool>> Handle(CompleteAnnouncementCommand request, CancellationToken cancellationToken)
            {
                var order = await _orderRepository.GetByIdAsync(request.OrderId);
                if (order.OrderStatusId == (int)OrderStatusEnum.Sended)// request.IsCompletlySended)
                    throw new ApiException("تکرار عملیات مجاز نمی باشد !");

                order.OrderStatusId = (int)OrderStatusEnum.Sended;
                order = _mapper.Map(request, order);

                await _orderRepository.UpdateAsync(order);
                return new Response<bool>(true, "سفارش با موفقیت به وضعیت تکمیل اعلام بار تبدیل شد .");
            }
        }

    }
}
