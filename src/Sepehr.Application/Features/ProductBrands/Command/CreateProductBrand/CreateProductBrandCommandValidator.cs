using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sepehr.Application.Features.ProductBrands.Command.CreateProductBrand
{
    public class CreateProductBrandCommandValidator : AbstractValidator<CreateProductBrandCommand>
    {
        public CreateProductBrandCommandValidator()
        {
            //RuleFor(x => x.ProductBrandProductBrandId).NotEqual(0).WithMessage("»—‰œ „Õ’Ê· „‘Œ’ ‰‘œÂ «”  !");
        }

    }
}