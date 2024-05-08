using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sepehr.Application.Features.ProductStandards.Command.CreateProductStandard
{
    public class CreateProductStandardCommandValidator : AbstractValidator<CreateProductStandardCommand>
    {
        public CreateProductStandardCommandValidator()
        {
            //RuleFor(x => x.ProductStandardBrandId).NotEqual(0).WithMessage("«” «‰œ«—œ „Õ’Ê· „‘Œ’ ‰‘œÂ «”  !");
        }

    }
}