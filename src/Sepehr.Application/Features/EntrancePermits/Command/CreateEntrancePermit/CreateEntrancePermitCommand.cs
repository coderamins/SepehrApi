using AutoMapper;
using MediatR;
using Sepehr.Application.DTOs;
using Sepehr.Application.DTOs.Order;
using Sepehr.Application.Interfaces;
using Sepehr.Application.Interfaces.Repositories;
using Sepehr.Application.Wrappers;
using Sepehr.Domain.Entities;
using Sepehr.Domain.Enums;

namespace Sepehr.Application.Features.EntrancePermits.Command.CreateEntrancePermit
{
    public partial class CreateEntrancePermitCommand : IRequest<Response<EntrancePermit>>
    {
        public int PurchaseOrderTransferRemittanceId { get; set; }
        public List<AttachmentDto>? Attachments { get; set; }
    }

    public class CreateEntrancePermitCommandHandler : IRequestHandler<CreateEntrancePermitCommand, Response<EntrancePermit>>
    {   
        private readonly IEntrancePermitRepositoryAsync __entrancePermitRepository;
        private readonly IProductInventoryRepositoryAsync _productInventory;
        private readonly IMapper _mapper;
        private readonly ISmsService _smsService;
        public CreateEntrancePermitCommandHandler(
            IEntrancePermitRepositoryAsync _entrancePermitRepository,
            IProductRepositoryAsync productRepository,
            IProductInventoryRepositoryAsync productInventory,
            IMapper mapper, ISmsService smsService)
        {
            __entrancePermitRepository = _entrancePermitRepository;
            _productInventory = productInventory;
            _mapper = mapper;
            _smsService = smsService;
        }

        public async Task<Response<EntrancePermit>> Handle(CreateEntrancePermitCommand request, CancellationToken cancellationToken)
        {
            try
            {
                List<EntrancePermit> createdEntrancePermits = new List<EntrancePermit>();
                var _entrancePermit = _mapper.Map<EntrancePermit>(request);
                var newOrder =await __entrancePermitRepository.CreateEntrancePermit(_entrancePermit);

                //await _smsService.SendAsync(new SmsRequest
                //{
                //    Mobile = _entrancePermit.Customer.Mobile,
                //    Message = $"مشتری گرامی \n سفارش شما به شماره {newOrder.OrderCode} دریافت شد . \n  شرکت فولاد سپهر ایرانیان"
                //});

                return new Response<EntrancePermit>(newOrder, $"سفارش جدید با شناسه {_entrancePermit.OrderCode} موفقیت ثبت شد .");
            }
            catch (Exception e)
            {
                throw;
            }
        }



    }
}