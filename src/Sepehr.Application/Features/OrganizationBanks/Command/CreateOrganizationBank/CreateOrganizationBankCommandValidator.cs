using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sepehr.Application.Features.OrganizationBanks.Command.CreateOrganizationBank
{
    public class CreateOrganizationBankCommandValidator : AbstractValidator<CreateOrganizationBankCommand>
    {
        public CreateOrganizationBankCommandValidator()
        {
            //RuleFor(x => x.OrganizationBankOrganizationBankId).NotEqual(0).WithMessage("»—‰œ „Õ’Ê· „‘Œ’ ‰‘œÂ «”  !");
        }

    }
}