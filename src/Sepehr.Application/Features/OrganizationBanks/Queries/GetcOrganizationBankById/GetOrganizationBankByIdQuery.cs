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

namespace Sepehr.Application.Features.OrganizationBanks.Queries.GetOrganizationBankById
{
    public class GetOrganizationBankByIdQuery : IRequest<Response<OrganizationBank>>
    {
        public int Id { get; set; }

        public class GetOrganizationBankByIdQueryHandler : IRequestHandler<GetOrganizationBankByIdQuery, Response<OrganizationBank>>
        {
            private readonly IOrganizationBankRepositoryAsync _organizationBankRepository;

            public GetOrganizationBankByIdQueryHandler(
                IOrganizationBankRepositoryAsync organizationBankRepository
            )
            {
                _organizationBankRepository = organizationBankRepository;
            }

            public async Task<Response<OrganizationBank>>
            Handle(
                GetOrganizationBankByIdQuery query,
                CancellationToken cancellationToken
            )
            {
                var organizationBank = await _organizationBankRepository.GetOrganizationBankInfo(query.Id);
                if (organizationBank == null)
                    throw new ApiException(new ErrorMessageFactory().MakeError("بانک",ErrorType.NotFound));
                return new Response<OrganizationBank>(organizationBank);
            }
        }
    }
}
