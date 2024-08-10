using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Sepehr.Application.DTOs.TransferRemittanceUnloadingPermit;
using Sepehr.Application.Features.UnloadingPermits.Command.DeleteUnloadingPermitById;
using Sepehr.Application.Features.UnloadingPermits.Command.RevokeUnloadingPermit;
using Sepehr.Application.Features.UnloadingPermits.Command.UpdateUnloadingPermit;
using Sepehr.Application.Features.UnloadingPermits.Queries.GetAllUnloadingPermits;
using Sepehr.Application.Features.UnloadingPermits.Queries.GetUnloadingPermitByCode;
using Sepehr.Application.Features.UnloadingPermits.Queries.GetUnloadingPermitById;
using Sepehr.Infrastructure.Authentication;

namespace Sepehr.WebApi.Controller
{
    [ApiVersion("1.0")]
    public class UnloadingPermitController : BaseApiController
    {

        [HttpGet]
        [HasPermission("GetAllUnloadingPermits")]
        public async Task<IActionResult> Get([FromQuery] GetAllUnloadingPermitsParameter filter)
        {
            return Ok(await Mediator
                .Send(new GetAllUnloadingPermitsQuery()
                {
                    PageSize = filter.PageSize,
                    PageNumber = filter.PageNumber,
                    UnloadingPermitCode=filter.UnloadingPermitCode
                }));
        }

        // GET api/<controller>/5
        [HasPermission("GetUnloadingPermitById")]
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            return Ok(await Mediator.Send(new GetUnloadingPermitByIdQuery { Id = id }));
        }

        // GET api/<controller>/5
        [HasPermission("GetUnloadingPermitByCode")]
        [HttpGet("{unloadingPermitCode}")]
        public async Task<IActionResult> GetUnloadingPermitByCode(int unloadingPermitCode)
        {
            return Ok(await Mediator.Send(new GetUnloadingPermitByCodeQuery { unloadingPermitCode = unloadingPermitCode }));
        }

        // POST api/<controller>
        [HasPermission("CreateUnloadingPermit")]
        [HttpPost]
        public async Task<IActionResult> Post(CreateUnloadingPermitCommand command)
        {
            return Ok(await Mediator.Send(command));
        }

        // PUT api/<controller>/5
        [HasPermission("UpdateUnloadingPermit")]
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(Guid id, UpdateUnloadingPermitCommand command)
        {
            if (id != command.Id)
            {
                return BadRequest();
            }
            return Ok(await Mediator.Send(command));
        }

        // PUT api/<controller>/5
        [HasPermission("RevokeUnloadingPermit")]
        [HttpPut("RevokeUnloadingPermit/{id}")]
        public async Task<IActionResult> RevokeUnloadingPermit(Guid id, RevokeUnloadingPermitCommand command)
        {
            if (id != command.Id)
            {
                return BadRequest();
            }
            return Ok(await Mediator.Send(command));
        }

        // DELETE api/<controller>/5
        [HasPermission("DeleteUnloadingPermit")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            return Ok(await Mediator
                .Send(new DeleteUnloadingPermitByIdCommand { Id = id }));
        }

    }
}
