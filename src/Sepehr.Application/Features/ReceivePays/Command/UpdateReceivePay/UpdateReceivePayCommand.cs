using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using Sepehr.Application.Exceptions;
using Sepehr.Application.Interfaces.Repositories;
using Sepehr.Application.Wrappers;
using Sepehr.Domain.Entities;

namespace Sepehr.Application.Features.ReceivePays.Command.UpdateReceivePay
{
    public class UpdateReceivePayCommand : IRequest<Response<ReceivePay>>
    {
        public Guid Id { get; set; }


        /// <summary>
        /// نوع دریافت از 
        /// </summary>
        public int? ReceivePaymentTypeFromId { get; set; }

        /// <summary>
        /// نوع پرداخت به
        /// </summary>
        public int? ReceivePaymentTypeToId { get; set; }

        public decimal Amount{ get; set; }
        //public required string ReceivedFrom { get; set; }
        //public required string PayTo { get; set; }
        public string? AccountOwner { get; set; }
        public string? TrachingCode { get; set; }
        public string? CompanyName { get; set; }
        public string? ContractCode { get; set; }
        //public int? AccountingDocNo { get; set; }
        public string AccountingDescription { get; set; } = string.Empty;

        #region اگر مقصد یکی یا هر دو طرف مشتری باشد
        public int? ReceiveFromCompanyId { get; set; }
        public int? PayToCompanyId { get; set; }
        #endregion

        public string? Description { get; set; }

        #region  دریافت از
        public required string ReceiveFromId { get; set; }
        #endregion

        #region پرداخت به
        public required string PayToId { get; set; }
        #endregion

        public IList<IFormFile>? Attachments { get; set; }

        public class UpdateReceivePayCommandHandler : IRequestHandler<UpdateReceivePayCommand, Response<ReceivePay>>
        {
            private readonly IMapper _mapper;
            private readonly IReceivePayRepositoryAsync _receivePayRepository;
            public UpdateReceivePayCommandHandler(IReceivePayRepositoryAsync receivePayRepository, IMapper mapper)
            {
                _receivePayRepository = receivePayRepository;
                _mapper = mapper;
            }
            public async Task<Response<ReceivePay>> Handle(UpdateReceivePayCommand command, CancellationToken cancellationToken)
            {
                try
                {
                    var receivePayRepository = await _receivePayRepository.GetByIdAsync(command.Id);
                    receivePayRepository = _mapper.Map(command, receivePayRepository);

                    if (receivePayRepository == null)
                        throw new ApiException($"دریافت/پرداخت یافت نشد !");
                    //else if(receivePayRepository)
                    else
                    {
                       var receivePay= await _receivePayRepository.UpdateReceivePayAsync(receivePayRepository);
                        return new Response<ReceivePay>(receivePay, "ویرایش با موفقیت انجام شد .");
                    }
                }
                catch (Exception e)
                {

                    throw;
                }
            }
        }
    }
}