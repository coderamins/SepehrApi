using AutoMapper;
using MediatR;
using Sepehr.Application.Exceptions;
using Sepehr.Application.Interfaces.Repositories;
using Sepehr.Application.Wrappers;
using Sepehr.Domain.Common;
using Sepehr.Domain.ViewModels;

namespace Sepehr.Application.Features.UnloadingPermits.Queries.GetUnloadingPermitByCode
{
    public class GetUnloadingPermitByCodeQuery : IRequest<Response<UnloadingPermitViewModel>>
    {
        public int unloadingPermitCode { get; set; }

        public class GetUnloadingPermitByCodeQueryHandler : IRequestHandler<GetUnloadingPermitByCodeQuery, Response<UnloadingPermitViewModel>>
        {
            private readonly IUnloadingPermitRepositoryAsync _unloadingPermitRepository;
            private readonly IMapper _mapper;

            public GetUnloadingPermitByCodeQueryHandler(
                IUnloadingPermitRepositoryAsync unloadingPermitRepository,
                IMapper mapper
            )
            {
                _unloadingPermitRepository = unloadingPermitRepository;
                _mapper = mapper;
            }

            public async Task<Response<UnloadingPermitViewModel>>
            Handle(
                GetUnloadingPermitByCodeQuery query,
                CancellationToken cancellationToken
            )
            {
                try
                {
                    var unloadingPermit = await _unloadingPermitRepository.GetUnloadingPermitInfo(query.unloadingPermitCode);
                    if (unloadingPermit == null)
                        throw new ApiException(new ErrorMessageFactory().MakeError("مجوز تخلیه", ErrorType.NotFound));

                    var result = _mapper.Map<UnloadingPermitViewModel>(unloadingPermit);
                    return new Response<UnloadingPermitViewModel>(result);
                }
                catch (Exception e)
                {

                    throw;
                }
            }
        }
    }
}
