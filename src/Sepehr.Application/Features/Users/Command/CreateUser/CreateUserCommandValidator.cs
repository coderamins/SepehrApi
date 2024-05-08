using FluentValidation;
using Sepehr.Application.Features.ApplicationUsers.Command.CreateApplicationUser;
using Sepehr.Application.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sepehr.Application.Features.ApplicationUsers.Command.CreateApplicationUser
{
    public class CreateApplicationUserCommandValidator:AbstractValidator<CreateApplicationUserCommand>
    {
        public CreateApplicationUserCommandValidator()
        {
                RuleFor(x=>x.UserName).NotEmpty().WithMessage("نام کاربری الزامی می باشد !");
                RuleFor(x=>x.Password).NotEmpty().WithMessage("کلمه عبور الزامی می باشد !");
                RuleFor(x=>x.Password).Equal(x=>x.ConfirmPassword).WithMessage("کلمه عبور و تکرار آن یکسان نیستند !");
                RuleFor(x=>x.Password).Must(x=> new CustomPasswordValidator(8).ValidateAsync(x));
        }



    }
}