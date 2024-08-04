using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Sepehr.Application.Exceptions;
using Sepehr.Application.Interfaces.Repositories;
using Sepehr.Application.Wrappers;
using Sepehr.Domain.LogEntities;

namespace Sepehr.Application.Features.EntrancePermits.Command.DeleteEntrancePermitById
{
    public class DeleteEntrancePermitByIdCommand : IRequest<Response<bool>>
    {
        public Guid Id { get; set; }

        public class
        DeleteEntrancePermitByIdCommandHandler
        : IRequestHandler<DeleteEntrancePermitByIdCommand, Response<bool>>
        {
            private readonly IEntrancePermitRepositoryAsync _EntrancePermitRepository;
            private readonly ITableRecordRemovalRepositoryAsync _tableRecordRemoval;

            public DeleteEntrancePermitByIdCommandHandler(
                IEntrancePermitRepositoryAsync EntrancePermitRepository,
                ITableRecordRemovalRepositoryAsync tableRecordRemoval
            )
            {
                _EntrancePermitRepository = EntrancePermitRepository;
                _tableRecordRemoval = tableRecordRemoval;
            }

            public async Task<Response<bool>>
            Handle(
                DeleteEntrancePermitByIdCommand command,
                CancellationToken cancellationToken
            )
            {
                await _EntrancePermitRepository.DeleteEntrancePermit(command.Id);

                return new Response<bool>(true,"مجوز ورود با موفقیت حذف شد .");
            }
        }
    }
}
