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

namespace Sepehr.Application.Features.PettyCashs.Queries.GetPettyCashById
{
    public class GetPettyCashByIdQuery : IRequest<Response<PettyCash>>
    {
        public int Id { get; set; }

        public class GetPettyCashByIdQueryHandler : IRequestHandler<GetPettyCashByIdQuery, Response<PettyCash>>
        {
            private readonly IPettyCashRepositoryAsync _PettyCashRepository;

            public GetPettyCashByIdQueryHandler(
                IPettyCashRepositoryAsync PettyCashRepository
            )
            {
                _PettyCashRepository = PettyCashRepository;
            }

            public async Task<Response<PettyCash>>
            Handle(
                GetPettyCashByIdQuery query,
                CancellationToken cancellationToken
            )
            {
                var PettyCash = await _PettyCashRepository.GetByIdAsync(query.Id);
                if (PettyCash == null)
                    throw new ApiException(new ErrorMessageFactory().MakeError("تنخواه گردان",ErrorType.NotFound));
                return new Response<PettyCash>(PettyCash);
            }
        }
    }
}
