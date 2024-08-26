using FluentValidation;
using Microsoft.AspNetCore.Http;
using Sepehr.Application.DTOs.Product;

namespace Sepehr.Application.Features.TransferRemittances.Command.CreateTransferRemittance
{
    public class CreateTransferRemittanceCommandValidator : AbstractValidator<CreateTransferRemittanceCommand>
    {
        public CreateTransferRemittanceCommandValidator()
        {
            RuleFor(c => c.PurchaseOrderId).NotNull().NotEmpty().WithMessage("شماره سفارش الزامی می باشد !");
            RuleFor(x => x.OriginWarehouseId)
                .NotEqual(x => x.DestinationWarehouseId)
                .WithMessage("انبار مبدا و مقصد نمی توانند یکسان باشند !");

            RuleFor(x => x.Details.Count()).GreaterThan(0).WithMessage("انتخاب حداقل یک کالا جهت انتقال الزامی می باشد !");
            RuleForEach(x => x.Details).SetValidator(new TransferRemittanceDetailValidator());
        }

        private class TransferRemittanceDetailValidator : AbstractValidator<TransferRemittanceDetailDto>
        {
            public TransferRemittanceDetailValidator()
            {
                RuleFor(x => x.TransferAmount).GreaterThan(0).WithMessage("مقدار انتقال باید بزرگتر از صفر باشد !");
            }
        }

        private class FileValidator:AbstractValidator<IFormFile>
        {
            public FileValidator()
            {
                RuleFor(x => x.Length).LessThanOrEqualTo(1000000).WithMessage("حجم فایل از اندازه مجاز می باشد !");
                RuleFor(x => x.ContentType).Must(x => x.Equals("image/jpeg") ||
                x.Equals("image/jpg") ||
                x.Equals("image/png") ||
                x.Equals("image/pdf") ||
                x.Equals("application/pdf"))
                    .WithMessage("قرمت فایل پیوست نامعتبر می باشد !");
            }
        }

    }
}