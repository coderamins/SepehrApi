using FluentValidation;
using Sepehr.Application.Features.ApplicationRoles.Command.CreateApplicationRole;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sepehr.Application.Features.ApplicationRoles.Command.CreateApplicationRole
{
    public class CreateApplicationRoleCommandValidator:AbstractValidator<CreateApplicationRoleCommand>
    {
        public CreateApplicationRoleCommandValidator()
        {
        }



    }
}