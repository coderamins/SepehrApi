using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sepehr.Application.Features.ProductStates.Command.CreateProductState
{
    public class CreateProductStateCommandValidator : AbstractValidator<CreateProductStateCommand>
    {
        public CreateProductStateCommandValidator()
        {
            //RuleFor(x => x.ProductStateBrandId).NotEqual(0).WithMessage("»—‰œ „Õ’Ê· „‘Œ’ ‰‘œÂ «”  !");
        }

    }
}