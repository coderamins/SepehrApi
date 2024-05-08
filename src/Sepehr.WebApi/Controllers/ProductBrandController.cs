using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Sepehr.Application.Features.ProductBrands.Command.CreateProductBrand;
using Sepehr.Application.Features.ProductBrands.Command.DeleteProductBrandById;
using Sepehr.Application.Features.ProductBrands.Command.UpdateProductBrand;
using Sepehr.Application.Features.ProductBrands.Queries.GetAllProductBrands;
using Sepehr.Application.Features.ProductBrands.Queries.GetProductBrandById;
using Sepehr.Infrastructure.Authentication;

namespace Sepehr.WebApi.Controller
{
    [ApiVersion("1.0")]
    public class ProductBrandController : BaseApiController
    {
        [HasPermission("GetAllProductBrands")]
        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] GetAllProductBrandsParameter filter)
        {
            return Ok(await Mediator
                .Send(new GetAllProductBrandsQuery()
                {
                    PageSize = filter.PageSize,
                    PageNumber = filter.PageNumber,
                    ProductId=filter.ProductId,
                }));
        }

        // GET api/<controller>/5
        [HasPermission("GetProductBrandById")]
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            return Ok(await Mediator.Send(new GetProductBrandByIdQuery { Id = id }));
        }

        // POST api/<controller>
        [HasPermission("CreateProductBran")]
        [HttpPost]
        public async Task<IActionResult> Post(CreateProductBrandCommand command)
        {
            return Ok(await Mediator.Send(command));
        }

        // PUT api/<controller>/5
        [HasPermission("UpdateProductBrand")]
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, UpdateProductBrandCommand command)
        {
            if (id != command.Id)
            {
                return BadRequest();
            }
            return Ok(await Mediator.Send(command));
        }

        // DELETE api/<controller>/5
        [HasPermission("DeleteProductBrand")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            return Ok(await Mediator
                .Send(new DeleteProductBrandByIdCommand { Id = id }));
        }


    }
}
