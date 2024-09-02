using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Sepehr.Application.Features.Costs.Command.CreateCost;
using Sepehr.Application.Features.Costs.Command.DeleteCostById;
using Sepehr.Application.Features.Costs.Command.UpdateCost;
using Sepehr.Application.Features.Costs.Queries.GetAllCosts;
using Sepehr.Application.Features.Costs.Queries.GetCostById;
using Sepehr.Application.Helpers;

namespace Sepehr.WebApi.Controller
{
    [ApiVersion("1.0")]
    public class CostController : BaseApiController
    {

        [HasPermission("GetAllCosts")]
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await Mediator
                .Send(new GetAllCostsQuery()));
        }

        // GET api/<controller>/5
        [HasPermission("GetCostById")]
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            return Ok(await Mediator.Send(new GetCostByIdQuery { Id = id }));
        }

        // POST api/<controller>
        [HasPermission("CreateCost")]
        [HttpPost]
        public async Task<IActionResult> Post(CreateCostCommand command)
        {
            return Ok(await Mediator.Send(command));
        }

        // PUT api/<controller>/5
        [HasPermission("UpdateCost")]
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, UpdateCostCommand command)
        {
            if (id != command.Id)
            {
                return BadRequest();
            }
            return Ok(await Mediator.Send(command));
        }

        // DELETE api/<controller>/5
        [HasPermission("DeleteCost")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            return Ok(await Mediator
                .Send(new DeleteCostByIdCommand { Id = id }));
        }


    }
}
