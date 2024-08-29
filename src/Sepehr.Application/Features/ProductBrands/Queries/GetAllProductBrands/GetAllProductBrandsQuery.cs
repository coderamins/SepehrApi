using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Sepehr.Application.Features.ProductBrands.Queries.GetAllProductBrands;
using Sepehr.Application.Interfaces.Repositories;
using Sepehr.Application.Wrappers;
using Sepehr.Domain.Entities;
using Sepehr.Domain.ViewModels;

namespace Sepehr.Application.Features.ProductBrands.Queries.GetAllProductBrands
{
    public class GetAllProductBrandsQuery : IRequest<PagedResponse<IEnumerable<ProductBrandViewModel>>>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public Guid? ProductId { get; set; }
    }
    public class GetAllProductBrandsQueryHandler :
         IRequestHandler<GetAllProductBrandsQuery, PagedResponse<IEnumerable<ProductBrandViewModel>>>
    {
        private readonly IProductBrandRepositoryAsync _productBrandRepository;
        private readonly IMapper _mapper;
        public GetAllProductBrandsQueryHandler(IProductBrandRepositoryAsync productBrandRepository, IMapper mapper)
        {
            _productBrandRepository = productBrandRepository;
            _mapper = mapper;
        }

        public async Task<PagedResponse<IEnumerable<ProductBrandViewModel>>> Handle(
            GetAllProductBrandsQuery request, 
            CancellationToken cancellationToken)
        {
            try
            {
                var validFilter = _mapper.Map<GetAllProductBrandsParameter>(request);
                var productBrand = 
                    await _productBrandRepository
                    .LoadAllWithRelatedAsQueryableAsync<ProductBrand>(request.PageNumber, request.PageSize,
                    p => p.Product,
                    p=> p.Product.ProductMainUnit,
                    p=> p.Product.ProductSubUnit,
                    p => p.Brand);

                productBrand = productBrand.Where((b => b.ProductId == validFilter.ProductId || validFilter.ProductId == null));

                var productBrandViewModel = _mapper.Map<IEnumerable<ProductBrandViewModel>>(productBrand);
                return new PagedResponse<IEnumerable<ProductBrandViewModel>>(
                    productBrandViewModel.OrderByDescending(p=>p.Id),
                    validFilter.PageNumber,
                    validFilter.PageSize,productBrand.Count());
            }
            catch (Exception e) 
            {

                throw;
            }
        }
    }
}