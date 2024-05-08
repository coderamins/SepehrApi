using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Sepehr.Application.Exceptions;
using Sepehr.Application.Interfaces.Repositories;
using Sepehr.Application.Wrappers;
using Sepehr.Domain.Entities;

namespace Sepehr.Application.Features.Products.Queries.GetProductById
{
    public class GetProductByIdQuery : IRequest<Response<Product>>
    {
        public Guid Id { get; set; }

        public class GetProductByIdQueryHandler : IRequestHandler<GetProductByIdQuery, Response<Product>>
        {
            private readonly IProductRepositoryAsync _productRepository;

            public GetProductByIdQueryHandler(
                IProductRepositoryAsync productRepository
            )
            {
                _productRepository = productRepository;
            }

            public async Task<Response<Product>>
            Handle(
                GetProductByIdQuery query,
                CancellationToken cancellationToken
            )
            {
                var product = await _productRepository.GetByIdAsync(query.Id);
                if (product == null)
                    throw new ApiException($"محصول یافت نشد !");
                return new Response<Product>(product);
            }
        }
    }
}
