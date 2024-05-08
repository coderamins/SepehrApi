using Microsoft.AspNetCore.Mvc;
using Sepehr.Application.Features.ProductTypes.Command.CreateProductType;
using Sepehr.Application.Features.ProductTypes.Command.DeleteProductTypeById;
using Sepehr.Application.Features.ProductTypes.Command.UpdateProductType;
using Sepehr.Application.Features.ProductTypes.Queries.GetAllProductTypes;
using Sepehr.Application.Features.ProductTypes.Queries.GetProductTypeById;
using Sepehr.Infrastructure.Authentication;

namespace Sepehr.WebApi.Controller
{
    [ApiVersion("1.0")]
    public class ProductTypeController : BaseApiController
    {

        [HasPermission("GetAllProductTypes")]
        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] GetAllProductTypesParameter filter)
        {
            return Ok(await Mediator
                .Send(new GetAllProductTypesQuery()
                {
                    PageSize = filter.PageSize,
                    PageNumber = filter.PageNumber
                }));
        }

        // GET api/<controller>/5
        [HasPermission("GetProductTypeById")]
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            return Ok(await Mediator.Send(new GetProductTypeByIdQuery { Id = id }));
        }

        // POST api/<controller>
        [HasPermission("CreateProductType")]
        [HttpPost]
        public async Task<IActionResult> Post(CreateProductTypeCommand command)
        {
            return Ok(await Mediator.Send(command));
        }

        // PUT api/<controller>/5
        [HasPermission("UpdateProductType")]
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, UpdateProductTypeCommand command)
        {
            if (id != command.Id)
            {
                return BadRequest();
            }
            return Ok(await Mediator.Send(command));
        }

        // DELETE api/<controller>/5
        [HasPermission("DeleteProductType")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            return Ok(await Mediator
                .Send(new DeleteProductTypeByIdCommand { Id = id }));
        }


    }
}
