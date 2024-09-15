using FluentValidation;

namespace Sepehr.Application.Features.DraftOrders.Command.CreateDraftOrder
{
    public class CreateProductInventoryCommandValidator : AbstractValidator<CreateDraftOrderCommand>
    {
        public CreateProductInventoryCommandValidator()
        {
            //RuleFor(x => x.DraftOrderBrandId).NotEqual(0).WithMessage("»—‰œ „Õ’Ê· „‘Œ’ ‰‘œÂ «”  !");
        }

    }
}