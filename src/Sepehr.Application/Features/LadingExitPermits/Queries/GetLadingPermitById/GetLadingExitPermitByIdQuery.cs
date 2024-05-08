using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Sepehr.Application.Exceptions;
using Sepehr.Application.Interfaces.Repositories;
using Sepehr.Application.Wrappers;
using Sepehr.Domain.Common;
using Sepehr.Domain.Entities;
using Sepehr.Domain.ViewModels;

namespace Sepehr.Application.Features.LadingExitPermits.Queries.GetLadingExitPermitById
{
    public class GetLadingExitPermitByIdQuery : IRequest<Response<LadingExitPermitViewModel>>
    {
        public Guid Id { get; set; }

        public class GetLadingExitPermitByIdQueryHandler : IRequestHandler<GetLadingExitPermitByIdQuery, Response<LadingExitPermitViewModel>>
        {
            private readonly ILadingExitPermitRepositoryAsync _ladingExitPermitRepository;
            private readonly IMapper _mapper;
            public GetLadingExitPermitByIdQueryHandler(
                ILadingExitPermitRepositoryAsync ladingExitPermitRepository,
                IMapper mapper
            )
            {
                _ladingExitPermitRepository = ladingExitPermitRepository;
                _mapper = mapper;   
            }

            public async Task<Response<LadingExitPermitViewModel>>
            Handle(
                GetLadingExitPermitByIdQuery query,
                CancellationToken cancellationToken
            )
            {
                var ladingExitPermit = await _ladingExitPermitRepository.GetLadingExitPermitInfo(query.Id);

                if (ladingExitPermit == null)
                    throw new ApiException(new ErrorMessageFactory().MakeError("مجوز خروج",ErrorType.NotFound));

                var ladingExitPermitViewModel = _mapper.Map<LadingExitPermitViewModel>(ladingExitPermit);

                return new Response<LadingExitPermitViewModel>(ladingExitPermitViewModel);
            }
        }
    }
}
