using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Sepehr.Application.Features.LadingPermits.Command.CreateLadingPermit;
using Sepehr.Application.Features.LadingPermits.Command.DeleteLadingPermitById;
using Sepehr.Application.Features.LadingPermits.Command.RevokeLadingPermit;
using Sepehr.Application.Features.LadingPermits.Command.UpdateLadingPermit;
using Sepehr.Application.Features.LadingPermits.Queries.GetAllLadingPermits;
using Sepehr.Application.Features.LadingPermits.Queries.GetLadingPermitById;
using Sepehr.Application.Helpers;

namespace Sepehr.WebApi.Controller
{
    [ApiVersion("1.0")]
    public class LadingPermitController : BaseApiController
    {

        [HttpGet]
        [HasPermission("GetAllLadingPermits")]
        public async Task<IActionResult> Get([FromQuery] GetAllLadingPermitsParameter filter)
        {
            return Ok(await Mediator
                .Send(new GetAllLadingPermitsQuery()
                {
                    PageSize = filter.PageSize,
                    PageNumber = filter.PageNumber,
                    HasExitPermit=filter.HasExitPermit,
                }));
        }

        // GET api/<controller>/5
        [HasPermission("GetLadingPermitById")]
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            return Ok(await Mediator.Send(new GetLadingPermitByIdQuery { Id = id }));
        }

        // POST api/<controller>
        [HasPermission("CreateLadingPermit")]
        [HttpPost]
        public async Task<IActionResult> Post(CreateLadingPermitCommand command)
        {
            return Ok(await Mediator.Send(command));
        }

        // PUT api/<controller>/5
        [HasPermission("UpdateLadingPermit")]
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, UpdateLadingPermitCommand command)
        {
            if (id != command.Id)
            {
                return BadRequest();
            }
            return Ok(await Mediator.Send(command));
        }

        // PUT api/<controller>/5
        [HasPermission("RevokeLadingPermit")]
        [HttpPut("RevokeLadingPermit/{id}")]
        public async Task<IActionResult> RevokeLadingPermit(int id, RevokeLadingPermitCommand command)
        {
            if (id != command.Id)
            {
                return BadRequest();
            }
            return Ok(await Mediator.Send(command));
        }

        // DELETE api/<controller>/5
        [HasPermission("DeleteLadingPermit")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            return Ok(await Mediator
                .Send(new DeleteLadingPermitByIdCommand { Id = id }));
        }

        // PUT api/<controller>/5
        //[HasPermission("LadingExitPermit")]
        //[HttpPut("LadingExitPermit/{LadingPermitId}")]
        //public async Task<IActionResult> LadingExitPermit(int LadingPermitId, CreateLadingExitPermitCommand command)
        //{
        //    if (LadingPermitId != command.LadingPermitId)
        //    {
        //        return BadRequest();
        //    }
        //    return Ok(await Mediator.Send(command));
        //}

    }
}
