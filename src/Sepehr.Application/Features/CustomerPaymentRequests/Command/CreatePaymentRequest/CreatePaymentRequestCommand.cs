﻿using AutoMapper;
using MediatR;
using Sepehr.Application.DTOs;
using Sepehr.Application.Interfaces.Repositories;
using Sepehr.Application.Wrappers;
using Sepehr.Domain.Common;
using Sepehr.Domain.Entities;
using Sepehr.Domain.Enums;
using Serilog;
using System.Text.Json.Serialization;

namespace Sepehr.Application.Features.PaymentRequests.Command.CreatePaymentRequest
{
    public partial class CreatePaymentRequestCommand : PaymentRequestDto, IRequest<Response<PaymentRequest>>
    {
        public Guid customerId { get; set; }
    }
    public class CreatePaymentRequestCommandHandler : IRequestHandler<CreatePaymentRequestCommand, Response<PaymentRequest>>
    {
        private readonly IPaymentRequestRepositoryAsync _paymentRequestRepository;
        private readonly ILadingExitPermitRepositoryAsync _ladingExitPermit;
        private readonly IUnloadingPermitRepositoryAsync _puOrderUnloadPermitRepository;
        private readonly IMapper _mapper;
        public CreatePaymentRequestCommandHandler(
            IPaymentRequestRepositoryAsync paymentRequestRepository,
            IMapper mapper,
            ILadingExitPermitRepositoryAsync ladingExitPermit,
            IUnloadingPermitRepositoryAsync puOrderUnloadPermitRepository)
        {
            _paymentRequestRepository = paymentRequestRepository;
            _mapper = mapper;
            _ladingExitPermit = ladingExitPermit;
            _puOrderUnloadPermitRepository = puOrderUnloadPermitRepository;
        }

        public async Task<Response<PaymentRequest>> Handle(CreatePaymentRequestCommand command, CancellationToken cancellationToken)
        {
            try
            {
                var paymentRequest = _mapper.Map<PaymentRequest>(command);
                await _paymentRequestRepository.AddAsync(paymentRequest);

                return new Response<PaymentRequest>(paymentRequest, 
                    new ErrorMessageFactory()
                    .MakeError("درخواست پرداخت", ErrorType.CreatedSuccess));
            }
            catch (Exception e)
            {
                Log.Write(Serilog.Events.LogEventLevel.Error, e.Message ?? e.InnerException.Message);
                throw;
            }
        }

    }
}