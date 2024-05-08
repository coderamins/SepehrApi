using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Sepehr.Application.Exceptions;
using Sepehr.Application.Interfaces.Repositories;
using Sepehr.Application.Wrappers;
using Sepehr.Domain.Entities;
using Sepehr.Domain.LogEntities;

namespace Sepehr.Application.Features.CargoAnnouncements.Command.DeleteCargoAnnouncementById
{
    public class DeleteCargoAnncByIdCommand : IRequest<Response<bool>>
    {
        public Guid Id { get; set; }

        public class
        DeleteCargoAnncByIdCommandHandler
        : IRequestHandler<DeleteCargoAnncByIdCommand, Response<bool>>
        {
            private readonly ICargoAnnouncementRepositoryAsync _cargoAnncRepository;
            private readonly ITableRecordRemovalRepositoryAsync _tableRecordRemoval;


            public DeleteCargoAnncByIdCommandHandler(
                ICargoAnnouncementRepositoryAsync CargoAnncRepository,
                ITableRecordRemovalRepositoryAsync tableRecordRemoval
            )
            {
                _cargoAnncRepository = CargoAnncRepository;
                _tableRecordRemoval = tableRecordRemoval;
            }

            public async Task<Response<bool>>
            Handle(
                DeleteCargoAnncByIdCommand command,
                CancellationToken cancellationToken
            )
            {
                var cargoAnnc = await _cargoAnncRepository.GetByIdAsync(command.Id);
                if (cargoAnnc == null)
                    throw new ApiException($"اعلام بار یافت نشد !");

                await _tableRecordRemoval.AddAsync(new TableRecordRemovalInfo
                {
                    RemovedRecordId = cargoAnnc.Id.ToString(),
                    TableName = "order"
                });

                await _cargoAnncRepository.DeleteAsync(cargoAnnc);
                return new Response<bool>("اعلام بار با موفقیت حذف شد .");
            }
        }
    }
}
