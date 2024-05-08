using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Sepehr.Application.Features.ProductStandards.Command.CreateProductStandard;
using Sepehr.Application.Features.ProductStandards.Command.DeleteProductStandardById;
using Sepehr.Application.Features.ProductStandards.Command.UpdateProductStandard;
using Sepehr.Application.Features.ProductStandards.Queries.GetAllProductStandards;
using Sepehr.Application.Features.ProductStandards.Queries.GetProductStandardById;
using Sepehr.Infrastructure.Authentication;

namespace Sepehr.WebApi.Controller
{
    [ApiVersion("1.0")]
    public class ProductStandardController : BaseApiController
    {

        [HasPermission("GetAllProductStandards")]
        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] GetAllProductStandardsParameter filter)
        {
            return Ok(await Mediator
                .Send(new GetAllProductStandardsQuery()
                {
                    PageSize = filter.PageSize,
                    PageNumber = filter.PageNumber
                }));
        }

        // GET api/<controller>/5
        [HasPermission("GetProductStandardById")]
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            return Ok(await Mediator.Send(new GetProductStandardByIdQuery { Id = id }));
        }

        // POST api/<controller>
        [HasPermission("CreateProductStandard")]
        [HttpPost]
        public async Task<IActionResult> Post(CreateProductStandardCommand command)
        {
            return Ok(await Mediator.Send(command));
        }

        // PUT api/<controller>/5
        [HasPermission("UpdateProductStandard")]
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, UpdateProductStandardCommand command)
        {
            if (id != command.Id)
            {
                return BadRequest();
            }
            return Ok(await Mediator.Send(command));
        }

        // DELETE api/<controller>/5
        [HasPermission("DeleteProductStandard")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            return Ok(await Mediator
                .Send(new DeleteProductStandardByIdCommand { Id = id }));
        }


    }
}
