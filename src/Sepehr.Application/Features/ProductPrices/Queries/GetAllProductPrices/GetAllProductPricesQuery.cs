using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Sepehr.Application.Interfaces.Repositories;
using Sepehr.Application.Wrappers;
using Sepehr.Domain.Entities;
using Sepehr.Domain.ViewModels;

namespace Sepehr.Application.Features.ProductPrices.Queries.GetAllProductPrices
{
    public class GetAllProductPricesQuery : IRequest<PagedResponse<IEnumerable<ProductPriceViewModel>>>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public bool? IsActive { get; set; } = true;
        public Guid? ProductId { get; set; }
    }
    public class GetAllProductPricesQueryHandler :
         IRequestHandler<GetAllProductPricesQuery, PagedResponse<IEnumerable<ProductPriceViewModel>>>
    {
        private readonly IProductPriceRepositoryAsync _productPriceRepository;
        private readonly IMapper _mapper;
        public GetAllProductPricesQueryHandler(IProductPriceRepositoryAsync productPriceRepository, IMapper mapper)
        {
            _productPriceRepository = productPriceRepository;
            _mapper = mapper;
        }

        public async Task<PagedResponse<IEnumerable<ProductPriceViewModel>>> Handle(
            GetAllProductPricesQuery request,
            CancellationToken cancellationToken)
        {
            try
            {
                var validFilter = _mapper.Map<GetAllProductPricesParameter>(request);
                var productPrices = await _productPriceRepository.GetAllProductPrices(validFilter);
                    //.LoadAllWithRelatedAsQueryableAsync<ProductPrice>(request.PageNumber, request.PageSize,
                    //p => p.Product,
                    //p => p.ProductBrand);

                //var result = productPrices.Where(
                //        p => (p.IsActive == request.IsActive && request.IsActive != null || request.IsActive == null)
                //        && (p.ProductId == request.ProductId || request.ProductId == null)
                //    ).OrderByDescending(p => p.Created).ToList();

                var productPriceViewModel = _mapper.Map<IEnumerable<ProductPriceViewModel>>(productPrices);
                return new PagedResponse<IEnumerable<ProductPriceViewModel>>(
                    productPriceViewModel,
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