using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Sepehr.Application.Exceptions;
using Sepehr.Application.Interfaces.Repositories;
using Sepehr.Application.Wrappers;

namespace Sepehr.Application.Features.ProductPrices.Command.DeleteProductPriceById
{
    public class DeleteProductPriceByIdCommand : IRequest<Response<bool>>
    {
        public Guid Id { get; set; }

        public class
        DeleteProductPriceByIdCommandHandler
        : IRequestHandler<DeleteProductPriceByIdCommand, Response<bool>>
        {
            private readonly IProductPriceRepositoryAsync _productPriceRepository;

            public DeleteProductPriceByIdCommandHandler(
                IProductPriceRepositoryAsync productPriceRepository
            )
            {
                _productPriceRepository = productPriceRepository;
            }

            public async Task<Response<bool>>
            Handle(
                DeleteProductPriceByIdCommand command,
                CancellationToken cancellationToken
            )
            {
                var productPrice = await _productPriceRepository.GetByIdAsync(command.Id);
                if (productPrice == null)
                    throw new ApiException($"محصول یافت نشد !");
                 await _productPriceRepository.DeleteAsync(productPrice);
                return new Response<bool>(true,"محصول با موفقیت حذف شد .");
            }
        }
    }
}
