using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Sepehr.Application.Exceptions;
using Sepehr.Application.Interfaces.Repositories;
using Sepehr.Application.Wrappers;
using Sepehr.Domain.Common;

namespace Sepehr.Application.Features.ProductStates.Command.DeleteProductStateById
{
    public class DeleteProductStateByIdCommand : IRequest<Response<bool>>
    {
        public int Id { get; set; }

        public class
        DeleteProductStateByIdCommandHandler
        : IRequestHandler<DeleteProductStateByIdCommand, Response<bool>>
        {
            private readonly IProductStateRepositoryAsync _productStateRepository;

            public DeleteProductStateByIdCommandHandler(
                IProductStateRepositoryAsync productStateRepository
            )
            {
                _productStateRepository = productStateRepository;
            }

            public async Task<Response<bool>>
            Handle(
                DeleteProductStateByIdCommand command,
                CancellationToken cancellationToken
            )
            {
                var productState = await _productStateRepository.GetByIdAsync(command.Id);
                if (productState == null)
                    new ErrorMessageFactory().MakeError("برند", ErrorType.NotFound);
                
                await _productStateRepository.DeleteAsync(productState);
                return new Response<bool>(true, new ErrorMessageFactory().MakeError("برند", ErrorType.DeletedSuccess));
            }
        }
    }
}
