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

namespace Sepehr.Application.Features.ShareHolders.Queries.GetShareHolderById
{
    public class GetShareHolderByIdQuery : IRequest<Response<ShareHolder>>
    {
        public Guid Id { get; set; }

        public class GetShareHolderByIdQueryHandler : IRequestHandler<GetShareHolderByIdQuery, Response<ShareHolder>>
        {
            private readonly IShareHolderRepositoryAsync _shareHolderRepository;

            public GetShareHolderByIdQueryHandler(
                IShareHolderRepositoryAsync shareHolderRepository
            )
            {
                _shareHolderRepository = shareHolderRepository;
            }

            public async Task<Response<ShareHolder>>
            Handle(
                GetShareHolderByIdQuery query,
                CancellationToken cancellationToken
            )
            {
                var shareHolder = await _shareHolderRepository.GetByIdAsync(query.Id);
                if (shareHolder == null)
                    throw new ApiException(new ErrorMessageFactory().MakeError("سهامدار",ErrorType.NotFound));
                return new Response<ShareHolder>(shareHolder);
            }
        }
    }
}
