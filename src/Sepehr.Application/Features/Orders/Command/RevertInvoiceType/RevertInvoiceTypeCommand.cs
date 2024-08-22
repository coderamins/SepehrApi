using AutoMapper;
using MediatR;
using Sepehr.Application.DTOs;
using Sepehr.Application.DTOs.Order;
using Sepehr.Application.Exceptions;
using Sepehr.Application.Interfaces.Repositories;
using Sepehr.Application.Wrappers;
using Sepehr.Domain.Entities;
using Sepehr.Domain.Enums;

namespace Sepehr.Application.Features.Orders.Command.ApproveInvoiceType
{
    public class RevertInvoiceTypeCommand : IRequest<Response<bool>>
    {
        public Guid OrderId { get; set; }

        public class ApproveInvoiceTypeCommandHandler : IRequestHandler<ApproveInvoiceTypeCommand, Response<bool>>
        {
            private readonly IOrderRepositoryAsync _orderRepository;
            private readonly IMapper _mapper;
            public ApproveInvoiceTypeCommandHandler(
                IOrderRepositoryAsync orderRepository,
                IMapper mapper
                )
            {
                _orderRepository = orderRepository;
                _mapper = mapper;
            }
            public async Task<Response<bool>> Handle(ApproveInvoiceTypeCommand command, CancellationToken cancellationToken)
            {
                try
                {
                    var order = await _orderRepository.GetOrderForUpdateInvoiceType(command.OrderId);
                    if (order.InvoiceTypeId != (int)EInvoiceType.Formal)
                        throw new ApiException("فاکتور سفارش رسمی نمی باشد !");

                    if (order.OrderStatusId != (int)OrderStatusEnum.AccApproved)
                        throw new ApiException("وضعیت سفارش نامعتبر می باشد !");
                    
                    order = _mapper.Map(command, order);

                    order.OrderStatusId = (int)OrderStatusEnum.AccNotApproved;
                    await _orderRepository.RevertOrderInvoiceType(order);


                    return new Response<bool>(true);

                }
                catch (Exception e)
                {
                    throw;
                }
                //return new Response<bool>(await _orderRepository.ApproveInvoiceType(request.OrderId));
            }
        }

    }

}
