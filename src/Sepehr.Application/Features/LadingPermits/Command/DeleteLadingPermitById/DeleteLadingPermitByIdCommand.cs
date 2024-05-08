using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Sepehr.Application.Exceptions;
using Sepehr.Application.Interfaces.Repositories;
using Sepehr.Application.Wrappers;
using Sepehr.Domain.Common;

namespace Sepehr.Application.Features.LadingPermits.Command.DeleteLadingPermitById
{
    public class DeleteLadingPermitByIdCommand : IRequest<Response<bool>>
    {
        public int Id { get; set; }

        public class
        DeleteLadingPermitByIdCommandHandler
        : IRequestHandler<DeleteLadingPermitByIdCommand, Response<bool>>
        {
            private readonly ILadingPermitRepositoryAsync _ladingPermitRepository;

            public DeleteLadingPermitByIdCommandHandler(
                ILadingPermitRepositoryAsync ladingPermitRepository
            )
            {
                _ladingPermitRepository = ladingPermitRepository;
            }

            public async Task<Response<bool>>
            Handle(
                DeleteLadingPermitByIdCommand command,
                CancellationToken cancellationToken
            )
            {
                var ladingPermit = await _ladingPermitRepository.GetByIdAsync(command.Id);
                if (ladingPermit == null)
                    new ErrorMessageFactory().MakeError("مجوز بارگیری", ErrorType.NotFound);
                
                await _ladingPermitRepository.DeleteAsync(ladingPermit);
                return new Response<bool>(true, new ErrorMessageFactory().MakeError("مجوز بارگیری", ErrorType.DeletedSuccess));
            }
        }
    }
}
