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

namespace Sepehr.Application.Features.RentPayments.Command.DeleteRentPaymentById
{
    public class DeleteRentPaymentByIdCommand : IRequest<Response<bool>>
    {
        public Guid Id { get; set; }

        public class
        DeleteRentPaymentByIdCommandHandler
        : IRequestHandler<DeleteRentPaymentByIdCommand, Response<bool>>
        {
            private readonly IRentPaymentRepositoryAsync _rentPaymentRepository;
            ITableRecordRemovalRepositoryAsync _tableRecordRemoval;

            public DeleteRentPaymentByIdCommandHandler(
                IRentPaymentRepositoryAsync rentPaymentRepository,
                ITableRecordRemovalRepositoryAsync tableRecordRemoval
            )
            {
                _rentPaymentRepository = rentPaymentRepository;
                _tableRecordRemoval = tableRecordRemoval;
            }

            public async Task<Response<bool>>
            Handle(
                DeleteRentPaymentByIdCommand command,
                CancellationToken cancellationToken
            )
            {
                var rentPayment = await _rentPaymentRepository.GetByIdAsync(command.Id);
                if (rentPayment == null)
                    new ErrorMessageFactory().MakeError("کرایه", ErrorType.NotFound);

                await _tableRecordRemoval.AddAsync(new TableRecordRemovalInfo { RemovedRecordId = rentPayment.Id.ToString(), TableName = "rentPayment" });

                await _rentPaymentRepository.DeleteAsync(rentPayment);
                return new Response<bool>(true, new ErrorMessageFactory().MakeError("کرایه", ErrorType.DeletedSuccess));
            }
        }
    }
}
