using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sepehr.Application.Features.Products.Command.UpdateProduct
{
    public class UpdateProductCommandValidator : AbstractValidator<UpdateProductCommand>
    {
        public UpdateProductCommandValidator()
        {
            RuleFor(x => x.ApproximateWeight).NotNull().NotEqual(0).WithMessage("وزن الزامی می باشد !");
            RuleFor(x => x.ProductSize).NotEmpty().WithMessage("سایز الزامی می باشد !");
        }

    }
}