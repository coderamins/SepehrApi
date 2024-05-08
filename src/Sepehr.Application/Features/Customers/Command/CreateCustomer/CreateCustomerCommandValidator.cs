using FluentValidation;
using Sepehr.Application.Features.Customers.Command.CreateCustomer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sepehr.Application.Features.Customers.Command.CreateCustomer
{
    public class CreateCustomerCommandValidator:AbstractValidator<CreateCustomerCommand>
    {
        public CreateCustomerCommandValidator()
        {
                RuleFor(x=>x.NationalId).Length(10).WithMessage("کد ملی معتبر نمی باشد !");
                RuleFor(x=>x.NationalId).NotEmpty().WithMessage("کد ملی الزامی می باشد !");
                RuleFor(x=>x.Mobile).Length(11).WithMessage("شماره موبایل نامعتبر می باشد !");
                RuleFor(x=>x.NationalId).NotEmpty()
                        .When(x=> !ValidationHelper.IsValidNationalCode(x.NationalId)).WithMessage("کد ملی نامعتبر می باشد !");
        }

    }
}