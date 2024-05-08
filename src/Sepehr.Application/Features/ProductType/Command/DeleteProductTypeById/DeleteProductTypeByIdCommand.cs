using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Sepehr.Application.Exceptions;
using Sepehr.Application.Interfaces.Repositories;
using Sepehr.Application.Wrappers;
using Sepehr.Domain.Common;

namespace Sepehr.Application.Features.ProductTypes.Command.DeleteProductTypeById
{
    public class DeleteProductTypeByIdCommand : IRequest<Response<bool>>
    {
        public int Id { get; set; }

        public class
        DeleteProductTypeByIdCommandHandler
        : IRequestHandler<DeleteProductTypeByIdCommand, Response<bool>>
        {
            private readonly IProductTypeRepositoryAsync _productTypeRepository;

            public DeleteProductTypeByIdCommandHandler(
                IProductTypeRepositoryAsync productTypeRepository
            )
            {
                _productTypeRepository = productTypeRepository;
            }

            public async Task<Response<bool>>
            Handle(
                DeleteProductTypeByIdCommand command,
                CancellationToken cancellationToken
            )
            {
                var productType = await _productTypeRepository.GetByIdAsync(command.Id);
                if (productType == null)
                    new ErrorMessageFactory().MakeError("نوع کالا", ErrorType.NotFound);
                
                await _productTypeRepository.DeleteAsync(productType);
                return new Response<bool>(true, new ErrorMessageFactory().MakeError("نوع کالا", ErrorType.DeletedSuccess));
            }
        }
    }
}
