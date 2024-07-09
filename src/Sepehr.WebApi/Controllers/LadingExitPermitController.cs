using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Sepehr.Application.Features.DriverFareAmounts;
using Sepehr.Application.Features.LadingExitPermits.Command.CreateLadingExitPermit;
using Sepehr.Application.Features.LadingExitPermits.Command.DeleteLadingExitPermitById;
using Sepehr.Application.Features.LadingExitPermits.Command.RevokeLadingExitPermit;
using Sepehr.Application.Features.LadingExitPermits.Command.UpdateLadingExitPermit;
using Sepehr.Application.Features.LadingExitPermits.Queries.GetAllLadingExitPermits;
using Sepehr.Application.Features.LadingExitPermits.Queries.GetLadingExitPermitById;
using Sepehr.Infrastructure.Authentication;

namespace Sepehr.WebApi.Controller
{
    [ApiVersion("1.0")]
    public class LadingExitPermitController : BaseApiController
    {
        [HasPermission("GetAllLadingExitPermits")]
        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] GetAllLadingExitPermitsParameter filter)
        {
            return Ok(await Mediator
                .Send(new GetAllLadingExitPermitsQuery()
                {
                    PageSize = filter.PageSize,
                    PageNumber = filter.PageNumber,
                    LadingPermitId = filter.LadingPermitId
                }));
        }

        // GET api/<controller>/5
        [HasPermission("GetLadingExitPermitById")]
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            return Ok(await Mediator.Send(new GetLadingExitPermitByIdQuery { Id = id }));
        }

        // POST api/<controller>
        [HasPermission("CreateLadingExitPermit")]
        [HttpPost]
        public async Task<IActionResult> Post(CreateLadingExitPermitCommand command)
        {
            return Ok(await Mediator.Send(command));
        }

        // POST api/<controller>
        [HasPermission("ApproveDriverFareAmount")]
        [HttpPost("ApproveDriverFareAmount")]
        public async Task<IActionResult> ApproveDriverFareAmount(ApproveLadingExitPermitFareAmountCommand command)
        {
            return Ok(await Mediator.Send(command));
        }

        // PUT api/<controller>/5
        [HasPermission("UpdateLadingExitPermit")]
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(Guid id, UpdateLadingExitPermitCommand command)
        {
            if (id != command.Id)
            {
                return BadRequest();
            }
            return Ok(await Mediator.Send(command));
        }

        // PUT api/<controller>/5
        [HasPermission("AddAttachments")]
        [HttpPut("AddAttachments")]
        public async Task<IActionResult> AddAttachments(CreateLadingExitPermitAttachment command)
        {
            return Ok(await Mediator.Send(command));
        }

        // DELETE api/<controller>/5
        [HasPermission("DeleteLadingExitPermit")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            return Ok(await Mediator
                .Send(new DeleteLadingExitPermitByIdCommand { Id = id }));
        }

        // DELETE api/<controller>/5
        [HasPermission("RevokeLadingExitPermit")]
        [HttpPut("RevokeLadingExitPermit/{id}")]
        public async Task<IActionResult> RevokeLadingExitPermit(Guid id)
        {
            return Ok(await Mediator
                .Send(new RevokeLadingExitPermitCommand { Id = id }));
        }


    }
}
