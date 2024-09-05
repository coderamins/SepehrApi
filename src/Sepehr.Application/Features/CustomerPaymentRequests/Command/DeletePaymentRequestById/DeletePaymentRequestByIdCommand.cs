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

            public DeletePaymentRequestByIdCommandHandler(
                IPaymentRequestRepositoryAsync paymentRequestRepository
                
            )
            {
                _paymentRequestRepository = paymentRequestRepository;                
            }

            public async Task<Response<bool>>
            Handle(
                DeletePaymentRequestByIdCommand command,
                CancellationToken cancellationToken
            )
            {
                var paymentRequest = await _paymentRequestRepository.GetByIdAsync(command.Id);
                if (paymentRequest == null)
                    throw new ApiException(new ErrorMessageFactory().MakeError("درخواست پرداخت", ErrorType.NotFound));

                await _paymentRequestRepository.DeleteAsync(paymentRequest);
                return new Response<bool>(true, new ErrorMessageFactory().MakeError("درخواست پرداخت", ErrorType.DeletedSuccess));
            }
        }
    }
}
