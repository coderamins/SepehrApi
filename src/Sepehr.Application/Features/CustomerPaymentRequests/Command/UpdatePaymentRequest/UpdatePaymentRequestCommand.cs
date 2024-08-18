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
using Sepehr.Application.Interfaces;
using Sepehr.Application.Interfaces.Repositories;
using Sepehr.Application.Wrappers;
using Sepehr.Domain.Common;
using Sepehr.Domain.Entities;
using Sepehr.Domain.Enums;

namespace Sepehr.Application.Features.PaymentRequests.Command.UpdatePaymentRequest
{
    public class UpdatePaymentRequestCommand : PaymentRequestDto, IRequest<Response<PaymentRequest>>
    {
        public Guid Id { get; set; }
        public Guid customerId { get; set; }

        public class UpdatePaymentRequestCommandHandler : IRequestHandler<UpdatePaymentRequestCommand, Response<PaymentRequest>>
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
            public async Task<Response<PaymentRequest>> Handle(UpdatePaymentRequestCommand command, CancellationToken cancellationToken)
            {
                var paymentRequest = await _paymentRequestRepository.GetByIdAsync(command.Id);
                paymentRequest = _mapper.Map<UpdatePaymentRequestCommand, PaymentRequest>(command, paymentRequest);

                if (paymentRequest == null)
                    throw new ApiException(new ErrorMessageFactory().MakeError("درخواست پرداخت", ErrorType.NotFound));
                if (paymentRequest.PaymentRequestStatusId != (int)EPaymentRequestStatus.InProgress)
                    throw new ApiException("درخواست تایید شده و امکان ویرایش وجود ندارد !");
                else
                {
                    paymentRequest.ApproverId = Guid.Parse(_userService.UserId);
                    await _paymentRequestRepository.UpdatePaymentRequestAsync(paymentRequest);

                    return new Response<PaymentRequest>(paymentRequest,
                        new ErrorMessageFactory()
                        .MakeError("درخواست پرداخت", ErrorType.UpdatedSuccess));
                }
            }
        }
    }
}