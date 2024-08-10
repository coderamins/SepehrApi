using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Sepehr.Application.Exceptions;
using Sepehr.Application.Interfaces.Repositories;
using Sepehr.Application.Wrappers;
using Sepehr.Domain.Common;

namespace Sepehr.Application.Features.UnloadingPermits.Command.DeleteUnloadingPermitById
{
    public class DeleteUnloadingPermitByIdCommand : IRequest<Response<bool>>
    {
        public Guid Id { get; set; }

        public class
        DeleteUnloadingPermitByIdCommandHandler
        : IRequestHandler<DeleteUnloadingPermitByIdCommand, Response<bool>>
        {
            private readonly IUnloadingPermitRepositoryAsync _unloadingPermitRepository;

            public DeleteUnloadingPermitByIdCommandHandler(
                IUnloadingPermitRepositoryAsync unloadingPermitRepository
            )
            {
                _unloadingPermitRepository = unloadingPermitRepository;
            }

            public async Task<Response<bool>>
            Handle(
                DeleteUnloadingPermitByIdCommand command,
                CancellationToken cancellationToken
            )
            {
                var unloadingPermit = await _unloadingPermitRepository.GetByIdAsync(command.Id);
                if (unloadingPermit == null)
                    new ErrorMessageFactory().MakeError("مجوز تخلیه", ErrorType.NotFound);
                
                await _unloadingPermitRepository.DeleteAsync(unloadingPermit);
                return new Response<bool>(true, new ErrorMessageFactory().MakeError("مجوز تخلیه", ErrorType.DeletedSuccess));
            }
        }
    }
}
