using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Sepehr.Application.Features.LadingExitPermits.Queries.GetAllLadingExitPermits;
using Sepehr.Application.Interfaces.Repositories;
using Sepehr.Application.Wrappers;
using Sepehr.Domain.Entities;
using Sepehr.Domain.ViewModels;

namespace Sepehr.Application.Features.LadingExitPermits.Queries.GetAllLadingExitPermits
{
    public class GetAllLadingExitPermitsQuery : IRequest<PagedResponse<IEnumerable<LadingExitPermitViewModel>>>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public int? LadingPermitId { get; set; }
    }
    public class GetAllLadingExitPermitsQueryHandler :
         IRequestHandler<GetAllLadingExitPermitsQuery, PagedResponse<IEnumerable<LadingExitPermitViewModel>>>
    {
        private readonly ILadingExitPermitRepositoryAsync _ladingExitPermitRepository;
        private readonly IMapper _mapper;
        public GetAllLadingExitPermitsQueryHandler(ILadingExitPermitRepositoryAsync ladingExitPermitRepository, IMapper mapper)
        {
            _ladingExitPermitRepository = ladingExitPermitRepository;
            _mapper = mapper;
        }

        public async Task<PagedResponse<IEnumerable<LadingExitPermitViewModel>>> Handle(
            GetAllLadingExitPermitsQuery request, 
            CancellationToken cancellationToken)
        {
            try
            {
                var validFilter = _mapper.Map<GetAllLadingExitPermitsParameter>(request);
                var ladingExitPermits = await _ladingExitPermitRepository.GetAllLadingExitPermits(validFilter);

                var ladingExitPermitViewModel = _mapper.Map<IEnumerable<LadingExitPermitViewModel>>(
                    ladingExitPermits.Skip((validFilter.PageNumber - 1) * validFilter.PageSize)
                    .Take(validFilter.PageSize).ToList());

                return new PagedResponse<IEnumerable<LadingExitPermitViewModel>>(
                    ladingExitPermitViewModel.OrderByDescending(p=>p.Id),
                    validFilter.PageNumber,
                    validFilter.PageSize, ladingExitPermits.Count());
            }
            catch (Exception e) 
            {

                throw;
            }
        }
    }
}