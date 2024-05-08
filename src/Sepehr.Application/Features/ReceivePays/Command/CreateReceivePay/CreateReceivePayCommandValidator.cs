using FluentValidation;
using Microsoft.AspNetCore.Http;
using Sepehr.Application.Features.ReceivePays.Command.CreateReceivePay;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sepehr.Application.Features.ReceivePays.Command.CreateReceivePay
{
    public class CreateReceivePayCommandValidator : AbstractValidator<CreateReceivePayCommand>
    {
        public CreateReceivePayCommandValidator()
        {
            RuleForEach(x => x.Attachments).SetValidator(new FileValidator());
            RuleFor(x => x.ReceiveFromCompanyId).Null().When(x => x.ReceivePaymentTypeFromId != 1).WithMessage("فیلد 'دریافت از شرکت' نامعتبر می باشد !");
            RuleFor(x => x.PayToCompanyId).Null().When(x => x.ReceivePaymentTypeToId != 1).WithMessage("فیلد 'پرداخت به شرکت' نامعتبر می باشد !");
            //RuleFor(x => x.ReceivePaymentTypeToId).);  
        }

        private class FileValidator : AbstractValidator<IFormFile>
        {
            public FileValidator()
            {
                RuleFor(x => x.Length).NotNull().LessThanOrEqualTo(1000000)
                    .WithMessage("اندازه فایل بیشتر از اندازه معمولی می باشد !");

                RuleFor(x => x.ContentType).NotNull()
                    .Must(x => x.Equals("image/pdf") || x.Equals("application/pdf") || x.Equals("image/jpeg") || x.Equals("image/jpg") || x.Equals("image/png"))
                    .WithMessage("فرمت فایل اشتباه می باشد ! ");

            }
        }

    }
}