using FluentValidation;
using Sepehr.Application.Features.Customers.Command.CreateCustomer;
using Sepehr.Application.Features.Personnels.Command.CreatePersonnel;
using Sepehr.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sepehr.Application.Features.Personnels.Command.CreatePersonnel
{
    public class CreatePersonnelCommandValidator : AbstractValidator<CreatePersonnelCommand>
    {
        public CreatePersonnelCommandValidator()
        {
            RuleFor(x => x.NationalId).Length(10).WithMessage("کد ملی معتبر نمی باشد !");
            RuleFor(x => x.NationalId).NotEmpty().WithMessage("کد ملی الزامی می باشد !");
            RuleForEach(command => command.Phonebook).SetValidator(new PhonebookValidator());
            RuleFor(x => x.NationalId).NotEmpty()
                        .When(x => !ValidationHelper.IsValidNationalCode(x.NationalId)).WithMessage("کد ملی نامعتبر می باشد !");
        }

        private class PhonebookValidator : AbstractValidator<CreatePhonebookRequest>
        {
            public PhonebookValidator()
            {
                RuleFor(x => x.PhoneNumber).Length(11)
                    .When(x => x.PhoneNumberTypeId == 1).WithMessage("شماره موبایل نامعتبر می باشد !");
            }

        }


    }
}