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

namespace Sepehr.Application.Features.LadingExitPermits.Command.DeleteLadingExitPermitById
{
    public class DeleteLadingExitPermitByIdCommand : IRequest<Response<bool>>
    {
        public Guid Id { get; set; }

        public class
        DeleteLadingExitPermitByIdCommandHandler
        : IRequestHandler<DeleteLadingExitPermitByIdCommand, Response<bool>>
        {
            private readonly ILadingExitPermitRepositoryAsync _ladingExitPermitRepository;
            ITableRecordRemovalRepositoryAsync _tableRecordRemoval;

            public DeleteLadingExitPermitByIdCommandHandler(
                ILadingExitPermitRepositoryAsync ladingExitPermitRepository,
                ITableRecordRemovalRepositoryAsync tableRecordRemoval
            )
            {
                _ladingExitPermitRepository = ladingExitPermitRepository;
                _tableRecordRemoval = tableRecordRemoval;
            }

            public async Task<Response<bool>>
            Handle(
                DeleteLadingExitPermitByIdCommand command,
                CancellationToken cancellationToken
            )
            {
                var ladingExitPermit = await _ladingExitPermitRepository.GetByIdAsync(command.Id);
                if (ladingExitPermit == null)
                    new ErrorMessageFactory().MakeError("مجوز خروج", ErrorType.NotFound);

                await _tableRecordRemoval.AddAsync(new TableRecordRemovalInfo { RemovedRecordId = ladingExitPermit.Id.ToString(), TableName = "ladingExitPermit" });

                await _ladingExitPermitRepository.DeleteAsync(ladingExitPermit);
                return new Response<bool>(true, new ErrorMessageFactory().MakeError("مجوز خروج", ErrorType.DeletedSuccess));
            }
        }
    }
}
