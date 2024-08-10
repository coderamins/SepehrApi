using FluentValidation;
using Microsoft.AspNetCore.Http;
using Sepehr.Application.Features.TransferRemittances.Command.TransferRemittanceUnloadingPermit;

namespace Sepehr.Application.DTOs.TransferRemittanceUnloadingPermit
{
    public class CreateUnloadingPermitCommandValidator : AbstractValidator<CreateUnloadingPermitCommand>
    {
        public CreateUnloadingPermitCommandValidator()
        {
            RuleFor(x => x.DriverMobile).Length(11).WithMessage("شماره موبایل نامعتبر می باشد !");
            //RuleForEach(x => x.Attachments).SetValidator(new FileValidator());
            RuleForEach(x => x.UnloadingPermitDetails)
                .SetValidator(new TransferRemittanceUnloadingPermitDetailValidator());
        }

        private class TransferRemittanceUnloadingPermitDetailValidator:
            AbstractValidator<UnloadingPermitDetailDto>
        {
            public TransferRemittanceUnloadingPermitDetailValidator()
            {
                RuleFor(x => x.UnloadedAmount).GreaterThan(0).WithMessage("مقدار تخلیه باید بزرگتر از صفر باشد !");
            }
        }

        private class FileValidator : AbstractValidator<IFormFile>
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
