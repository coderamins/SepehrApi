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

namespace Sepehr.Application.Features.ProductTypes.Queries.GetProductTypeById
{
    public class GetProductTypeByIdQuery : IRequest<Response<ProductType>>
    {
        public int Id { get; set; }

        public class GetProductTypeByIdQueryHandler : IRequestHandler<GetProductTypeByIdQuery, Response<ProductType>>
        {
            private readonly IProductTypeRepositoryAsync _productTypeRepository;

            public GetProductTypeByIdQueryHandler(
                IProductTypeRepositoryAsync productTypeRepository
            )
            {
                _productTypeRepository = productTypeRepository;
            }

            public async Task<Response<ProductType>>
            Handle(
                GetProductTypeByIdQuery query,
                CancellationToken cancellationToken
            )
            {
                var productType = await _productTypeRepository.GetByIdAsync(query.Id);
                if (productType == null)
                    throw new ApiException(new ErrorMessageFactory().MakeError("نوع کالا",ErrorType.NotFound));
                return new Response<ProductType>(productType);
            }
        }
    }
}
