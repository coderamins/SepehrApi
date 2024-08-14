using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Sepehr.Application.Exceptions;
using Sepehr.Application.Interfaces.Repositories;
using Sepehr.Application.Wrappers;
using Sepehr.Domain.Common;
using Sepehr.Domain.Entities;
using Sepehr.Domain.ViewModels;

namespace Sepehr.Application.Features.PaymentRequests.Queries.GetPaymentRequestById
{
    public class GetPaymentRequestByIdQuery : IRequest<Response<PaymentRequestViewModel>>
    {
        public Guid Id { get; set; }

        public class GetPaymentRequestByIdQueryHandler : IRequestHandler<GetPaymentRequestByIdQuery, Response<PaymentRequestViewModel>>
        {
            private readonly IPaymentRequestRepositoryAsync _paymentRequestRepository;
            private readonly IMapper _mapper;
            public GetPaymentRequestByIdQueryHandler(
                IPaymentRequestRepositoryAsync paymentRequestRepository,
                IMapper mapper
            )
            {
                _paymentRequestRepository = paymentRequestRepository;
                _mapper = mapper;
            }

            public async Task<Response<PaymentRequestViewModel>>
            Handle(
                GetPaymentRequestByIdQuery query,
                CancellationToken cancellationToken
            )
            {
                var paymentRequest = await _paymentRequestRepository.GetPaymentRequestInfo(query.Id);
                if (paymentRequest == null)
                    throw new ApiException(new ErrorMessageFactory().MakeError("درخواست پرداخت",ErrorType.NotFound));

                var mappedPayRequest=_mapper.Map<PaymentRequestViewModel>(paymentRequest);

                return new Response<PaymentRequestViewModel>(mappedPayRequest);
            }
        }
    }
}
