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

namespace Sepehr.Application.Features.ShareHolders.Command.DeleteShareHolderById
{
    public class DeleteShareHolderByIdCommand : IRequest<Response<bool>>
    {
        public Guid Id { get; set; }

        public class
        DeleteShareHolderByIdCommandHandler
        : IRequestHandler<DeleteShareHolderByIdCommand, Response<bool>>
        {
            private readonly IShareHolderRepositoryAsync _shareHolderRepository;
            

            public DeleteShareHolderByIdCommandHandler(
                IShareHolderRepositoryAsync shareHolderRepository                
            )
            {
                _shareHolderRepository = shareHolderRepository;
                
            }

            public async Task<Response<bool>>
            Handle(
                DeleteShareHolderByIdCommand command,
                CancellationToken cancellationToken
            )
            {
                var shareHolder = await _shareHolderRepository.GetByIdAsync(command.Id);
                if (shareHolder == null)
                    new ErrorMessageFactory().MakeError("سهامدار", ErrorType.NotFound);

                await _shareHolderRepository.DeleteAsync(shareHolder);
                return new Response<bool>(true, new ErrorMessageFactory().MakeError("سهامدار", ErrorType.DeletedSuccess));
            }
        }
    }
}
