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

namespace Sepehr.Application.Features.CustomerLabels.Command.DeleteCustomerLabelById
{
    public class DeleteCustomerLabelByIdCommand : IRequest<Response<bool>>
    {
        public int Id { get; set; }

        public class
        DeleteCustomerLabelByIdCommandHandler
        : IRequestHandler<DeleteCustomerLabelByIdCommand, Response<bool>>
        {
            private readonly ICustomerLabelRepositoryAsync _customerLabelRepository;
            

            public DeleteCustomerLabelByIdCommandHandler(
                ICustomerLabelRepositoryAsync customerLabelRepository
                
            )
            {
                _customerLabelRepository = customerLabelRepository;
                
            }

            public async Task<Response<bool>>
            Handle(
                DeleteCustomerLabelByIdCommand command,
                CancellationToken cancellationToken
            )
            {
                var customerLabel = await _customerLabelRepository.GetByIdAsync(command.Id);
                if (customerLabel == null)
                    throw new ApiException(new ErrorMessageFactory().MakeError("برچسب مشتری", ErrorType.NotFound));

                await _customerLabelRepository.DeleteAsync(customerLabel);
                return new Response<bool>(true, new ErrorMessageFactory().MakeError("برچسب مشتری", ErrorType.DeletedSuccess));
            }
        }
    }
}
