using Microsoft.AspNetCore.Mvc;
using Sepehr.Application.Features.ApplicationRoles.Command.CreateApplicationRole;
using Sepehr.Application.Features.ApplicationRoles.Command.DeleteApplicationRoleById;
using Sepehr.Application.Features.ApplicationRoles.Command.UpdateApplicationRole;
using Sepehr.Application.Features.ApplicationRoles.Queries.GetAllApplicationRoles;
using Sepehr.Application.Features.ApplicationRoles.Queries.GetApplicationRoleById;
using Sepehr.Application.Helpers;

namespace Sepehr.WebApi.Controller
{
    [ApiVersion("1.0")]
    public class ApplicationRoleController : BaseApiController
    {
        [HasPermission("GetAllApplicationRoles", Roles = "Admin")]
        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] GetAllApplicationRolesParameter filter)
        {
            return Ok(await Mediator
                .Send(new GetAllApplicationRolesQuery()
                {
                    PageSize = filter.PageSize,
                    PageNumber = filter.PageNumber
                }));
        }

        // GET api/<controller>/5
        [HasPermission("GetApplicationRoleById",Roles ="Admin")]
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(string id)
        {
            return Ok(await Mediator.Send(new GetApplicationRoleByIdQuery { Id = id }));
        }

        // POST api/<controller>
        [HasPermission("CreateApplicationRole")]
        [HttpPost]
        public async Task<IActionResult> Post(CreateApplicationRoleCommand command)
        {
            return Ok(await Mediator.Send(command));
        }

        // PUT api/<controller>/5
        [HasPermission("UpdateApplicationRole")]
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(Guid id, UpdateApplicationRoleCommand command)
        {
            if (id != command.Id)
            {
                return BadRequest();
            }
            return Ok(await Mediator.Send(command));
        }

        // DELETE api/<controller>/5
        [HasPermission("DeleteApplicationRole")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            return Ok(await Mediator
                .Send(new DeleteApplicationRoleByIdCommand { Id = id }));
        }
    }
}
