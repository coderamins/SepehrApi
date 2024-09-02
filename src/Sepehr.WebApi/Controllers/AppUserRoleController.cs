using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Sepehr.Application.Features.UserRoles.Command.CreateUserRole;
using Sepehr.Application.Features.UserRoles.Command.DeleteUserRoleById;
using Sepehr.Application.Features.UserRoles.Queries.GetAllUserRoles;
using Sepehr.Application.Helpers;

namespace Sepehr.WebApi.Controller
{
    [ApiVersion("1.0")]
    public class UserRoleController : BaseApiController
    {
        //[HasPermission("GetAllUserRoles")]
        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] GetAllUserRolesParameter filter)
        {
            return Ok(await Mediator
                .Send(new GetAllUserRolesQuery()
                {
                    UserId = filter.UserId
                }));
        }

        // POST api/<controller>
        //[HasPermission("CreateUserRole")]
        [HttpPost]
        public async Task<IActionResult> Post(CreateUserRoleCommand command)
        {
            return Ok(await Mediator.Send(command));
        }

        // DELETE api/<controller>/5
        //[HasPermission("DeleteUserRole")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid userId,Guid roleId)
        {
            return Ok(await Mediator
                .Send(new DeleteUserRoleByIdCommand { UserId = userId,RoleId=roleId }));
        }
    }
}
