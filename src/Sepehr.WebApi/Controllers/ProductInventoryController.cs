using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Sepehr.Application.Features.ProductInventories.Command.CreateProductInventoryFromFile;
using Sepehr.Application.Features.ProductPrices.Command.CreateProductPrice;
using Sepehr.Application.Features.ProductPrices.Queries.GetAllProductPrices;
using Sepehr.Application.Helpers;
using Swashbuckle.AspNetCore.Annotations;

namespace Sepehr.WebApi.Controller
{
    [ApiVersion("1.0")]
    public class ProductInventoryController : BaseApiController
    {
        //[HasPermission("GetAllProductInventories")]
        //[HttpGet]
        //public async Task<IActionResult> Get([FromQuery] GetAllProductInventoriesParameter filter)
        //{
        //    return Ok(await Mediator
        //        .Send(new GetAllProductInventoriesQuery()
        //        {
        //            PageSize = filter.PageSize,
        //            PageNumber = filter.PageNumber,
        //            IsActive=filter.IsActive,
        //            ProductId=filter.ProductId,
        //        }));
        //}

        //[HasPermission("ExportProductInventories")]
        //[HttpGet("ExportProductInventories")]
        //public async Task<IActionResult> ExportProductInventories([FromQuery] ExportAllProductInventoriesToExcelParameter filter)
        //{
        //    return Ok(await Mediator
        //        .Send(new ExportAllProductInventoriesToExcelQuery()));
        //}


        // POST api/<controller>
        //[HasPermission("CreateProductInventory")]
        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Post(CreateProductInventoryCommand command)
        {
            return Ok(await Mediator.Send(command));
        }


        // POST api/<controller>
        //[HasPermission("UploadProductInventory")]
        [AllowAnonymous]
        [SwaggerOperation("بارگذاری فایل موجودی محصولات انبار سپهر")]
        [HttpPost("UploadFilePost")]
        public async Task<IActionResult> UploadFilePost([FromForm] CreateProductInventoryFromFileCommand command)
        {
            return Ok(await Mediator.Send(command));
        }

        //// PUT api/<controller>/5
        //[HasPermission("UpdateProductInventory")]
        //[HttpPut("{id}")]
        //public async Task<IActionResult> Put(Guid id, UpdateProductInventoryCommand command)
        //{
        //    if (id != command.Id)
        //    {
        //        return BadRequest();
        //    }
        //    return Ok(await Mediator.Send(command));
        //}

        //// DELETE api/<controller>/5
        //[HasPermission("DeleteProductInventory")]
        //[HttpDelete("{id}")]
        //public async Task<IActionResult> Delete(Guid id)
        //{
        //    return Ok(await Mediator
        //        .Send(new DeleteProductInventoryByIdCommand { Id = id }));
        //}

        // GET api/<controller>/5
        [HasPermission("GetProductInventoriesExcelReport")]
        [HttpGet("GetProductInventoriesExcelReport")]
        public async Task<IActionResult> GetProductInventoriesExcelReport(int? WarehouseTypeId, int? WarehouseId)
        {
            return Ok(await Mediator.Send(new GetProductInventoriesExcelReportQuery
            {
                WarehouseTypeId = WarehouseTypeId,
                WarehouseId = WarehouseId
            }));
        }

        // GET api/<controller>/5
        [HasPermission("GetInventoryUploadInstanceByHistory")]
        [HttpGet("GetInventoryUploadInstanceByHistory")]
        public async Task<IActionResult> GetInventoryUploadInstanceByHistory(string uploadedDate)
        {
            return Ok(await Mediator.Send(new InventoryUploadInstanceByHistoryQuery
            {
                UploadedDate= uploadedDate
            }));
        }

    }
}
