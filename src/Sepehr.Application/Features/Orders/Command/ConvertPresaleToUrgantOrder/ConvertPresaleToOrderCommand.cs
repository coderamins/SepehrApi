using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Azure.Core;
using MediatR;
using Sepehr.Application.DTOs.Order;
using Sepehr.Application.DTOs.PurchaseOrder;
using Sepehr.Application.Exceptions;
using Sepehr.Application.Features.Products.Command.UpdateProduct;
using Sepehr.Application.Features.PurchaseOrders.Command.CreatePurchaseOrder;
using Sepehr.Application.Interfaces.Repositories;
using Sepehr.Application.Wrappers;
using Sepehr.Domain.Entities;
using Sepehr.Domain.Enums;

namespace Sepehr.Application.Features.Orders.Command.UpdateOrder
{
    public class ConvertPresaleToOrderCommand : IRequest<Response<bool>>
    {
        public Guid Id { get; set; }

        public class ConvertPresaleToOrderCommandHandler : IRequestHandler<ConvertPresaleToOrderCommand, Response<bool>>
        {
            private readonly IOrderRepositoryAsync _orderRepository;
            private readonly IMapper _mapper;

            public ConvertPresaleToOrderCommandHandler(IOrderRepositoryAsync orderRepository,
                IMapper mapper)
            {
                _orderRepository = orderRepository;
                _mapper = mapper;
            }
            public async Task<Response<bool>> Handle(ConvertPresaleToOrderCommand command, CancellationToken cancellationToken)
            {
                var order = await _orderRepository.GetOrderById(command.Id);
                if (order == null)
                    throw new ApiException("سفارش یافت نشد !");

                await _orderRepository.ConvertPreSaleOrderToUrgant(order);

                return new Response<bool>(true, "سفارش پیش فروش با موفقیت تایید و تبدیل شد . ");
            }
        }
    }
}