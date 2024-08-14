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
using Sepehr.Application.Interfaces.Repositories;
using Sepehr.Application.Wrappers;
using Sepehr.Domain.Common;
using Sepehr.Domain.Entities;
using Sepehr.Domain.Enums;

namespace Sepehr.Application.Features.PaymentRequests.Command.RejectPaymentRequest
{
    public class RejectPaymentRequestCommand : IRequest<Response<string>>
    {
        public Guid Id { get; set; }
        public string RejectReasonDesc { get; set; } = string.Empty;

        public class RejectPaymentRequestCommandHandler : IRequestHandler<RejectPaymentRequestCommand, Response<string>>
        {
            private readonly IMapper _mapper;
            private readonly IPaymentRequestRepositoryAsync _paymentRequestRepository;
            public RejectPaymentRequestCommandHandler(IPaymentRequestRepositoryAsync paymentRequestRepository, IMapper mapper)
            {
                _paymentRequestRepository = paymentRequestRepository;
                _mapper = mapper;
            }
            public async Task<Response<string>> Handle(RejectPaymentRequestCommand command, CancellationToken cancellationToken)
            {
                var paymentRequest = await _paymentRequestRepository.GetByIdAsync(command.Id);
                paymentRequest = _mapper.Map<RejectPaymentRequestCommand, PaymentRequest>(command, paymentRequest);

                if (paymentRequest == null)
                    throw new ApiException(new ErrorMessageFactory().MakeError("درخواست پرداخت", ErrorType.NotFound));
                if (!new int[] { 1, 2, 4 }.Contains(paymentRequest.PaymentRequestStatusId))
                    throw new ApiException("وضعیت درخواست نامعتبر می باشد !");
                else
                {
                    paymentRequest.PaymentRequestStatusId = (int)EPaymentRequestStatus.Rejected;
                    paymentRequest.RejectReasonDesc = command.RejectReasonDesc;
                    await _paymentRequestRepository.UpdateAsync(paymentRequest);
                    return new Response<string>(paymentRequest.Id.ToString(), new ErrorMessageFactory().MakeError("درخواست پرداخت", ErrorType.UpdatedSuccess));
                }
            }
        }
    }
}