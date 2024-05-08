using FluentValidation;
using Sepehr.Application.Features.Permissions.Command.CreatePermission;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sepehr.Application.Features.Permissions.Command.CreatePermission
{
    public class CreatePermissionCommandValidator:AbstractValidator<CreatePermissionCommand>
    {
        public CreatePermissionCommandValidator()
        {
            RuleFor(p=>p.Name).NotEmpty().WithMessage("نام لاتین نمیتواند خالی باشد !");
        }



    }
}