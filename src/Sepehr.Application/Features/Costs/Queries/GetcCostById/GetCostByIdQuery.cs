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

namespace Sepehr.Application.Features.Costs.Queries.GetCostById
{
    public class GetCostByIdQuery : IRequest<Response<Cost>>
    {
        public Guid Id { get; set; }

        public class GetCostByIdQueryHandler : IRequestHandler<GetCostByIdQuery, Response<Cost>>
        {
            private readonly ICostRepositoryAsync _costRepository;

            public GetCostByIdQueryHandler(
                ICostRepositoryAsync costRepository
            )
            {
                _costRepository = costRepository;
            }

            public async Task<Response<Cost>>
            Handle(
                GetCostByIdQuery query,
                CancellationToken cancellationToken
            )
            {
                var cost = await _costRepository.GetByIdAsync(query.Id);
                if (cost == null)
                    throw new ApiException(new ErrorMessageFactory().MakeError("هزینه",ErrorType.NotFound));
                return new Response<Cost>(cost);
            }
        }
    }
}
