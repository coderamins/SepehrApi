using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Sepehr.Application.Interfaces;
using Sepehr.Application.Interfaces.Repositories;
using Sepehr.Application.Wrappers;
using Sepehr.Domain.Entities;
using Sepehr.Domain.ViewModels;

namespace Sepehr.Application.Features.ProductPrices.Queries.GetAllProductPrices
{
    public class InventoryUploadInstanceByHistoryQuery : IRequest<Response<byte[]>>
    {
        public required string UploadedDate { get; set; }
    }
    public class ExportProductsToUploadInventoryQueryHandler :
         IRequestHandler<InventoryUploadInstanceByHistoryQuery, Response<byte[]>>
    {
        private readonly IProductInventoryRepositoryAsync _productInventoryRepository;
        private readonly IMapper _mapper;
        private readonly IExportUtility _exportUtility;
        public ExportProductsToUploadInventoryQueryHandler(
            IProductInventoryRepositoryAsync productInventoryRepository,
            IMapper mapper,
            IExportUtility exportUtility)
        {
            _productInventoryRepository = productInventoryRepository;
            _mapper = mapper;
            _exportUtility = exportUtility;
        }

        public async Task<Response<byte[]>> Handle(
            InventoryUploadInstanceByHistoryQuery request,
            CancellationToken cancellationToken)
        {
            try
            {
                var prodInventories = await _productInventoryRepository
                    .GetUploadedInventoryFromHistory(request.UploadedDate);

                string[,] columns =
                {
                        { "productBrandId" ,"کد کالا برند"},
                        { "productCode" ,"کد کالا"},
                        { "productName" ,"شرح کالا"},
                        { "BrandId" ,"کد برند"},
                        { "BrandName" ,"شرح برند"},
                        { "ApproximateInventory" ,"موجودی"}
                    };

                var filecontent = _exportUtility.ExportExcel(prodInventories.ToList(), columns, "", false);

                return new Response<byte[]>(filecontent);

            }
            catch (Exception e)
            {

                throw;
            }
        }
    }
}