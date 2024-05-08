using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Sepehr.Application.Exceptions;
using Sepehr.Application.Interfaces.Repositories;
using Sepehr.Application.Wrappers;

namespace Sepehr.Application.Features.ProductSuppliers.Command.DeleteProductSupplierById
{
    public class DeleteProductSupplierByIdCommand : IRequest<Response<Guid>>
    {
        public Guid Id { get; set; }

        public class
        DeleteProductSupplierByIdCommandHandler
        : IRequestHandler<DeleteProductSupplierByIdCommand, Response<Guid>>
        {
            private readonly IProductSupplierRepositoryAsync _productSupplierRepository;

            public DeleteProductSupplierByIdCommandHandler(
                IProductSupplierRepositoryAsync productSupplierRepository
            )
            {
                _productSupplierRepository = productSupplierRepository;
            }

            public async Task<Response<Guid>>
            Handle(
                DeleteProductSupplierByIdCommand command,
                CancellationToken cancellationToken
            )
            {
                var productSupplier = await _productSupplierRepository.GetByIdAsync(command.Id);
                if (productSupplier == null)
                    throw new ApiException($"تامین کننده یافت نشد !");
                await _productSupplierRepository.DeleteAsync(productSupplier);
                return new Response<Guid>(productSupplier.Id);
            }
        }
    }
}
