using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using Sepehr.Application.Interfaces.Repositories;
using Sepehr.Application.Wrappers;
using Sepehr.Domain.Entities;

namespace Sepehr.Application.Features.ReceivePays.Command.CreateReceivePay
{
    public partial class CreateReceivePayCommand : IRequest<Response<ReceivePay>>
    {
        /// <summary>
        /// نوع دریافت از 
        /// </summary>
        public int? ReceivePaymentTypeFromId { get; set; }

        /// <summary>
        /// نوع پرداخت به
        /// </summary>
        public int? ReceivePaymentTypeToId { get; set; }


        public string? AccountOwner { get; set; }
        public string? TrachingCode { get; set; }
        public string? CompanyName { get; set; }
        public string? ContractCode { get; set; }
        public decimal Amount { get; set; }
        public string? Description { get; set; }

        #region  دریافت از
        public required string ReceiveFromId { get; set; }
        #endregion

        #region پرداخت به
        public required string PayToId { get; set; }
        #endregion

        #region اگر مقصد یکی یا هر دو طرف مشتری باشد
        public int? ReceiveFromCompanyId { get; set; }
        public int? PayToCompanyId { get; set; }
        #endregion

        public IList<IFormFile>? Attachments { get; set; }

    }
    public class CreateReceivePayCommandHandler : IRequestHandler<CreateReceivePayCommand, Response<ReceivePay>>
    {
        private readonly IReceivePayRepositoryAsync _receivePayRepository;
        private readonly IMapper _mapper;
        public CreateReceivePayCommandHandler(IReceivePayRepositoryAsync receivePayRepository, IMapper mapper)
        {
            _receivePayRepository = receivePayRepository;
            _mapper = mapper;
        }        

        public async Task<Response<ReceivePay>> Handle(CreateReceivePayCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var receivePay = _mapper.Map<ReceivePay>(request);
                //receivePay.Attachments = null;
                await _receivePayRepository.AddAsync(receivePay);
                return new Response<ReceivePay>(receivePay, "اطلاعات  دریافت پرداخت با موفقیت ایجاد گردید .");
            }
            catch (Exception e)
            {
                throw;
            }
        }

    }
}