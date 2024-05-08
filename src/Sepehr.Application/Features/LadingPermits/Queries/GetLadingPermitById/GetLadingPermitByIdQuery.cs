using AutoMapper;
using MediatR;
using Sepehr.Application.Exceptions;
using Sepehr.Application.Interfaces.Repositories;
using Sepehr.Application.Wrappers;
using Sepehr.Domain.Common;
using Sepehr.Domain.ViewModels;

namespace Sepehr.Application.Features.LadingPermits.Queries.GetLadingPermitById
{
    public class GetLadingPermitByIdQuery : IRequest<Response<LadingPermitViewModel>>
    {
        public int Id { get; set; }

        public class GetLadingPermitByIdQueryHandler : IRequestHandler<GetLadingPermitByIdQuery, Response<LadingPermitViewModel>>
        {
            private readonly ILadingPermitRepositoryAsync _ladingPermitRepository;
            private readonly IMapper _mapper;

            public GetLadingPermitByIdQueryHandler(
                ILadingPermitRepositoryAsync ladingPermitRepository,
                IMapper mapper
            )
            {
                _ladingPermitRepository = ladingPermitRepository;
                _mapper = mapper;
            }

            public async Task<Response<LadingPermitViewModel>>
            Handle(
                GetLadingPermitByIdQuery query,
                CancellationToken cancellationToken
            )
            {
                try
                {
                    var ladingPermit = await _ladingPermitRepository.GetLadingPermitInfo(query.Id);
                    if (ladingPermit == null)
                        throw new ApiException(new ErrorMessageFactory().MakeError("مجوز بارگیری", ErrorType.NotFound));

                    var result = _mapper.Map<LadingPermitViewModel>(ladingPermit);
                    return new Response<LadingPermitViewModel>(result);
                }
                catch (Exception e)
                {

                    throw;
                }
            }
        }
    }
}
