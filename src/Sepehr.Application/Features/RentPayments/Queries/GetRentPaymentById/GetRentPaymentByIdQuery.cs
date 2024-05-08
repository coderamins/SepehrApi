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

namespace Sepehr.Application.Features.RentPayments.Queries.GetRentPaymentById
{
    public class GetRentPaymentByIdQuery : IRequest<Response<RentPayment>>
    {
        public Guid Id { get; set; }

        public class GetRentPaymentByIdQueryHandler : IRequestHandler<GetRentPaymentByIdQuery, Response<RentPayment>>
        {
            private readonly IRentPaymentRepositoryAsync _rentPaymentRepository;

            public GetRentPaymentByIdQueryHandler(
                IRentPaymentRepositoryAsync rentPaymentRepository
            )
            {
                _rentPaymentRepository = rentPaymentRepository;
            }

            public async Task<Response<RentPayment>>
            Handle(
                GetRentPaymentByIdQuery query,
                CancellationToken cancellationToken
            )
            {
                var rentPayment = await _rentPaymentRepository.GetByIdAsync(query.Id);
                if (rentPayment == null)
                    throw new ApiException(new ErrorMessageFactory().MakeError("کرایه",ErrorType.NotFound));
                return new Response<RentPayment>(rentPayment);
            }
        }
    }
}
