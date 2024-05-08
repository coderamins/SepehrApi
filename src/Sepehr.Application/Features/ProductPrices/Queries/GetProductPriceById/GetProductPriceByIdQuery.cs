using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Sepehr.Application.Exceptions;
using Sepehr.Application.Interfaces.Repositories;
using Sepehr.Application.Wrappers;
using Sepehr.Domain.Entities;

namespace Sepehr.Application.Features.ProductPrices.Queries.GetProductPriceById
{
    public class GetProductPriceByIdQuery : IRequest<Response<ProductPrice>>
    {
        public Guid Id { get; set; }

        public class GetProductPriceByIdQueryHandler : IRequestHandler<GetProductPriceByIdQuery, Response<ProductPrice>>
        {
            private readonly IProductPriceRepositoryAsync _productPriceRepository;

            public GetProductPriceByIdQueryHandler(
                IProductPriceRepositoryAsync productPriceRepository
            )
            {
                _productPriceRepository = productPriceRepository;
            }
                
            public async Task<Response<ProductPrice>>
            Handle(
                GetProductPriceByIdQuery query,
                CancellationToken cancellationToken
            )
            {
                var productPrice = await _productPriceRepository.GetByIdAsync(query.Id);
                if (productPrice == null)
                    throw new ApiException($"محصول یافت نشد !");
                return new Response<ProductPrice>(productPrice);
            }
        }
    }
}
