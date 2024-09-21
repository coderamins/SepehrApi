using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Sepehr.Application.Features.PettyCashs.Command.CreatePettyCash;
using Sepehr.Application.Features.PettyCashs.Command.DeletePettyCashById;
using Sepehr.Application.Features.PettyCashs.Command.UpdatePettyCash;
using Sepehr.Application.Features.PettyCashs.Queries.GetAllPettyCashs;
using Sepehr.Application.Features.PettyCashs.Queries.GetPettyCashById;
using Sepehr.Application.Helpers;

namespace Sepehr.WebApi.Controller
{
    [ApiVersion("1.0")]
    public class PettyCashController : BaseApiController
    {
        //[HasPermission("GetAllPettyCashs")]
        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] GetAllPettyCashsParameter filter)
        {
            return Ok(await Mediator
                .Send(new GetAllPettyCashsQuery()
                {
                    PageSize = filter.PageSize,
                    PageNumber = filter.PageNumber,
                    PettyCashId = filter.PettyCashId
                }));
        }

        // GET api/<controller>/5
        [HasPermission("GetPettyCashById")]
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            return Ok(await Mediator.Send(new GetPettyCashByIdQuery { Id = id }));
        }

        // POST api/<controller>
        [HasPermission("CreatePettyCash")]
        [HttpPost]
        public async Task<IActionResult> Post(CreatePettyCashCommand command)
        {
            return Ok(await Mediator.Send(command));
        }

        // PUT api/<controller>/5
        [HasPermission("UpdatePettyCash")]
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, UpdatePettyCashCommand command)
        {
            if (id != command.Id)
            {
                return BadRequest();
            }
            return Ok(await Mediator.Send(command));
        }

        // DELETE api/<controller>/5
        [HasPermission("DeletePettyCash")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            return Ok(await Mediator
                .Send(new DeletePettyCashByIdCommand { Id = id }));
        }


    }
}
