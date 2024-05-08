using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Sepehr.Application.Features.ProductPrices.Command.CreateProductPrice;
using Sepehr.Application.Features.ProductPrices.Command.CreateProductPriceFromFile;
using Sepehr.Application.Features.ProductPrices.Command.DeleteProductPriceById;
using Sepehr.Application.Features.ProductPrices.Command.UpdateProductPrice;
using Sepehr.Application.Features.ProductPrices.Queries.GetAllProductPrices;
using Sepehr.Application.Features.ProductPrices.Queries.GetProductPriceById;
using Sepehr.Application.Interfaces;
using Sepehr.Application.Interfaces.Repositories;
using Sepehr.Domain.Entities;
using Sepehr.Infrastructure.Authentication;
using Sepehr.Infrastructure.Persistence.Repositories;

namespace Sepehr.WebApi.Controller
{
    [ApiVersion("1.0")]
    public class ProductPriceController : BaseApiController
    {
        [HasPermission("GetAllProductPrices")]
        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] GetAllProductPricesParameter filter)
        {
            return Ok(await Mediator
                .Send(new GetAllProductPricesQuery()
                {
                    PageSize = filter.PageSize,
                    PageNumber = filter.PageNumber,
                    IsActive=filter.IsActive,
                    ProductId=filter.ProductId,
                }));
        }

        [HasPermission("ExportProductPrices")]
        [HttpGet("ExportProductPrices")]
        public async Task<IActionResult> ExportProductPrices([FromQuery] ExportAllProductPricesToExcelParameter filter)
        {
            return Ok(await Mediator
                .Send(new ExportAllProductPricesToExcelQuery()));
        }

        // GET api/<controller>/5
        [HasPermission("GetProductPriceById")]
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            return Ok(await Mediator.Send(new GetProductPriceByIdQuery { Id = id }));
        }

        // POST api/<controller>
        [HasPermission("CreateProductPrice")]
        [HttpPost]
        public async Task<IActionResult> Post(CreateProductPriceCommand command)
        {
            return Ok(await Mediator.Send(command));
        }

        // POST api/<controller>
        [HasPermission("CreateProductPriceFromFile")]
        [HttpPost("UploadFilePost")]
        public async Task<IActionResult> UploadFilePost([FromForm]CreateProductPriceFromFileCommand command)
        {
            return Ok(await Mediator.Send(command));
        }

        // PUT api/<controller>/5
        [HasPermission("UpdateProductPrice")]
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(Guid id, UpdateProductPriceCommand command)
        {
            if (id != command.Id)
            {
                return BadRequest();
            }
            return Ok(await Mediator.Send(command));
        }

        // DELETE api/<controller>/5
        [HasPermission("DeleteProductPrice")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            return Ok(await Mediator
                .Send(new DeleteProductPriceByIdCommand { Id = id }));
        }


    }
}
