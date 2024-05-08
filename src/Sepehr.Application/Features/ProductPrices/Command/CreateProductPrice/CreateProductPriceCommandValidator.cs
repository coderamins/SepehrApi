using FluentValidation;

namespace Sepehr.Application.Features.ProductPrices.Command.CreateProductPrice
{
    public class CreateProductInventoryCommandValidator : AbstractValidator<CreateProductPriceCommand>
    {
        public CreateProductInventoryCommandValidator()
        {
            //RuleFor(x => x.ProductPriceBrandId).NotEqual(0).WithMessage("»—‰œ „Õ’Ê· „‘Œ’ ‰‘œÂ «”  !");
        }

    }
}