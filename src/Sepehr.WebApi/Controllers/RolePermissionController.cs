using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Sepehr.Application.Features.RolePermissions.Command.CreateRolePermission;
using Sepehr.Application.Features.RolePermissions.Command.DeleteRolePermissionById;
using Sepehr.Application.Features.RolePermissions.Command.UpdateRolePermission;
using Sepehr.Application.Features.RolePermissions.Queries.GetAllRolePermissions;
using Sepehr.Application.Features.RolePermissions.Queries.GetRolePermissionById;
using Sepehr.Application.Features.Products.Command.CreateProduct;
using Sepehr.Application.Features.Products.Command.DeleteProductById;
using Sepehr.Application.Features.Products.Command.UpdateProduct;
using Sepehr.Application.Features.Products.Queries.GetAllProducts;
using Sepehr.Application.Features.Products.Queries.GetProductById;
using Serilog;
using Sepehr.Infrastructure.Authentication;

namespace Sepehr.WebApi.Controller
{
    [ApiVersion("1.0")]
    public class RolePermissionController : BaseApiController
    {
        [HasPermission("GetRolePermissions")]
        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] GetAllRolePermissionsParameter filter)
        {
            return Ok(await Mediator
                .Send(new GetAllRolePermissionsQuery()
                {
                    PageSize = filter.PageSize,
                    PageNumber = filter.PageNumber
                }));
        }

        // GET api/<controller>/5
        [HasPermission("GetRolePermissionById")]
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            return Ok(await Mediator.Send(new GetRolePermissionByIdQuery { Id = id }));
        }

        // POST api/<controller>
        [HasPermission("CreateRolePermission")]
        [HttpPost]
        public async Task<IActionResult> Post(CreateRolePermissionCommand command)
        {
            return Ok(await Mediator.Send(command));
        }

        // PUT api/<controller>/5
        [HasPermission("UpdateRolePermission")]
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(string id, UpdateRolePermissionCommand command)
        {
            if (id != command.Id)
            {
                return BadRequest();
            }
            return Ok(await Mediator.Send(command));
        }

        // DELETE api/<controller>/5
        [HasPermission("DeleteRolePermission")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            return Ok(await Mediator
                .Send(new DeleteRolePermissionByIdCommand { Id = id }));
        }
    }
}
