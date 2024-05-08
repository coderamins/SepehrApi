using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Sepehr.Application.Exceptions;
using Sepehr.Application.Features.Products.Command.UpdateProduct;
using Sepehr.Application.Interfaces.Repositories;
using Sepehr.Application.Wrappers;
using Sepehr.Domain.Entities;

namespace Sepehr.Application.Features.Products.Command.DeleteProductById
{
    public class DeleteProductByIdCommand : IRequest<Response<bool>>
    {
        public Guid Id { get; set; }

        public class
        DeleteProductByIdCommandHandler
        : IRequestHandler<DeleteProductByIdCommand, Response<bool>>
        {
            private readonly IProductRepositoryAsync _productRepository;
            private readonly IMapper _mapper;

            public DeleteProductByIdCommandHandler(
                IProductRepositoryAsync productRepository,
                IMapper mapper
            )
            {
                _productRepository = productRepository;
                _mapper = mapper;
            }

            public async Task<Response<bool>>
            Handle(
                DeleteProductByIdCommand command,
                CancellationToken cancellationToken
            )
            {
                var product = await _productRepository.GetByIdAsync(command.Id);
                if (product == null)
                    throw new ApiException($"محصول یافت نشد !");

                product = _mapper.Map<DeleteProductByIdCommand, Product>(command, product);
                product.IsActive = false;
                await _productRepository.UpdateAsync(product);

                return new Response<bool>(true,"محصول با موفقیت غیر فعال شد .");
            }
        }
    }
}
