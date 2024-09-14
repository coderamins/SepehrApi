using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Sepehr.Application.Interfaces.Repositories;
using Sepehr.Application.Wrappers;
using Sepehr.Domain.ViewModels;

namespace Sepehr.Application.Features.DraftOrders.Queries.GetAllDraftOrders
{
    public class GetAllDraftOrdersQuery : IRequest<PagedResponse<IEnumerable<DraftOrderViewModel>>>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public bool? Converted { get; set; }
        public Guid? CreatorId { get; set; }
    }
    public class GetAllDraftOrdersQueryHandler :
         IRequestHandler<GetAllDraftOrdersQuery, PagedResponse<IEnumerable<DraftOrderViewModel>>>
    {
        private readonly IDraftOrderRepositoryAsync _draftOrderRepository;
        private readonly IMapper _mapper;
        public GetAllDraftOrdersQueryHandler(IDraftOrderRepositoryAsync draftOrderRepository, IMapper mapper)
        {
            _draftOrderRepository = draftOrderRepository;
            _mapper = mapper;
        }

        public async Task<PagedResponse<IEnumerable<DraftOrderViewModel>>> Handle(
            GetAllDraftOrdersQuery request,
            CancellationToken cancellationToken)
        {
            try
            {
                var validFilter = _mapper.Map<GetAllDraftOrdersParameter>(request);
                var draftOrders = await _draftOrderRepository.GetAllDraftOrders(validFilter);

                var draftOrderViewModel = _mapper.Map<IEnumerable<DraftOrderViewModel>>(draftOrders);
                return new PagedResponse<IEnumerable<DraftOrderViewModel>>(
                    draftOrderViewModel,
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