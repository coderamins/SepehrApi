using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Sepehr.Application.Features.CashDesks.Command.CreateCashDesk;
using Sepehr.Application.Features.CashDesks.Command.DeleteCashDeskById;
using Sepehr.Application.Features.CashDesks.Command.UpdateCashDesk;
using Sepehr.Application.Features.CashDesks.Queries.GetAllCashDesks;
using Sepehr.Application.Features.CashDesks.Queries.GetCashDeskById;
using Sepehr.Infrastructure.Authentication;

namespace Sepehr.WebApi.Controller
{
    [ApiVersion("1.0")]
    public class CashDeskController : BaseApiController
    {
        [HasPermission("GetAllCashDesks")]
        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] GetAllCashDesksParameter filter)
        {
            return Ok(await Mediator
                .Send(new GetAllCashDesksQuery()
                {
                    PageSize = filter.PageSize,
                    PageNumber = filter.PageNumber,
                    CashDeskId = filter.CashDeskId
                }));
        }

        // GET api/<controller>/5
        [HasPermission("GetCashDeskById")]
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            return Ok(await Mediator.Send(new GetCashDeskByIdQuery { Id = id }));
        }

        // POST api/<controller>
        [HasPermission("CreateCashDesk")]
        [HttpPost]
        public async Task<IActionResult> Post(CreateCashDeskCommand command)
        {
            return Ok(await Mediator.Send(command));
        }

        // PUT api/<controller>/5
        [HasPermission("UpdateCashDesk")]
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, UpdateCashDeskCommand command)
        {
            if (id != command.Id)
            {
                return BadRequest();
            }
            return Ok(await Mediator.Send(command));
        }

        // DELETE api/<controller>/5
        [HasPermission("DeleteCashDesk")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            return Ok(await Mediator
                .Send(new DeleteCashDeskByIdCommand { Id = id }));
        }


    }
}
