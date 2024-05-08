using FluentValidation;
using Sepehr.Application.Features.ApplicationRoles.Command.CreateApplicationRole;
using Sepehr.Application.Features.ApplicationRoles.Command.UpdateApplicationRole;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sepehr.Application.Features.ApplicationRoles.Command.UpdateApplicationRole
{
    public class UpdateApplicationRoleCommandValidator: AbstractValidator<UpdateApplicationRoleCommand>
    {
        public UpdateApplicationRoleCommandValidator()
        {
        }

    }
}