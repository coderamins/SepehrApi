using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Sepehr.Application.DTOs.UserRoles;
using Sepehr.Application.Wrappers;
using Sepehr.Domain.Entities;
using Sepehr.Domain.ViewModels;
using Sepehr.Application.DTOs.Account;
using Sepehr.Application.Wrappers;

namespace Sepehr.Application.Interfaces
{
    public interface IUserRoleService
    {
        Task<Response<bool>> AddUserRole(AddUserRoleRequest request);
        Task<Response<bool>> DeleteUserRole(AddUserRoleRequest request);
        Task<Response<IEnumerable<UserRoleViewModel>>> GetAllUserRoles();
    }
}