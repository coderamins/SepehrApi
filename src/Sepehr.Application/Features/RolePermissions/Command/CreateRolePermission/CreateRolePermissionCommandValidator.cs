using FluentValidation;
using Sepehr.Application.Features.RolePermissions.Command.CreateRolePermission;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sepehr.Application.Features.RolePermissions.Command.CreateRolePermission
{
    public class CreateRolePermissionCommandValidator:AbstractValidator<CreateRolePermissionCommand>
    {
        public CreateRolePermissionCommandValidator()
        {
        }



    }
}