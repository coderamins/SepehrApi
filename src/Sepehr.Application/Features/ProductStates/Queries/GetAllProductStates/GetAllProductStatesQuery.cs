using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Sepehr.Application.Features.ProductStates.Queries.GetAllProductStates;
using Sepehr.Application.Interfaces.Repositories;
using Sepehr.Application.Wrappers;
using Sepehr.Domain.Entities;
using Sepehr.Domain.ViewModels;

namespace Sepehr.Application.Features.ProductStates.Queries.GetAllProductStates
{
    public class GetAllProductStatesQuery : IRequest<PagedResponse<IEnumerable<ProductStateViewModel>>>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
    }
    public class GetAllProductStatesQueryHandler :
         IRequestHandler<GetAllProductStatesQuery, PagedResponse<IEnumerable<ProductStateViewModel>>>
    {
        private readonly IProductStateRepositoryAsync _productStateRepository;
        private readonly IMapper _mapper;
        public GetAllProductStatesQueryHandler(IProductStateRepositoryAsync productStateRepository, IMapper mapper)
        {
            _productStateRepository = productStateRepository;
            _mapper = mapper;
        }

        public async Task<PagedResponse<IEnumerable<ProductStateViewModel>>> Handle(
            GetAllProductStatesQuery request, 
            CancellationToken cancellationToken)
        {
            try
            {
                var validFilter = _mapper.Map<GetAllProductStatesParameter>(request);
                var productState = await _productStateRepository.GetAllAsync();
                
                var productStateViewModel = _mapper.Map<IEnumerable<ProductStateViewModel>>(productState);
                return new PagedResponse<IEnumerable<ProductStateViewModel>>(
                    productStateViewModel.OrderByDescending(p => p.Id),
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