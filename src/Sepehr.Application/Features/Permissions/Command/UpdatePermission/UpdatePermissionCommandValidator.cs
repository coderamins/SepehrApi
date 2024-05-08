using FluentValidation;
using Sepehr.Application.Features.Permissions.Command.CreatePermission;
using Sepehr.Application.Features.Permissions.Command.UpdatePermission;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sepehr.Application.Features.Permissions.Command.UpdatePermission
{
    public class UpdatePermissionCommandValidator: AbstractValidator<UpdatePermissionCommand>
    {
        public UpdatePermissionCommandValidator()
        {
            RuleFor(p => p.Name).NotEmpty().WithMessage("نام لاتین نمیتواند خالی باشد !");
        }

    }
}