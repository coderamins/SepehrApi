using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sepehr.Application.Features.Products.Command.CreateProduct
{
    public class CreateProductCommandValidator : AbstractValidator<CreateProductCommand>
    {
        public CreateProductCommandValidator()
        {
            RuleFor(x => x.ProductMainUnitId).NotEmpty().WithMessage("واحد اصلی الزامی می باشد !");
            RuleFor(x => x.ProductName).NotEmpty().WithMessage("نام محصول الزامی می باشد !");
            RuleFor(x => x.ApproximateWeight).NotNull().NotEqual(0).WithMessage("وزن الزامی می باشد !");
            RuleFor(x => x.ProductSize).NotEmpty().WithMessage("سایز الزامی می باشد !");
        }

    }
}