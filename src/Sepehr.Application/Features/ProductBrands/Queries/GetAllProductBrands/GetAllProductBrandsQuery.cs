using AutoMapper;
using MediatR;
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
        public string Keyword { get; set; } = string.Empty;
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
                int TotalCount = 0;

                var productBrands = 
                    _productBrandRepository
                    .LoadAllWithRelatedAsQueryableAsync<ProductBrand>(request.PageNumber, request.PageSize,
                    out TotalCount,
                    p => p.Product,
                    p => p.Product.ProductType,
                    p => p.Product.ProductMainUnit,
                    p=> p.Product.ProductSubUnit,
                    p => p.Brand);

                var whereClause = "";
                foreach (var keyword in validFilter.Keyword)
                {
                    whereClause += $" AND ProductName LIKE '%{keyword}%'";
                }

                foreach (var item in validFilter.Keyword.Split(' '))
                {
                    productBrands = productBrands.Where(
                        (b =>
                            b.Product.ProductCode.ToString().Contains(item) ||
                            b.Product.ProductName.Contains(item) ||
                            b.Brand.Name.Contains(item) ||
                            b.Product.ProductType.Desc.Contains(item) ||
                            string.IsNullOrEmpty(item));
                }

                productBrands = productBrands.Where(b => b.ProductId == validFilter.ProductId || validFilter.ProductId == null);

                var productBrandsViewModel = _mapper.Map<IEnumerable<ProductBrandViewModel>>(
                             productBrands.Skip((validFilter.PageNumber - 1) * validFilter.PageSize)
                             .Take(validFilter.PageSize).OrderByDescending(x=>x.Id));

                return new PagedResponse<IEnumerable<ProductBrandViewModel>>(
                    productBrandsViewModel,
                    validFilter.PageNumber,
                    validFilter.PageSize,TotalCount);
            }
            catch (Exception e) 
            {

                throw;
            }
        }
    }
}