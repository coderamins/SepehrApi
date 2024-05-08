using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Sepehr.Application.Features.CashDesks.Queries.GetAllCashDesks;
using Sepehr.Application.Interfaces.Repositories;
using Sepehr.Application.Wrappers;
using Sepehr.Domain.Entities;
using Sepehr.Domain.ViewModels;

namespace Sepehr.Application.Features.CashDesks.Queries.GetAllCashDesks
{
    public class GetAllCashDesksQuery : IRequest<PagedResponse<IEnumerable<CashDeskViewModel>>>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public Guid? ProductId { get; set; }
        public int? CashDeskId { get; set; }
    }
    public class GetAllCashDesksQueryHandler :
         IRequestHandler<GetAllCashDesksQuery, PagedResponse<IEnumerable<CashDeskViewModel>>>
    {
        private readonly ICashDeskRepositoryAsync _cashDeskRepository;
        private readonly IMapper _mapper;
        public GetAllCashDesksQueryHandler(ICashDeskRepositoryAsync cashDeskRepository, IMapper mapper)
        {
            _cashDeskRepository = cashDeskRepository;
            _mapper = mapper;
        }

        public async Task<PagedResponse<IEnumerable<CashDeskViewModel>>> Handle(
            GetAllCashDesksQuery request, 
            CancellationToken cancellationToken)
        {
            try
            {
                var validFilter = _mapper.Map<GetAllCashDesksParameter>(request);
                var cashDesk = 
                    await _cashDeskRepository
                    .LoadAllWithRelatedAsQueryableAsync<CashDesk>(request.PageNumber, request.PageSize);

                cashDesk = cashDesk.Where((b => b.Id == validFilter.CashDeskId || validFilter.CashDeskId == null));

                var cashDeskViewModel = _mapper.Map<IEnumerable<CashDeskViewModel>>(cashDesk);
                return new PagedResponse<IEnumerable<CashDeskViewModel>>(
                    cashDeskViewModel.OrderByDescending(p=>p.Id),
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