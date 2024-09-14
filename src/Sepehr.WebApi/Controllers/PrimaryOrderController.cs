using Microsoft.AspNetCore.Mvc;
using Sepehr.Application.Features.DraftOrders.Command.CreateDraftOrder;
using Sepehr.Application.Features.DraftOrders.Command.DeleteDraftOrderById;
using Sepehr.Application.Features.DraftOrders.Command.UpdateDraftOrder;
using Sepehr.Application.Features.DraftOrders.Queries.GetAllDraftOrders;
using Sepehr.Application.Features.DraftOrders.Queries.GetDraftOrderById;
using Sepehr.Application.Helpers;

namespace Sepehr.WebApi.Controller
{
    [ApiVersion("1.0")]
    public class DraftOrderController : BaseApiController
    {
        [HasPermission("GetAllDraftOrders")]
        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] GetAllDraftOrdersParameter filter)
        {
            return Ok(await Mediator
                .Send(new GetAllDraftOrdersQuery()
                {
                    PageSize = filter.PageSize,
                    PageNumber = filter.PageNumber,
                    Converted=filter.Converted,
                    CreatorId=filter.CreatorId
                }));
        }

        // GET api/<controller>/5
        [HasPermission("GetDraftOrderById")]
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            return Ok(await Mediator.Send(new GetDraftOrderByIdQuery { Id = id }));
        }

        // POST api/<controller>
        [HasPermission("CreateDraftOrder")]
        [HttpPost]
        public async Task<IActionResult> Post(CreateDraftOrderCommand command)
        {
            return Ok(await Mediator.Send(command));
        }

        // PUT api/<controller>/5
        [HasPermission("UpdateDraftOrder")]
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, UpdateDraftOrderCommand command)
        {
            if (id != command.Id)
            {
                return BadRequest();
            }
            return Ok(await Mediator.Send(command));
        }

        // DELETE api/<controller>/5
        [HasPermission("DeleteDraftOrder")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            return Ok(await Mediator
                .Send(new DeleteDraftOrderByIdCommand { Id = id }));
        }


    }
}
