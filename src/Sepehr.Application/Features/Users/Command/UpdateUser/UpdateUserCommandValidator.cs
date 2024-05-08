using FluentValidation;
using Sepehr.Application.Features.ApplicationUsers.Command.CreateApplicationUser;
using Sepehr.Application.Features.ApplicationUsers.Command.UpdateApplicationUser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static Sepehr.Application.Features.ValidationHelper;

namespace Sepehr.Application.Features.ApplicationUsers.Command.UpdateApplicationUser
{
    public class UpdateApplicationUserCommandValidator : AbstractValidator<UpdateApplicationUserCommand>
    {
        public UpdateApplicationUserCommandValidator()
        {
            RuleFor(x => x.UserName).NotEmpty().WithMessage("نام کاربری الزامی می باشد !");
        }
    }
}