using FluentValidation;
using Sepehr.Application.Features.UserRoles.Command.CreateUserRole;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sepehr.Application.Features.UserRoles.Command.CreateUserRole
{
    public class CreateUserRoleCommandValidator:AbstractValidator<CreateUserRoleCommand>
    {
        public CreateUserRoleCommandValidator()
        {
        }



    }
}