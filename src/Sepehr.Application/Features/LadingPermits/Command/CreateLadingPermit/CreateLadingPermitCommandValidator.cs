using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sepehr.Application.Features.LadingPermits.Command.CreateLadingPermit
{
    public class CreateLadingPermitCommandValidator : AbstractValidator<CreateLadingPermitCommand>
    {
        public CreateLadingPermitCommandValidator()
        {
            //RuleFor(x => x.LadingPermitBrandId).NotEqual(0).WithMessage("»—‰œ „Õ’Ê· „‘Œ’ ‰‘œÂ «”  !");
        }

    }
}