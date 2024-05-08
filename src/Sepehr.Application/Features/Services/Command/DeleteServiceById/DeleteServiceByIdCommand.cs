using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Sepehr.Application.Exceptions;
using Sepehr.Application.Interfaces.Repositories;
using Sepehr.Application.Wrappers;
using Sepehr.Domain.Common;

namespace Sepehr.Application.Features.Services.Command.DeleteServiceById
{
    public class DeleteServiceByIdCommand : IRequest<Response<bool>>
    {
        public int Id { get; set; }

        public class
        DeleteServiceByIdCommandHandler
        : IRequestHandler<DeleteServiceByIdCommand, Response<bool>>
        {
            private readonly IServiceRepositoryAsync _ServiceRepository;

            public DeleteServiceByIdCommandHandler(
                IServiceRepositoryAsync ServiceRepository
            )
            {
                _ServiceRepository = ServiceRepository;
            }

            public async Task<Response<bool>>
            Handle(
                DeleteServiceByIdCommand command,
                CancellationToken cancellationToken
            )
            {
                var Service = await _ServiceRepository.GetByIdAsync(command.Id);
                if (Service == null)
                    new ErrorMessageFactory().MakeError("خدمات سفارش", ErrorType.NotFound);
                
                await _ServiceRepository.DeleteAsync(Service);
                return new Response<bool>(true, new ErrorMessageFactory().MakeError("خدمات سفارش", ErrorType.DeletedSuccess));
            }
        }
    }
}
