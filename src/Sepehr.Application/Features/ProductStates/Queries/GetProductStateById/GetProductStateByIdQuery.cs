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

namespace Sepehr.Application.Features.ProductStates.Queries.GetProductStateById
{
    public class GetProductStateByIdQuery : IRequest<Response<ProductState>>
    {
        public Guid Id { get; set; }

        public class GetProductStateByIdQueryHandler : IRequestHandler<GetProductStateByIdQuery, Response<ProductState>>
        {
            private readonly IProductStateRepositoryAsync _productStateRepository;

            public GetProductStateByIdQueryHandler(
                IProductStateRepositoryAsync productStateRepository
            )
            {
                _productStateRepository = productStateRepository;
            }

            public async Task<Response<ProductState>>
            Handle(
                GetProductStateByIdQuery query,
                CancellationToken cancellationToken
            )
            {
                var productState = await _productStateRepository.GetByIdAsync(query.Id);
                if (productState == null)
                    throw new ApiException(new ErrorMessageFactory().MakeError("برند",ErrorType.NotFound));
                return new Response<ProductState>(productState);
            }
        }
    }
}
