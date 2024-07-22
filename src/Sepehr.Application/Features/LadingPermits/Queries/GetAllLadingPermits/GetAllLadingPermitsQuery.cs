using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Sepehr.Application.Features.LadingPermits.Queries.GetAllLadingPermits;
using Sepehr.Application.Interfaces.Repositories;
using Sepehr.Application.Wrappers;
using Sepehr.Domain.Entities;
using Sepehr.Domain.ViewModels;

namespace Sepehr.Application.Features.LadingPermits.Queries.GetAllLadingPermits
{
    public class GetAllLadingPermitsQuery : IRequest<PagedResponse<IEnumerable<LadingPermitViewModel>>>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public bool? HasExitPermit { get; set; }


    }
    public class GetAllLadingPermitsQueryHandler :
         IRequestHandler<GetAllLadingPermitsQuery, PagedResponse<IEnumerable<LadingPermitViewModel>>>
    {
        private readonly ILadingPermitRepositoryAsync _ladingPermitRepository;
        private readonly IMapper _mapper;
        public GetAllLadingPermitsQueryHandler(ILadingPermitRepositoryAsync ladingPermitRepository, IMapper mapper)
        {
            _ladingPermitRepository = ladingPermitRepository;
            _mapper = mapper;
        }

        public async Task<PagedResponse<IEnumerable<LadingPermitViewModel>>> Handle(
            GetAllLadingPermitsQuery request, 
            CancellationToken cancellationToken)
        {
            try
            {
                var validFilter = _mapper.Map<GetAllLadingPermitsParameter>(request);
                var ladingPermits = await _ladingPermitRepository.GetAllLadingPermits(validFilter);
                
                var ladingPermitViewModel = _mapper.Map<IEnumerable<LadingPermitViewModel>>(
                            ladingPermits.Skip((validFilter.PageNumber - 1) * validFilter.PageSize)
                            .Take(validFilter.PageSize).ToList());

                return new PagedResponse<IEnumerable<LadingPermitViewModel>>(
                    ladingPermitViewModel,
                    validFilter.PageNumber,
                    validFilter.PageSize,
                    ladingPermits.Count()
                    );
            }
            catch (Exception e) 
            {

                throw;
            }
        }
    }
}