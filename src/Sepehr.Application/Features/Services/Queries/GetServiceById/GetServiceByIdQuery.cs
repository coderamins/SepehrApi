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

namespace Sepehr.Application.Features.Services.Queries.GetServiceById
{
    public class GetServiceByIdQuery : IRequest<Response<Service>>
    {
        public Guid Id { get; set; }

        public class GetServiceByIdQueryHandler : IRequestHandler<GetServiceByIdQuery, Response<Service>>
        {
            private readonly IServiceRepositoryAsync _ServiceRepository;

            public GetServiceByIdQueryHandler(
                IServiceRepositoryAsync ServiceRepository
            )
            {
                _ServiceRepository = ServiceRepository;
            }

            public async Task<Response<Service>>
            Handle(
                GetServiceByIdQuery query,
                CancellationToken cancellationToken
            )
            {
                var Service = await _ServiceRepository.GetByIdAsync(query.Id);
                if (Service == null)
                    throw new ApiException(new ErrorMessageFactory().MakeError("خدمات سفارش",ErrorType.NotFound));
                return new Response<Service>(Service);
            }
        }
    }
}
