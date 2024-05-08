using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sepehr.Application.Features.Services.Command.CreateService
{
    public class CreateServiceCommandValidator : AbstractValidator<CreateServiceCommand>
    {
        public CreateServiceCommandValidator()
        {
            //RuleFor(x => x.ServiceBrandId).NotEqual(0).WithMessage("«” «‰œ«—œ „Õ’Ê· „‘Œ’ ‰‘œÂ «”  !");
        }

    }
}