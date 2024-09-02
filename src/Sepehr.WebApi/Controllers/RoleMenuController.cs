using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Sepehr.Application.DTOs.RoleMenu;
using Sepehr.Application.Interfaces;
using Sepehr.Application.Helpers;

namespace Sepehr.WebApi.Controller
{
    [ApiVersion("1.0")]
    public class RoleMenuController : BaseApiController
    {
        private readonly IRoleMenuService _roleMenuService;
        public RoleMenuController(IRoleMenuService RoleMenuService)
        {
            _roleMenuService = RoleMenuService;
        }

        // POST api/<controller>
        [HasPermission("CreateRoleMenu")]
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] AddRoleMenuRequest request)
        {
            return Ok(await _roleMenuService.AddRoleMenu(request));
        }

        [HasPermission("GetRoleMenuById")]
        [HttpGet]
        public async Task<IActionResult> Get(Guid roleId)
        {
            return Ok(await _roleMenuService.GetAllRoleMenus(roleId));
        }

        [HasPermission("GetUserApplicationMenus")]
        [HttpGet("GetApplicationMenus")]
        public async Task<IActionResult> GetApplicationMenus()
        {
            return Ok(await _roleMenuService.GetUserApplicationMenus());
        }

        [HasPermission("GetAllApplicationMenus")]
        [HttpGet("GetAllApplicationMenus")]
        public async Task<IActionResult> GetAllApplicationMenus()
        {
            return Ok(await _roleMenuService.GetAllApplicationMenus());
        }

        [HasPermission("DeleteApplicationMenu")]
        [HttpDelete("{Id}")]
        public async Task<IActionResult> Delete(Guid Id)
        {
            return Ok(await _roleMenuService.DeleteRoleMenu(Id));
        }


    }
}
