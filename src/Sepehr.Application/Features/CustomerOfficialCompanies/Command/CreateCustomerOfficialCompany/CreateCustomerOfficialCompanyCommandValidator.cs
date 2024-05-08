using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sepehr.Application.Features.CustomerOfficialCompanys.Command.CreateCustomerOfficialCompany
{
    public class CreateCustomerOfficialCompanyCommandValidator : AbstractValidator<CreateCustomerOfficialCompanyCommand>
    {
        public CreateCustomerOfficialCompanyCommandValidator()
        {
            //RuleFor(x => x.CustomerOfficialCompanyBrandId).NotEqual(0).WithMessage("»—‰œ „Õ’Ê· „‘Œ’ ‰‘œÂ «”  !");
        }

    }
}