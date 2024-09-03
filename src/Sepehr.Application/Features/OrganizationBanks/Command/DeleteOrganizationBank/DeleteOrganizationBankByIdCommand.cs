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
using Sepehr.Domain.LogEntities;

namespace Sepehr.Application.Features.OrganizationBanks.Command.DeleteOrganizationBankById
{
    public class DeleteOrganizationBankByIdCommand : IRequest<Response<bool>>
    {
        public int Id { get; set; }

        public class
        DeleteOrganizationBankByIdCommandHandler
        : IRequestHandler<DeleteOrganizationBankByIdCommand, Response<bool>>
        {
            private readonly IOrganizationBankRepositoryAsync _organizationBankRepository;
            

            public DeleteOrganizationBankByIdCommandHandler(
                IOrganizationBankRepositoryAsync organizationBankRepository
                
            )
            {
                _organizationBankRepository = organizationBankRepository;
                
            }

            public async Task<Response<bool>>
            Handle(
                DeleteOrganizationBankByIdCommand command,
                CancellationToken cancellationToken
            )
            {
                var organizationBank = await _organizationBankRepository.GetByIdAsync(command.Id);
                if (organizationBank == null)
                    throw new ApiException(new ErrorMessageFactory().MakeError("بانک", ErrorType.NotFound));

                await _organizationBankRepository.DeleteAsync(organizationBank);
                return new Response<bool>(true, new ErrorMessageFactory().MakeError("بانک", ErrorType.DeletedSuccess));
            }
        }
    }
}
