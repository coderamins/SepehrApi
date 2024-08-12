using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Sepehr.Application.Exceptions;
using Sepehr.Application.Interfaces.Repositories;
using Sepehr.Application.Wrappers;
using Sepehr.Domain.Common;
using Sepehr.Domain.Entities;

namespace Sepehr.Application.Features.PaymentRequests.Queries.GetPaymentRequestById
{
    public class GetPaymentRequestByIdQuery : IRequest<Response<PaymentRequest>>
    {
        public Guid Id { get; set; }

        public class GetPaymentRequestByIdQueryHandler : IRequestHandler<GetPaymentRequestByIdQuery, Response<PaymentRequest>>
        {
            private readonly IPaymentRequestRepositoryAsync _paymentRequestRepository;

            public GetPaymentRequestByIdQueryHandler(
                IPaymentRequestRepositoryAsync paymentRequestRepository
            )
            {
                _paymentRequestRepository = paymentRequestRepository;
            }

            public async Task<Response<PaymentRequest>>
            Handle(
                GetPaymentRequestByIdQuery query,
                CancellationToken cancellationToken
            )
            {
                var paymentRequest = await _paymentRequestRepository.GetByIdAsync(query.Id);
                if (paymentRequest == null)
                    throw new ApiException(new ErrorMessageFactory().MakeError("درخواست پرداخت",ErrorType.NotFound));
                return new Response<PaymentRequest>(paymentRequest);
            }
        }
    }
}
