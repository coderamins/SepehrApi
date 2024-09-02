using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Sepehr.Application.Features.PettyCashs.Queries.GetAllPettyCashs;
using Sepehr.Application.Interfaces.Repositories;
using Sepehr.Application.Wrappers;
using Sepehr.Domain.Entities;
using Sepehr.Domain.ViewModels;

namespace Sepehr.Application.Features.PettyCashs.Queries.GetAllPettyCashs
{
    public class GetAllPettyCashsQuery : IRequest<PagedResponse<IEnumerable<PettyCashViewModel>>>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public Guid? ProductId { get; set; }
        public int? PettyCashId { get; set; }
    }
    public class GetAllPettyCashsQueryHandler :
         IRequestHandler<GetAllPettyCashsQuery, PagedResponse<IEnumerable<PettyCashViewModel>>>
    {
        private readonly IPettyCashRepositoryAsync _PettyCashRepository;
        private readonly IMapper _mapper;
        public GetAllPettyCashsQueryHandler(IPettyCashRepositoryAsync PettyCashRepository, IMapper mapper)
        {
            _PettyCashRepository = PettyCashRepository;
            _mapper = mapper;
        }

        public async Task<PagedResponse<IEnumerable<PettyCashViewModel>>> Handle(
            GetAllPettyCashsQuery request, 
            CancellationToken cancellationToken)
        {
            try
            {
                int TotalCount = 0;
                var validFilter = _mapper.Map<GetAllPettyCashsParameter>(request);
                var PettyCash = 
                    _PettyCashRepository
                    .LoadAllWithRelatedAsQueryableAsync<PettyCash>(request.PageNumber, request.PageSize,out TotalCount);

                PettyCash = PettyCash.Where((b => b.Id == validFilter.PettyCashId || validFilter.PettyCashId == null));

                var PettyCashViewModel = _mapper.Map<IEnumerable<PettyCashViewModel>>(PettyCash);
                return new PagedResponse<IEnumerable<PettyCashViewModel>>(
                    PettyCashViewModel.OrderByDescending(p=>p.Id),
                    validFilter.PageNumber,
                    validFilter.PageSize);
            }
            catch (Exception e) 
            {

                throw;
            }
        }
    }
}