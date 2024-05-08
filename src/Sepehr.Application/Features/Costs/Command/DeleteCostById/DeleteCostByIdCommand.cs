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

namespace Sepehr.Application.Features.Costs.Command.DeleteCostById
{
    public class DeleteCostByIdCommand : IRequest<Response<bool>>
    {
        public int Id { get; set; }

        public class DeleteCostByIdCommandHandler
        : IRequestHandler<DeleteCostByIdCommand, Response<bool>>
        {
            private readonly ICostRepositoryAsync _costRepository;
            private readonly ITableRecordRemovalRepositoryAsync _tableRecordRemoval;

            public DeleteCostByIdCommandHandler(
                ICostRepositoryAsync costRepository,
                ITableRecordRemovalRepositoryAsync tableRecordRemoval
            )
            {
                _costRepository = costRepository;
                _tableRecordRemoval = tableRecordRemoval;
            }

            public async Task<Response<bool>>
            Handle(
                DeleteCostByIdCommand command,
                CancellationToken cancellationToken
            )
            {
                var cost = await _costRepository.GetByIdAsync(command.Id);
                if (cost == null)
                    throw new ApiException(new ErrorMessageFactory().MakeError("هزینه", ErrorType.NotFound));

                await _tableRecordRemoval.AddAsync(new TableRecordRemovalInfo
                {
                    RemovedRecordId = cost.Id.ToString(),
                    TableName = "order"
                });
                await _costRepository.DeleteAsync(cost);
                return new Response<bool>(true, new ErrorMessageFactory().MakeError("هزینه", ErrorType.DeletedSuccess));
            }
        }
    }
}
