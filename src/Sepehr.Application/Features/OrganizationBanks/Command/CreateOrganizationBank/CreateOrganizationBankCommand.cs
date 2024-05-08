using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Sepehr.Application.Exceptions;
using Sepehr.Application.Interfaces.Repositories;
using Sepehr.Application.Wrappers;
using Sepehr.Domain.Common;
using Sepehr.Domain.Entities;

namespace Sepehr.Application.Features.OrganizationBanks.Command.CreateOrganizationBank
{
    public partial class CreateOrganizationBankCommand : IRequest<Response<OrganizationBank>>
    {
        public required int BankId { get; set; }
        public required string AccountNo { get; set; }
        public required string AccountOwner { get; set; }
        public string BranchName { get; set; } = string.Empty;
    }
    public class CreateOrganizationBankCommandHandler : IRequestHandler<CreateOrganizationBankCommand, Response<OrganizationBank>>
    {
        private readonly IOrganizationBankRepositoryAsync _organizationBankRepository;
        private readonly IMapper _mapper;
        public CreateOrganizationBankCommandHandler(IOrganizationBankRepositoryAsync organizationBankRepository, IMapper mapper)
        {
            _organizationBankRepository = organizationBankRepository;
            _mapper = mapper;
        }

        public async Task<Response<OrganizationBank>> Handle(CreateOrganizationBankCommand request, CancellationToken cancellationToken)
        {
            var checkDuplicate =await _organizationBankRepository.GetOrganizationBankInfo(request.AccountNo);
            if (checkDuplicate != null)
                throw new ApiException(new ErrorMessageFactory().MakeError("بانک", ErrorType.DuplicateForCreate));

            var organizationBank = _mapper.Map<OrganizationBank>(request);
            await _organizationBankRepository.AddAsync(organizationBank);

            return new Response<OrganizationBank>(organizationBank, new ErrorMessageFactory().MakeError("بانک", ErrorType.CreatedSuccess));
        }

    }
}