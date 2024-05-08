using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Sepehr.Application.Exceptions;
using Sepehr.Application.Interfaces.Repositories;
using Sepehr.Application.Wrappers;
using Sepehr.Domain.Common;

namespace Sepehr.Application.Features.ProductStandards.Command.DeleteProductStandardById
{
    public class DeleteProductStandardByIdCommand : IRequest<Response<bool>>
    {
        public int Id { get; set; }

        public class
        DeleteProductStandardByIdCommandHandler
        : IRequestHandler<DeleteProductStandardByIdCommand, Response<bool>>
        {
            private readonly IProductStandardRepositoryAsync _productStandardRepository;

            public DeleteProductStandardByIdCommandHandler(
                IProductStandardRepositoryAsync ProductStandardRepository
            )
            {
                _productStandardRepository = ProductStandardRepository;
            }

            public async Task<Response<bool>>
            Handle(
                DeleteProductStandardByIdCommand command,
                CancellationToken cancellationToken
            )
            {
                var ProductStandard = await _productStandardRepository.GetByIdAsync(command.Id);
                if (ProductStandard == null)
                    new ErrorMessageFactory().MakeError("استاندارد", ErrorType.NotFound);
                
                await _productStandardRepository.DeleteAsync(ProductStandard);
                return new Response<bool>(true, new ErrorMessageFactory().MakeError("استاندارد", ErrorType.DeletedSuccess));
            }
        }
    }
}
