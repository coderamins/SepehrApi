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
    public class ApprovePurchaseOrderInvoiceTypeCommand : IRequest<Response<bool>>
    {
        public Guid OrderId { get; set; }
        public int InvoiceTypeId { get; set; }
        public string InvoiceApproveDescription { get; set; } = string.Empty;
        //public int? CustomerOfficialCompanyId { get; set; }
        public int OrderStatusId { get; set; }

        public required List<ApproveInvoiceOrderDetail> Details { get; set; }
        public List<AttachmentDto>? Attachments { get; set; }

        public class ApprovePurchaseOrderInvoiceTypeCommandHandler : IRequestHandler<ApprovePurchaseOrderInvoiceTypeCommand, Response<bool>>
        {
            private readonly IPurchaseOrderRepositoryAsync _orderRepository;
            private readonly IMapper _mapper;
            public ApprovePurchaseOrderInvoiceTypeCommandHandler(
                IPurchaseOrderRepositoryAsync orderRepository,
                IMapper mapper
                )
            {
                _orderRepository = orderRepository;
                _mapper = mapper;
            }
            public async Task<Response<bool>> Handle(ApprovePurchaseOrderInvoiceTypeCommand command, CancellationToken cancellationToken)
            {
                try
                {
                    if (command.Attachments != null)
                        command.Attachments.ForEach(a => a.AttachmentType = AttachmentType.ApproveInvoiceType);

                    var order = await _orderRepository.GetByIdAsync(command.OrderId);
                    if (order == null)
                        throw new ApiException("سفارش یافت نشد !");
                    if (order.InvoiceTypeId != (int)Domain.Enums.PurchaseInvoiceType.Official)
                        throw new ApiException("فاکتور سفارش رسمی نمی باشد !");

                    if (order.OrderStatusId == command.OrderStatusId)
                        throw new ApiException(string.Format("{0} قبلا انجام شده است !"
                            , command.OrderStatusId == 2 ? "تایید حسابداری سفارش" : "عدم تایید حسابداری سفارش"));

                    order = _mapper.Map(command, order);
                    order.Details.Clear();
                    //foreach (var item in command.Details)
                    //{
                    //    var d = order.Details.FirstOrDefault(d => d.Id == item.Id);
                    //    d.AlternativeProductBrandId = item.AlternativeProductBrandId;
                    //    d.AlternativeProductPrice = item.AlternativeProductPrice;
                    //    d.AlternativeProductAmount = item.AlternativeProductAmount;
                    //}

                    //List<Attachment> orderAttachments = _mapper.Map<List<Attachment>>(command.Attachments);
                    ////.ForEach(a=>a.OrderId=command.OrderId);
                    //orderAttachments.ForEach(a=>a.AttachmentType=AttachmentType.ApproveInvoiceType);

                    //await _orderRepository.AddAttachmnets(orderAttachments,command.OrderId);
                    //await _orderRepository.UpdateAsync(order);

                    await _orderRepository.ApprovePurchaseOrder(order);
                    return new Response<bool>(true, "تایید فاکتور سفارش با موفقیت انجام شد .");

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
