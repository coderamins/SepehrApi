using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Sepehr.Application.Features.ProductStates.Command.CreateProductState;
using Sepehr.Application.Features.ProductStates.Command.DeleteProductStateById;
using Sepehr.Application.Features.ProductStates.Command.UpdateProductState;
using Sepehr.Application.Features.ProductStates.Queries.GetAllProductStates;
using Sepehr.Application.Features.ProductStates.Queries.GetProductStateById;
using Sepehr.Application.Helpers;

namespace Sepehr.WebApi.Controller
{
    [ApiVersion("1.0")]
    public class ProductStateController : BaseApiController
    {
        [HasPermission("GetAllProductStates,CreateProductState")]
        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] GetAllProductStatesParameter filter)
        {
            return Ok(await Mediator
                .Send(new GetAllProductStatesQuery()
                {
                    PageSize = filter.PageSize,
                    PageNumber = filter.PageNumber
                }));
        }

        // GET api/<controller>/5
        [HasPermission("GetProductStateById,CreateProductState")]
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            return Ok(await Mediator.Send(new GetProductStateByIdQuery { Id = id }));
        }

        // POST api/<controller>
        [HasPermission("CreateProductState")]
        [HttpPost]
        public async Task<IActionResult> Post(CreateProductStateCommand command)
        {
            return Ok(await Mediator.Send(command));
        }

        // PUT api/<controller>/5
        [HasPermission("UpdateProductStat")]
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, UpdateProductStateCommand command)
        {
            if (id != command.Id)
            {
                return BadRequest();
            }
            return Ok(await Mediator.Send(command));
        }

        // DELETE api/<controller>/5
        [HasPermission("DeleteProductStat")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            return Ok(await Mediator
                .Send(new DeleteProductStateByIdCommand { Id = id }));
        }


    }
}
