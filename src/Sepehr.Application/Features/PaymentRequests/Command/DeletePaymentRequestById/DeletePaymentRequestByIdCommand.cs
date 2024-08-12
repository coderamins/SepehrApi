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

namespace Sepehr.Application.Features.PaymentRequests.Command.DeletePaymentRequestById
{
    public class DeletePaymentRequestByIdCommand : IRequest<Response<bool>>
    {
        public Guid Id { get; set; }

        public class
        DeletePaymentRequestByIdCommandHandler
        : IRequestHandler<DeletePaymentRequestByIdCommand, Response<bool>>
        {
            private readonly IPaymentRequestRepositoryAsync _paymentRequestRepository;
            ITableRecordRemovalRepositoryAsync _tableRecordRemoval;

            public DeletePaymentRequestByIdCommandHandler(
                IPaymentRequestRepositoryAsync paymentRequestRepository,
                ITableRecordRemovalRepositoryAsync tableRecordRemoval
            )
            {
                _paymentRequestRepository = paymentRequestRepository;
                _tableRecordRemoval = tableRecordRemoval;
            }

            public async Task<Response<bool>>
            Handle(
                DeletePaymentRequestByIdCommand command,
                CancellationToken cancellationToken
            )
            {
                var paymentRequest = await _paymentRequestRepository.GetByIdAsync(command.Id);
                if (paymentRequest == null)
                    new ErrorMessageFactory().MakeError("درخواست پرداخت", ErrorType.NotFound);

                await _tableRecordRemoval.AddAsync(new TableRecordRemovalInfo { RemovedRecordId = paymentRequest.Id.ToString(), TableName = "paymentRequest" });

                await _paymentRequestRepository.DeleteAsync(paymentRequest);
                return new Response<bool>(true, new ErrorMessageFactory().MakeError("درخواست پرداخت", ErrorType.DeletedSuccess));
            }
        }
    }
}
