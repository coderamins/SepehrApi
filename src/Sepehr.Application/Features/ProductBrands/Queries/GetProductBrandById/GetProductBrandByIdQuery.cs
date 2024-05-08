using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Sepehr.Application.Exceptions;
using Sepehr.Application.Interfaces.Repositories;
using Sepehr.Application.Wrappers;
using Sepehr.Domain.Common;
using Sepehr.Domain.Entities;

namespace Sepehr.Application.Features.ProductBrands.Queries.GetProductBrandById
{
    public class GetProductBrandByIdQuery : IRequest<Response<ProductBrand>>
    {
        public Guid Id { get; set; }

        public class GetProductBrandByIdQueryHandler : IRequestHandler<GetProductBrandByIdQuery, Response<ProductBrand>>
        {
            private readonly IProductBrandRepositoryAsync _productBrandRepository;

            public GetProductBrandByIdQueryHandler(
                IProductBrandRepositoryAsync productBrandRepository
            )
            {
                _productBrandRepository = productBrandRepository;
            }

            public async Task<Response<ProductBrand>>
            Handle(
                GetProductBrandByIdQuery query,
                CancellationToken cancellationToken
            )
            {
                var productBrand = await _productBrandRepository.GetByIdAsync(query.Id);
                if (productBrand == null)
                    throw new ApiException(new ErrorMessageFactory().MakeError("برند",ErrorType.NotFound));
                return new Response<ProductBrand>(productBrand);
            }
        }
    }
}
