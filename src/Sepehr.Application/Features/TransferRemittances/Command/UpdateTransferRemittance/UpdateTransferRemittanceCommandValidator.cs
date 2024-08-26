using FluentValidation;
using Sepehr.Application.DTOs.Product;

namespace Sepehr.Application.Features.TransferRemittances.Command.UpdateTransferRemittance
{
    public class UpdateTransferRemittanceValidator : AbstractValidator<UpdateTransferRemittanceCommand>
    {
        public UpdateTransferRemittanceValidator()
        {
            RuleFor(x => x.OriginWarehouseId)
                .NotEqual(x => x.DestinationWarehouseId)
                .WithMessage("انبار مبدا و مقصد نمی توانند یکسان باشند !");

            RuleFor(x => x.Details.Count()).GreaterThan(0).WithMessage("انتخاب حداقل یک کالا جهت انتقال الزامی می باشد !");
        }

        public class TransferRemittanceDetailValidator : AbstractValidator<TransferRemittanceDetailDto>
        {
            public TransferRemittanceDetailValidator()
            {
                RuleFor(x => x.TransferAmount).GreaterThan(0).WithMessage("مقدار انتقال باید بزرگتر از صفر باشد !");
            }
        }

    }
}