using AutoMapper;
using MediatR;
using Sepehr.Application.Interfaces;
using Sepehr.Application.Interfaces.Repositories;
using Sepehr.Application.Wrappers;
using Sepehr.Domain.ViewModels;

namespace Sepehr.Application.Features.ProductPrices.Queries.GetAllProductPrices
{
    public class ExportAllProductPricesToExcelQuery : IRequest<Response<byte[]>>
    {
    }
    public class ExportAllProductPricesToExcelQueryHandler :
         IRequestHandler<ExportAllProductPricesToExcelQuery, Response<byte[]>>
    {
        private readonly IProductPriceRepositoryAsync _productPriceRepository;
        private readonly IMapper _mapper;
        private readonly IExportUtility _exportUtility;
        public ExportAllProductPricesToExcelQueryHandler(
            IProductPriceRepositoryAsync productPriceRepository, IMapper mapper, IExportUtility exportUtility)
        {
            _productPriceRepository = productPriceRepository;
            _mapper = mapper;
            _exportUtility = exportUtility;
        }

        public async Task<Response<byte[]>> Handle(
            ExportAllProductPricesToExcelQuery request,
            CancellationToken cancellationToken)
        {
            try
            {
                var validFilter = _mapper.Map<GetAllProductPricesParameter>(request);
                var productPrice = await _productPriceRepository.GetAllProductPrices(validFilter);

                var prodPrices = _mapper.Map<IEnumerable<ProductPriceViewModel>>(productPrice);

                string[,] columns =
{
                    { "ProductName" ,"عنوان کالا"},
                    { "ProductCode" ,"کد کالا"},
                    { "RegisterDate" ,"تاریخ ثبت"},
                    { "BrandName" ,"نام برند"},
                    { "ProductBrandId" ,"کد برند"},
                    { "Price" ,"قیمت"}
                };

                byte[] filecontent = _exportUtility.ExportExcel(prodPrices.ToList(), columns, "", false);

                return new Response<byte[]>(filecontent);
            }
            catch (Exception e)
            {

                throw;
            }
        }
    }
}