using Sepehr.Application.DTOs.RoleMenu;
using Sepehr.Application.Wrappers;
using Sepehr.Domain.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sepehr.Application.Interfaces.Repositories
{
    public interface IRoleMenuRepository
    {
        Task<Response<bool>> AddRoleMenu(AddRoleMenuRequest request);
        Task<Response<bool>> DeleteRoleMenu(Guid id);
        Task<Response<List<ApplicationMenuViewModel>>> GetAllApplicationMenus();
        Task<Response<IEnumerable<RoleMenuViewModel>>> GetAllRoleMenus(Guid roleId);
        Task<Response<IEnumerable<ApplicationMenuViewModel>>> GetUserApplicationMenus();
    }
}
