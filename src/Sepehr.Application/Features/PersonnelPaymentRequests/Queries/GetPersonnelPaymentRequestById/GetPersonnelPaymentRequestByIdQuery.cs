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

namespace Sepehr.Application.Features.PersonnelPaymentRequests.Queries.GetPersonnelPaymentRequestById
{
    public class GetPersonnelPaymentRequestByIdQuery : IRequest<Response<PersonnelPaymentRequestViewModel>>
    {
        public Guid Id { get; set; }

        public class GetPersonnelPaymentRequestByIdQueryHandler : IRequestHandler<GetPersonnelPaymentRequestByIdQuery, Response<PersonnelPaymentRequestViewModel>>
        {
            private readonly IPersonnelPaymentRequestRepositoryAsync _personnelPaymentRequestRepository;
            private readonly IMapper _mapper;
            public GetPersonnelPaymentRequestByIdQueryHandler(
                IPersonnelPaymentRequestRepositoryAsync personnelPaymentRequestRepository,
                IMapper mapper
            )
            {
                _personnelPaymentRequestRepository = personnelPaymentRequestRepository;
                _mapper = mapper;
            }

            public async Task<Response<PersonnelPaymentRequestViewModel>>
            Handle(
                GetPersonnelPaymentRequestByIdQuery query,
                CancellationToken cancellationToken
            )
            {
                var personnelPaymentRequest = await _personnelPaymentRequestRepository.GetPersonnelPaymentRequestInfo(query.Id);
                if (personnelPaymentRequest == null)
                    throw new ApiException(new ErrorMessageFactory().MakeError("درخواست پرداخت",ErrorType.NotFound));

                var mappedPayRequest=_mapper.Map<PersonnelPaymentRequestViewModel>(personnelPaymentRequest);

                return new Response<PersonnelPaymentRequestViewModel>(mappedPayRequest);
            }
        }
    }
}
