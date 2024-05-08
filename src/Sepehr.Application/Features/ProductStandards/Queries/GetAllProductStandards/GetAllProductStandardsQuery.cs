using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Sepehr.Application.Interfaces.Repositories;
using Sepehr.Application.Wrappers;
using Sepehr.Domain.ViewModels;

namespace Sepehr.Application.Features.ProductStandards.Queries.GetAllProductStandards
{
    public class GetAllProductStandardsQuery : IRequest<PagedResponse<IEnumerable<ProductStandardViewModel>>>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
    }
    public class GetAllProductStandardsQueryHandler :
         IRequestHandler<GetAllProductStandardsQuery, PagedResponse<IEnumerable<ProductStandardViewModel>>>
    {
        private readonly IProductStandardRepositoryAsync _productStandardRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<GetAllProductStandardsQuery> _logger;
        public GetAllProductStandardsQueryHandler(IProductStandardRepositoryAsync ProductStandardRepository, 
            IMapper mapper, ILogger<GetAllProductStandardsQuery> logger)
        {
            _productStandardRepository = ProductStandardRepository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<PagedResponse<IEnumerable<ProductStandardViewModel>>> Handle(
            GetAllProductStandardsQuery request, 
            CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation("فراخوانی محصولات", request);
                var validFilter = _mapper.Map<GetAllProductStandardsParameter>(request);
                var ProductStandard = await _productStandardRepository.GetAllAsync();
                
                var ProductStandardViewModel = _mapper.Map<IEnumerable<ProductStandardViewModel>>(ProductStandard);
                return new PagedResponse<IEnumerable<ProductStandardViewModel>>(
                    ProductStandardViewModel.OrderByDescending(p => p.Id),
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