using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sepehr.Application.Features.ProductTypes.Command.CreateProductType
{
    public class CreateProductTypeCommandValidator : AbstractValidator<CreateProductTypeCommand>
    {
        public CreateProductTypeCommandValidator()
        {
            //RuleFor(x => x.ProductTypeBrandId).NotEqual(0).WithMessage("»—‰œ „Õ’Ê· „‘Œ’ ‰‘œÂ «”  !");
        }

    }
}