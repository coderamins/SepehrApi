using FluentValidation;
using Sepehr.Application.Features.RolePermissions.Command.CreateRolePermission;
using Sepehr.Application.Features.RolePermissions.Command.UpdateRolePermission;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sepehr.Application.Features.RolePermissions.Command.UpdateRolePermission
{
    public class UpdateRolePermissionCommandValidator: AbstractValidator<UpdateRolePermissionCommand>
    {
        public UpdateRolePermissionCommandValidator()
        {
        }

    }
}