using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using AutoMapper;
using Azure.Core;
using MediatR;
using Sepehr.Application.DTOs;
using Sepehr.Application.Exceptions;
using Sepehr.Application.Interfaces.Repositories;
using Sepehr.Application.Wrappers;
using Sepehr.Domain.Common;
using Sepehr.Domain.Entities;
using Sepehr.Domain.Enums;

namespace Sepehr.Application.Features.PaymentRequests.Command.ProceedPaymentRequest
{
    public class ProceedToPaymentRequestCommand : IRequest<Response<string>>
    {
        public Guid Id { get; set; }
        public required int PaymentOriginTypeId { get; set; }
        public required string PaymentOriginId { get; set; }

        public List<AttachmentDto>? Attachments { get; set; }

        public class ProceedToPaymentRequestCommandHandler : IRequestHandler<ProceedToPaymentRequestCommand, Response<string>>
        {
            private readonly IMapper _mapper;
            private readonly IPaymentRequestRepositoryAsync _paymentRequestRepository;
            public ProceedToPaymentRequestCommandHandler(IPaymentRequestRepositoryAsync paymentRequestRepository, IMapper mapper)
            {
                _paymentRequestRepository = paymentRequestRepository;
                _mapper = mapper;
            }
            public async Task<Response<string>> Handle(ProceedToPaymentRequestCommand command, CancellationToken cancellationToken)
            {
                var paymentRequest = await _paymentRequestRepository.GetByIdAsync(command.Id);
                if (paymentRequest == null)
                    throw new ApiException("درخواست پرداخت یافت نشد !");

                paymentRequest = _mapper.Map(command, paymentRequest);

                if (paymentRequest == null)
                    throw new ApiException(new ErrorMessageFactory().MakeError("درخواست پرداخت", ErrorType.NotFound));
                if (!new int[] { 1,2,4}.Contains(paymentRequest.PaymentRequestStatusId))
                    throw new ApiException("وضعیت درخواست نامعتبر می باشد !");
                else
                {
                    await _paymentRequestRepository.ProceedPaymentAsync(paymentRequest);
                    return new Response<string>(paymentRequest.Id.ToString(),"درخواست پرداخت با موفقیت انجام شد .(پرداخت شد)");
                }
            }
        }
    }
}