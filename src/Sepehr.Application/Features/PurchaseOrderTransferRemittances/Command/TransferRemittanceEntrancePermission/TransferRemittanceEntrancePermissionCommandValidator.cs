using FluentValidation;
using Sepehr.Application.Features.TransferRemittances.Command.TransferRemittanceUnloadingPermit;
using Sepehr.Application.Features.TransferRemittances.Command.TransferRemittanceEntrancePermission;

namespace Sepehr.Application.DTOs.TransferRemittanceUnloadingPermit
{
    public class TransferRemittanceEntrancePermissionCommandValidator : AbstractValidator<TransferRemittanceEntrancePermissionCommand>
    {
        public TransferRemittanceEntrancePermissionCommandValidator()
        {
            //RuleForEach(x => x.Attachments).SetValidator(new FileValidator());
        }

        private class TransferRemittanceUnloadingPermitDetailValidator:
            AbstractValidator<PurOrderTransRemittUnloadingPermitDetailDto>
        {
            public TransferRemittanceUnloadingPermitDetailValidator()
            {
                RuleFor(x => x.UnloadedAmount).GreaterThan(0).WithMessage("مقدار تخلیه باید بزرگتر از صفر باشد !");
            }
        }

        //private class FileValidator : AbstractValidator<string>
        //{
        //    private Stream _stream;
        //    public FileValidator()
        //    {
                
        //        RuleFor(x => Convert.FromBase64String(x)).
        //        RuleFor(x => x.ContentType).Must(x => x.Equals("image/jpeg") ||
        //        x.Equals("image/jpg") ||
        //        x.Equals("image/png") ||
        //        x.Equals("image/pdf") ||
        //        x.Equals("image/pdf"))
        //            .WithMessage("قرمت فایل پیوست نامعتبر می باشد !");
        //    }
        //}

        //private class FileValidator : AbstractValidator<IFormFile>
        //{
        //    public FileValidator()
        //    {
        //        RuleFor(x => x.Length).GreaterThan(1000).WithMessage("حجم فایل از اندازه مجاز می باشد !");
        //        RuleFor(x => x.ContentType).Must(x => x.Equals("image/jpeg") ||
        //        x.Equals("image/jpg") ||
        //        x.Equals("image/png") ||
        //        x.Equals("image/pdf") ||
        //        x.Equals("image/pdf"))
        //            .WithMessage("قرمت فایل پیوست نامعتبر می باشد !");
        //    }
        //}

    }
}
