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

namespace Sepehr.Application.Features.PersonnelPaymentRequests.Command.DeletePersonnelPaymentRequestById
{
    public class DeletePersonnelPaymentRequestByIdCommand : IRequest<Response<bool>>
    {
        public Guid Id { get; set; }

        public class
        DeletePersonnelPaymentRequestByIdCommandHandler
        : IRequestHandler<DeletePersonnelPaymentRequestByIdCommand, Response<bool>>
        {
            private readonly IPersonnelPaymentRequestRepositoryAsync _personnelPaymentRequestRepository;

            public DeletePersonnelPaymentRequestByIdCommandHandler(
                IPersonnelPaymentRequestRepositoryAsync personnelPaymentRequestRepository
                
            )
            {
                _personnelPaymentRequestRepository = personnelPaymentRequestRepository;
                
            }

            public async Task<Response<bool>>
            Handle(
                DeletePersonnelPaymentRequestByIdCommand command,
                CancellationToken cancellationToken
            )
            {
                var personnelPaymentRequest = await _personnelPaymentRequestRepository.GetByIdAsync(command.Id);
                if (personnelPaymentRequest == null)
                    throw new ApiException(new ErrorMessageFactory().MakeError("درخواست پرداخت", ErrorType.NotFound));

                await _personnelPaymentRequestRepository.DeleteAsync(personnelPaymentRequest);
                return new Response<bool>(true, new ErrorMessageFactory().MakeError("درخواست پرداخت", ErrorType.DeletedSuccess));
            }
        }
    }
}
