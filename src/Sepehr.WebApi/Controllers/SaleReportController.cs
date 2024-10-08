﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Sepehr.Application.Features.Reports.SaleReport;
using Sepehr.Application.Helpers;
using Serilog;
using Swashbuckle.AspNetCore.Annotations;

namespace Sepehr.WebApi.Controller
{
    [ApiVersion("1.0")]
    public class SaleReportController : BaseApiController
    {
        [HasPermission("GetSaleReportByProductType")]
        [SwaggerOperation("گزارش آمار فروش براساس نوع کالا")]
        [HttpGet("GetSaleReportByProductType")]
        public async Task<IActionResult> GetSaleReportByProductType([FromQuery] SaleReportByProductTypeParameter filter)
        {
            return Ok(await Mediator
                .Send(new SaleReportByProductTypeQuery()
                {
                    FromDate = filter.FromDate,
                    ToDate = filter.ToDate,
                    ProductTypeId = filter.ProductTypeId,
                }));
        }

        [HasPermission("GetSaleStatusDiagram")]
        [SwaggerOperation("نمودار وضعیت فروش")]
        [HttpGet("GetSaleStatusDiagram")]
        public async Task<IActionResult> GetSaleStatusDiagram([FromQuery] SaleReportByProductTypeParameter filter)
        {
            return Ok(await Mediator
                .Send(new SaleStatusDiagramQuery()
                {
                    FromDate = filter.FromDate,
                    ToDate = filter.ToDate,
                    ProductTypeId = filter.ProductTypeId,
                }));
        }


    }
}
