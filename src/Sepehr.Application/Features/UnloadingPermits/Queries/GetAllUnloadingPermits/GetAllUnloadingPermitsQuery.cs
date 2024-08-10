using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Sepehr.Application.Features.UnloadingPermits.Queries.GetAllUnloadingPermits;
using Sepehr.Application.Interfaces.Repositories;
using Sepehr.Application.Wrappers;
using Sepehr.Domain.Entities;
using Sepehr.Domain.ViewModels;

namespace Sepehr.Application.Features.UnloadingPermits.Queries.GetAllUnloadingPermits
{
    public class GetAllUnloadingPermitsQuery : IRequest<PagedResponse<IEnumerable<UnloadingPermitViewModel>>>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public int? UnloadingPermitCode { get; set; }

    }
    public class GetAllUnloadingPermitsQueryHandler :
         IRequestHandler<GetAllUnloadingPermitsQuery, PagedResponse<IEnumerable<UnloadingPermitViewModel>>>
    {
        private readonly IUnloadingPermitRepositoryAsync _unloadingPermitRepository;
        private readonly IMapper _mapper;
        public GetAllUnloadingPermitsQueryHandler(IUnloadingPermitRepositoryAsync unloadingPermitRepository, IMapper mapper)
        {
            _unloadingPermitRepository = unloadingPermitRepository;
            _mapper = mapper;
        }

        public async Task<PagedResponse<IEnumerable<UnloadingPermitViewModel>>> Handle(
            GetAllUnloadingPermitsQuery request, 
            CancellationToken cancellationToken)
        {
            try
            {
                var validFilter = _mapper.Map<GetAllUnloadingPermitsParameter>(request);
                var unloadingPermits = await _unloadingPermitRepository.GetAllUnloadingPermits(validFilter);
                
                var unloadingPermitViewModel = _mapper.Map<IEnumerable<UnloadingPermitViewModel>>(
                            unloadingPermits.Skip((validFilter.PageNumber - 1) * validFilter.PageSize)
                            .Take(validFilter.PageSize).ToList());

                return new PagedResponse<IEnumerable<UnloadingPermitViewModel>>(
                    unloadingPermitViewModel,
                    validFilter.PageNumber,
                    validFilter.PageSize,
                    unloadingPermits.Count()
                    );
            }
            catch (Exception e) 
            {

                throw;
            }
        }
    }
}