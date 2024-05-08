using FluentValidation;
using Sepehr.Application.Features.Customers.Command.CreateCustomer;
using Sepehr.Application.Features.Customers.Command.UpdateCustomer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sepehr.Application.Features.Customers.Command.UpdateCustomer
{
    public class UpdateCustomerCommandValidator: AbstractValidator<UpdateCustomerCommand>
    {
        public UpdateCustomerCommandValidator()
        {
                RuleFor(x=>x.NationalId).NotEmpty().WithMessage("کد ملی الزامی می باشد !");
                RuleFor(x=>x.NationalId).NotEmpty()
                        .When(x=> !ValidationHelper.IsValidNationalCode(x.NationalId)).WithMessage("کد ملی نامعتبر می باشد !");
        }

    }
}