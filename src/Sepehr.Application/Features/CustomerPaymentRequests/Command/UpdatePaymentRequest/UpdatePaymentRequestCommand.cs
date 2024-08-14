using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using AutoMapper;
using Azure.Core;
using MediatR;
using Sepehr.Application.Exceptions;
using Sepehr.Application.Interfaces;
using Sepehr.Application.Interfaces.Repositories;
using Sepehr.Application.Wrappers;
using Sepehr.Domain.Common;
using Sepehr.Domain.Entities;
using Sepehr.Domain.Enums;

namespace Sepehr.Application.Features.PaymentRequests.Command.UpdatePaymentRequest
{
    public class UpdatePaymentRequestCommand : IRequest<Response<string>>
    {
        public Guid Id { get; set; }
        public Guid CustomerId { get; set; }
        public EPaymentRequestType PaymentRequestTypeId { get; set; }
        public decimal Amount { get; set; }
        public int PaymentRequestReasonId { get; set; } 
        public required string BankAccountOrShabaNo { get; set; }
        public string AccountOwnerName { get; set; } = string.Empty;
        public int BankId { get; set; }
        public string ApplicatorName { get; set; } = string.Empty;
        public string PaymentRequestDescription { get; set; } = string.Empty;

        public class UpdatePaymentRequestCommandHandler : IRequestHandler<UpdatePaymentRequestCommand, Response<string>>
        {
            private readonly IMapper _mapper;
            private readonly IPaymentRequestRepositoryAsync _paymentRequestRepository;
            private readonly IAuthenticatedUserService _userService;
            public UpdatePaymentRequestCommandHandler(
                IPaymentRequestRepositoryAsync paymentRequestRepository,
                IAuthenticatedUserService userService,
                IMapper mapper)
            {
                _paymentRequestRepository = paymentRequestRepository;
                _mapper = mapper;
                _userService = userService; 
            }
            public async Task<Response<string>> Handle(UpdatePaymentRequestCommand command, CancellationToken cancellationToken)
            {
                var paymentRequests = _paymentRequestRepository.GetAllAsQueryable();

                var paymentRequest = await _paymentRequestRepository.GetByIdAsync(command.Id);
                paymentRequest = _mapper.Map<UpdatePaymentRequestCommand, PaymentRequest>(command, paymentRequest);

                if (paymentRequest == null)
                    throw new ApiException(new ErrorMessageFactory().MakeError("درخواست پرداخت", ErrorType.NotFound));
                else
                {
                    paymentRequest.ApproverId =Guid.Parse(_userService.UserId);
                    await _paymentRequestRepository.UpdateAsync(paymentRequest);
                    return new Response<string>(paymentRequest.Id.ToString(), new ErrorMessageFactory().MakeError("درخواست پرداخت", ErrorType.UpdatedSuccess));
                }
            }
        }
    }
}