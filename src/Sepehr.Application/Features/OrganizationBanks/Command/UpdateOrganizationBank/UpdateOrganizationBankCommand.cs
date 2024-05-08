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

namespace Sepehr.Application.Features.OrganizationBanks.Command.UpdateOrganizationBank
{
    public class UpdateOrganizationBankCommand : IRequest<Response<string>>
    {
        public int Id { get; set; }
        public required int BankId { get; set; }
        public required string AccountNo { get; set; }
        public required string AccountOwner { get; set; }
        public string BranchName { get; set; } = string.Empty;

        public class UpdateOrganizationBankCommandHandler : IRequestHandler<UpdateOrganizationBankCommand, Response<string>>
        {
            private readonly IMapper _mapper;
            private readonly IOrganizationBankRepositoryAsync _organizationBankRepository;
            public UpdateOrganizationBankCommandHandler(IOrganizationBankRepositoryAsync organizationBankRepository, IMapper mapper)
            {
                _organizationBankRepository = organizationBankRepository;
                _mapper = mapper;
            }
            public async Task<Response<string>> Handle(UpdateOrganizationBankCommand command, CancellationToken cancellationToken)
            {
                var organizationBank = await _organizationBankRepository.GetByIdAsync(command.Id);
                organizationBank = _mapper.Map(command, organizationBank);

                var organizationBanks = _organizationBankRepository.GetAllAsQueryable();
                if (organizationBank == null)
                    throw new ApiException(new ErrorMessageFactory().MakeError("بانک", ErrorType.NotFound));
                else if (organizationBanks.Any(b => b.AccountNo == command.AccountNo && b.Id != command.Id))
                    throw new ApiException("بانک با این مشخصات قبلا ایجاد شده است !");
                else
                {
                    await _organizationBankRepository.UpdateAsync(organizationBank);
                    return new Response<string>(organizationBank.Id.ToString(), new ErrorMessageFactory().MakeError("بانک", ErrorType.UpdatedSuccess));
                }
            }
        }
    }
}