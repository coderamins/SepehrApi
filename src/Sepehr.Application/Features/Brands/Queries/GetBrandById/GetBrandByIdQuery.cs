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

namespace Sepehr.Application.Features.Brands.Queries.GetBrandById
{
    public class GetBrandByIdQuery : IRequest<Response<Brand>>
    {
        public Guid Id { get; set; }

        public class GetBrandByIdQueryHandler : IRequestHandler<GetBrandByIdQuery, Response<Brand>>
        {
            private readonly IBrandRepositoryAsync _brandRepository;

            public GetBrandByIdQueryHandler(
                IBrandRepositoryAsync brandRepository
            )
            {
                _brandRepository = brandRepository;
            }

            public async Task<Response<Brand>>
            Handle(
                GetBrandByIdQuery query,
                CancellationToken cancellationToken
            )
            {
                var brand = await _brandRepository.GetByIdAsync(query.Id);
                if (brand == null)
                    throw new ApiException(new ErrorMessageFactory().MakeError("برند",ErrorType.NotFound));
                return new Response<Brand>(brand);
            }
        }
    }
}
