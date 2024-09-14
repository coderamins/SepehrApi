using AutoMapper;
using MediatR;
using Sepehr.Application.Interfaces.Repositories;
using Sepehr.Application.Wrappers;
using Sepehr.Domain.ViewModels;

namespace Sepehr.Application.Features.ProductBrands.Queries.GetAllProductPricesByProductType
{
    public class GetAllProductPricesByProductTypeQuery : IRequest<Response<IEnumerable<ProductBrandByProdTypeViewModel>>>
    {
        public string? Keyword { get; set; }
        public int? ProductTypeId { get; set; }
    }
    public class GetAllProductPricesByProductTypeQueryHandler :
         IRequestHandler<GetAllProductPricesByProductTypeQuery, Response<IEnumerable<ProductBrandByProdTypeViewModel>>>
    {
        private readonly IProductTypeRepositoryAsync _productTypes;
        private readonly IProductBrandRepositoryAsync _productBrandRepo;
        private readonly IMapper _mapper;
        public GetAllProductPricesByProductTypeQueryHandler(
            IProductBrandRepositoryAsync productBrandRepo,
            IMapper mapper, IProductTypeRepositoryAsync productTypes)
        {
            _productBrandRepo = productBrandRepo;
            _mapper = mapper;
            _productTypes = productTypes;
        }

        public async Task<Response<IEnumerable<ProductBrandByProdTypeViewModel>>> Handle(
            GetAllProductPricesByProductTypeQuery request,
            CancellationToken cancellationToken)
        {
            try
            {
                List<ProductBrandByProdTypeViewModel> result = new List<ProductBrandByProdTypeViewModel>();

                var prodTypes =await _productTypes.GetAllProductTypes();

                var validFilter = _mapper.Map<GetAllProductPricesByProductTypeParameter>(request);
                var productBrands = await _productBrandRepo
                    .GetAllProductPricesByProductType(validFilter);



                foreach(var item in prodTypes)
                {
                    result.Add(new ProductBrandByProdTypeViewModel
                    {
                        Id = item.Id,
                        Desc = item.Desc,
                        ProductBrands = _mapper.Map<List<ProductBrandViewModel>>(productBrands)
                    });
                }
                
                return new Response<IEnumerable<ProductBrandByProdTypeViewModel>>(result);

            }
            catch (Exception e)
            {

                throw;
            }
        }
    }
}