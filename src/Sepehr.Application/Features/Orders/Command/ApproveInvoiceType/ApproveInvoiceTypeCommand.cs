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
    public class ApproveInvoiceTypeCommand : IRequest<Response<bool>>
    {
        public Guid OrderId { get; set; }
        public int InvoiceTypeId { get; set; }
        public string InvoiceApproveDescription { get; set; } = string.Empty;
        public int? CustomerOfficialCompanyId { get; set; }

        public required List<ApproveInvoiceOrderDetail> Details { get; set; }

        public List<AttachmentDto>? Attachments { get; set; }

        public int OrderStatusId { get; set; }

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

                    if (order.OrderStatusId == command.OrderStatusId)
                        throw new ApiException(string.Format( "{0} قبلا انجام شده است !"
                            ,command.OrderStatusId== (int)OrderStatusEnum.AccApproved ? "تایید حسابداری سفارش":"عدم تایید حسابداری سفارش"));
                    
                    order = _mapper.Map(command, order);
                    foreach (var item in command.Details)
                    {
                        var d = order.Details.FirstOrDefault(d => d.Id == item.Id);
                        d.AlternativeProductBrandId = item.AlternativeProductBrandId;
                        d.AlternativeProductPrice = item.AlternativeProductPrice;
                        d.AlternativeProductAmount = item.AlternativeProductAmount;
                    }

                    await _orderRepository.ApproveOrderInvoiceType(order);


                    List<Attachment> orderAttachments = _mapper.Map<List<Attachment>>(command.Attachments);
                    orderAttachments.ForEach(a=>a.OrderId=command.OrderId);
                    orderAttachments.ForEach(a=>a.AttachmentType=AttachmentType.ApproveInvoiceType);

                    await _orderRepository.AddAttachmnets(orderAttachments,command.OrderId);
                    //await _orderRepository.UpdateAsync(order);
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
