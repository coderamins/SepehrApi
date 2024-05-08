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

namespace Sepehr.Application.Features.Warehouses.Command.DeleteWarehouseById
{
    public class DeleteWarehouseByIdCommand : IRequest<Response<bool>>
    {
        public int Id { get; set; }

        public class
        DeleteWarehouseByIdCommandHandler
        : IRequestHandler<DeleteWarehouseByIdCommand, Response<bool>>
        {
            private readonly IWarehouseRepositoryAsync _WarehouseRepository;
            ITableRecordRemovalRepositoryAsync _tableRecordRemoval;

            public DeleteWarehouseByIdCommandHandler(
                IWarehouseRepositoryAsync WarehouseRepository,
                ITableRecordRemovalRepositoryAsync tableRecordRemoval
            )
            {
                _WarehouseRepository = WarehouseRepository;
                _tableRecordRemoval = tableRecordRemoval;
            }

            public async Task<Response<bool>>
            Handle(
                DeleteWarehouseByIdCommand command,
                CancellationToken cancellationToken
            )
            {
                var Warehouse = await _WarehouseRepository.GetByIdAsync(command.Id);
                if (Warehouse == null)
                    new ErrorMessageFactory().MakeError("انبار", ErrorType.NotFound);

                await _tableRecordRemoval.AddAsync(new TableRecordRemovalInfo { RemovedRecordId = Warehouse.Id.ToString(), TableName = "Warehouse" });

                await _WarehouseRepository.DeleteAsync(Warehouse);
                return new Response<bool>(true, new ErrorMessageFactory().MakeError("انبار", ErrorType.DeletedSuccess));
            }
        }
    }
}
