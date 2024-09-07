using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Sepehr.Application.Interfaces.Repositories;
using Sepehr.Application.Wrappers;
using Sepehr.Domain;
using Sepehr.Domain.Entities;
using Sepehr.Domain.Enums;
using Sepehr.Domain.ViewModels;

namespace Sepehr.Application.Features.Products.Queries.GetAllProducts
{
    public class GetAllProductsByTypeQuery : IRequest<Response<IEnumerable<ProductTypeViewModel>>>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public ProductSortBase productSortBaset { get; set; }
        public bool? ByBrand { get; set; }
        public int? WarehouseId { get; set; }
        public long? OrderCode { get; set; }
        public string? Keyword { get; set; } = string.Empty;
    }
    public class GetAllProductsByTypeQueryHandler :
         IRequestHandler<GetAllProductsByTypeQuery, Response<IEnumerable<ProductTypeViewModel>>>
    {
        private readonly IProductRepositoryAsync _productRepository;
        private readonly IMapper _mapper;
        public GetAllProductsByTypeQueryHandler(IProductRepositoryAsync productRepository,
            IProductInventoryRepositoryAsync productInventoryRepository,
            IProductBrandRepositoryAsync productBrandRepository,
            IBrandRepositoryAsync brandRepository,
            IMapper mapper)
        {
            _productRepository = productRepository;
            _mapper = mapper;
        }

        public async Task<Response<IEnumerable<ProductTypeViewModel>>> Handle(
            GetAllProductsByTypeQuery request,
            CancellationToken cancellationToken)
        {
            try
            {
                IEnumerable<ProductViewModel> productViewModel = new List<ProductViewModel>();
                var validFilter = _mapper.Map<GetAllProductsParameter>(request);

                var dapperProducts = await _productRepository.GetAllProductsByInventory(validFilter);
                
                var productsByType = _mapper.Map<IEnumerable<ProductViewModel>>(dapperProducts);
                
                var prodTypes=_mapper.Map<List<ProductTypeViewModel>>(await _productRepository.GetProductTypes());

                prodTypes.ForEach(pt => pt.Products = productsByType.Where(p => pt.Id == p.ProductTypeId).ToList());

                return new Response<IEnumerable<ProductTypeViewModel>>(prodTypes);

            }
            catch (Exception e)
            {
                throw;
            }
        }
    }
}