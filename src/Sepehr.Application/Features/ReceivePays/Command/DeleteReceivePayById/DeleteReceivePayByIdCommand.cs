using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Sepehr.Application.Exceptions;
using Sepehr.Application.Interfaces.Repositories;
using Sepehr.Application.Wrappers;

namespace Sepehr.Application.Features.ReceivePays.Command.DeleteReceivePayById
{
    public class DeleteReceivePayByIdCommand : IRequest<Response<Guid>>
    {
        public Guid Id { get; set; }

        public class
        DeleteReceivePayByIdCommandHandler
        : IRequestHandler<DeleteReceivePayByIdCommand, Response<Guid>>
        {
            private readonly IReceivePayRepositoryAsync _productSupplierRepository;

            public DeleteReceivePayByIdCommandHandler(
                IReceivePayRepositoryAsync productSupplierRepository
            )
            {
                _productSupplierRepository = productSupplierRepository;
            }

            public async Task<Response<Guid>>
            Handle(
                DeleteReceivePayByIdCommand command,
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
