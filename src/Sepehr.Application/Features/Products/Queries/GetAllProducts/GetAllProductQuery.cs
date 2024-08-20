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
    public class GetAllProductsQuery : IRequest<PagedResponse<IEnumerable<ProductViewModel>>>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public ProductSortBase productSortBaset { get; set; }
        public bool? ByBrand { get; set; }
        public int? WarehouseId { get; set; }
        public int? WarehouseTypeId { get; set; }
        public int? ProductTypeId { get; set; }
        public string ProductName { get; set; } = string.Empty;
        public bool? HasPurchaseInventory { get; set; }
        public long? OrderCode { get; set; }
    }
    public class GetAllProductsQueryHandler :
         IRequestHandler<GetAllProductsQuery, PagedResponse<IEnumerable<ProductViewModel>>>
    {
        private readonly IProductRepositoryAsync _productRepository;
        private readonly IProductInventoryRepositoryAsync _productInventoryRepository;
        private readonly IProductBrandRepositoryAsync _productBrandRepository;
        private readonly IBrandRepositoryAsync _brandRepository;
        private readonly IMapper _mapper;
        public GetAllProductsQueryHandler(IProductRepositoryAsync productRepository,
            IProductInventoryRepositoryAsync productInventoryRepository,
            IProductBrandRepositoryAsync productBrandRepository,
            IBrandRepositoryAsync brandRepository,
            IMapper mapper)
        {
            _productRepository = productRepository;
            _productInventoryRepository = productInventoryRepository;
            _productBrandRepository = productBrandRepository;
            _brandRepository = brandRepository;
            _mapper = mapper;
        }

        public async Task<PagedResponse<IEnumerable<ProductViewModel>>> Handle(
            GetAllProductsQuery request,
            CancellationToken cancellationToken)
        {
            try
            {
                IEnumerable<ProductViewModel> productViewModel = new List<ProductViewModel>();
                var validFilter = _mapper.Map<GetAllProductsParameter>(request);

                var product = await _productRepository.GetAllProducts(validFilter);
                if (request.ByBrand == true)
                {
                    var dapperProducts = await _productRepository.GetAllProductsByInventory(validFilter);
                    productViewModel = _mapper.Map<IEnumerable<ProductViewModel>>(dapperProducts);

                    return new PagedResponse<IEnumerable<ProductViewModel>>(
                        productViewModel,
                        validFilter.PageNumber,
                        validFilter.PageSize);
                }

                productViewModel = _mapper.Map<IEnumerable<ProductViewModel>>(product);
                return new PagedResponse<IEnumerable<ProductViewModel>>(
                    productViewModel,
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