using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Sepehr.Application.Features.Products.Command.CreateProduct;
using Sepehr.Application.Features.Products.Command.DeleteProductById;
using Sepehr.Application.Features.Products.Command.UpdateProduct;
using Sepehr.Application.Features.Products.Queries.GetAllProducts;
using Sepehr.Application.Features.Products.Queries.GetProductById;
using Sepehr.Application.Features.TransferRemittances.Command.CreateTransferRemittance;
using Sepehr.Application.Helpers;

namespace Sepehr.WebApi.Controller
{
    [ApiVersion("1.0")]
    public class ProductController : BaseApiController
    {
        //[AllowAnonymous]
        [HasPermission("GetAllProducts")]
        [HttpGet]
        public async Task<IActionResult>
        Get([FromQuery] GetAllProductsParameter filter)
        {
            return Ok(await Mediator
                .Send(new GetAllProductsQuery()
                {
                    productSortBaset = filter.productSortBase,
                    PageSize = filter.PageSize,
                    PageNumber = filter.PageNumber,
                    ByBrand = filter.ByBrand,
                    WarehouseId=filter.WarehouseId,
                    WarehouseTypeId=filter.WarehouseTypeId,
                    ProductTypeId=filter.ProductTypeId,
                    ProductName =filter.ProductName,
                    HasPurchaseInventory=filter.HasPurchaseInventory,
                    OrderCode=filter.OrderCode,
                    Keyword = filter.Keyword,
                }));
        }

        [HasPermission("GetAllProductsByType")]
        [HttpGet("GetAllProductsByType")]
        public async Task<IActionResult> GetAllProductsByType([FromQuery] GetAllProductsParameter filter)
        {
            return Ok(await Mediator
                .Send(new GetAllProductsByTypeQuery()
                {
                    productSortBaset = filter.productSortBase,
                    WarehouseId = filter.WarehouseId,
                    OrderCode=filter.OrderCode,
                    ByBrand=filter.ByBrand,
                    Keyword=filter.Keyword,
                }));
        }

        // GET api/<controller>/5
        [HasPermission("GetProductsById")]
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            return Ok(await Mediator.Send(new GetProductByIdQuery { Id = id }));
        }

        // POST api/<controller>
        [HasPermission("CreateProduct")]
        [HttpPost]
        public async Task<IActionResult> Post(CreateProductCommand command)
        {
            return Ok(await Mediator.Send(command));
        }

        // PUT api/<controller>/5
        [HasPermission("UpdateProduct")]
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(Guid id, UpdateProductCommand command)
        {
            if (id != command.Id)
            {
                return BadRequest();
            }
            return Ok(await Mediator.Send(command));
        }

        // PUT api/<controller>/5
        [HasPermission("EnableProduct")]
        [HttpPut("EnableProduct/{id}")]
        public async Task<IActionResult> EnableProduct(Guid id, EnableProductByIdCommand command)
        {
            if (id != command.Id)
            {
                return BadRequest();
            }
            return Ok(await Mediator.Send(command));
        }

        // DELETE api/<controller>/5
        [HasPermission("DeleteProduct")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            return Ok(await Mediator
                .Send(new DeleteProductByIdCommand { Id = id }));
        }


    }
}
