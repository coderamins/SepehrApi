using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Sepehr.Application.Features.Permissions.Command.CreatePermission;
using Sepehr.Application.Features.Permissions.Command.DeletePermissionById;
using Sepehr.Application.Features.Permissions.Command.UpdatePermission;
using Sepehr.Application.Features.Permissions.Queries.GetAllPermissions;
using Sepehr.Application.Features.Permissions.Queries.GetPermissionById;
using Sepehr.Infrastructure.Authentication;

namespace Sepehr.WebApi.Controller
{
    [ApiVersion("1.0")]
    public class PermissionController : BaseApiController
    {
        [HasPermission("GetAllPermissions")]
        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] GetAllPermissionsParameter filter)
        {
            return Ok(await Mediator
                .Send(new GetAllPermissionsQuery()
                {
                    PageSize = filter.PageSize,
                    PageNumber = filter.PageNumber
                }));
        }

        //[HasPermission("GetAllPermissionsByMenu")]
        [AllowAnonymous]
        [HttpGet("GetAllPermissionsByMenu")]
        public async Task<IActionResult> GetAllPermissionsByMenu()
        {
            return Ok(await Mediator
                .Send(new GetAllPermissionsByMenuQuery()));
        }

        // GET api/<controller>/5
        [HasPermission("GetPermissionById")]
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            return Ok(await Mediator.Send(new GetPermissionByIdQuery { Id = id }));
        }

        // POST api/<controller>
        [HasPermission("CreatePermission")]
        [HttpPost]
        public async Task<IActionResult> Post(CreatePermissionCommand command)
        {
            return Ok(await Mediator.Send(command));
        }

        // PUT api/<controller>/5
        [HasPermission("UpdatePermission")]
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(Guid id, UpdatePermissionCommand command)
        {
            if (id != command.Id)
            {
                return BadRequest();
            }
            return Ok(await Mediator.Send(command));
        }

        // DELETE api/<controller>/5
        [HasPermission("DeletePermission")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            return Ok(await Mediator
                .Send(new DeletePermissionByIdCommand { Id = id }));
        }
    }
}
