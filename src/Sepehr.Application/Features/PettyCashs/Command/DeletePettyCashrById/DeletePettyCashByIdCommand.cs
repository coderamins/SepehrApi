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

namespace Sepehr.Application.Features.PettyCashs.Command.DeletePettyCashById
{
    public class DeletePettyCashByIdCommand : IRequest<Response<bool>>
    {
        public int Id { get; set; }

        public class
        DeletePettyCashByIdCommandHandler
        : IRequestHandler<DeletePettyCashByIdCommand, Response<bool>>
        {
            private readonly IPettyCashRepositoryAsync _PettyCashRepository;
            

            public DeletePettyCashByIdCommandHandler(
                IPettyCashRepositoryAsync PettyCashRepository                
            )
            {
                _PettyCashRepository = PettyCashRepository;                
            }

            public async Task<Response<bool>>
            Handle(
                DeletePettyCashByIdCommand command,
                CancellationToken cancellationToken
            )
            {
                var PettyCash = await _PettyCashRepository.GetByIdAsync(command.Id);
                if (PettyCash == null)
                    new ErrorMessageFactory().MakeError("تنخواه گردان", ErrorType.NotFound);

                await _PettyCashRepository.DeleteAsync(PettyCash);
                return new Response<bool>(true, new ErrorMessageFactory().MakeError("تنخواه گردان", ErrorType.DeletedSuccess));
            }
        }
    }
}
