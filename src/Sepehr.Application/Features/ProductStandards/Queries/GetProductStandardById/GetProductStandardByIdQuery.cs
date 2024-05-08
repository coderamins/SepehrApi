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

namespace Sepehr.Application.Features.ProductStandards.Queries.GetProductStandardById
{
    public class GetProductStandardByIdQuery : IRequest<Response<ProductStandard>>
    {
        public Guid Id { get; set; }

        public class GetProductStandardByIdQueryHandler : IRequestHandler<GetProductStandardByIdQuery, Response<ProductStandard>>
        {
            private readonly IProductStandardRepositoryAsync _productStandardRepository;

            public GetProductStandardByIdQueryHandler(
                IProductStandardRepositoryAsync ProductStandardRepository
            )
            {
                _productStandardRepository = ProductStandardRepository;
            }

            public async Task<Response<ProductStandard>>
            Handle(
                GetProductStandardByIdQuery query,
                CancellationToken cancellationToken
            )
            {
                var ProductStandard = await _productStandardRepository.GetByIdAsync(query.Id);
                if (ProductStandard == null)
                    throw new ApiException(new ErrorMessageFactory().MakeError("استاندارد",ErrorType.NotFound));
                return new Response<ProductStandard>(ProductStandard);
            }
        }
    }
}
