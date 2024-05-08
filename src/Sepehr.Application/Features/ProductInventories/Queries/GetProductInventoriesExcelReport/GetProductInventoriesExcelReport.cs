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
    public class GetProductInventoriesExcelReportQuery : IRequest<Response<byte[]>>
    {
        public int? WarehouseTypeId { get; set; }
        public int? WarehouseId { get; set; }
    }
    public class GetProductInventoriesExcelReportQueryHandler :
         IRequestHandler<GetProductInventoriesExcelReportQuery, Response<byte[]>>
    {
        private readonly IProductInventoryRepositoryAsync _productInventoryRepository;
        private readonly IMapper _mapper;
        private readonly IExportUtility _exportUtility;
        public GetProductInventoriesExcelReportQueryHandler(
            IProductInventoryRepositoryAsync productInventoryRepository,
            IMapper mapper,
            IExportUtility exportUtility)
        {
            _productInventoryRepository = productInventoryRepository;
            _mapper = mapper;
            _exportUtility = exportUtility;
        }

        public async Task<Response<byte[]>> Handle(
            GetProductInventoriesExcelReportQuery request,
            CancellationToken cancellationToken)
        {
            try
            {
                var prodInventories = await _productInventoryRepository
                    .GetProductInventories(request.WarehouseTypeId, request.WarehouseId);

                byte[] filecontent =new byte[0];
                if (request.WarehouseId == null)
                {
                    string[,] columns =
                    {
                        { "ProductName" ,"کد کالا"},
                        { "ProductCode" ,"نام کالا"},
                        { "ProductBrandId" ,"کد برند"},
                        { "ProductBrandName" ,"شرح برند"},
                        { "WarehouseName" ,"انبار"},
                        { "ApproximateInventory" ,"موجودی"}
                    };

                    filecontent = _exportUtility.ExportExcel(prodInventories.ToList(), columns, "", false);

                }
                else
                {
                    string[,] columns =
                    {
                        { "ProductBrandId" ,"کد کالا برند"},
                        { "ProductCode" ,"کد کالا"},
                        { "ProductName" ,"شرح کالا"},
                        { "BrandId" ,"کد برند"},
                        { "ProductBrandName" ,"شرح برند"},
                        { "ApproximateInventory" ,"موجودی"}
                    };

                    filecontent = _exportUtility.ExportExcel(prodInventories.ToList(), columns, "", false);

                }

                return new Response<byte[]>(filecontent);

            }
            catch (Exception e)
            {

                throw;
            }
        }
    }
}