using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sepehr.Application.Features.CustomerLabels.Command.CreateCustomerLabel
{
    public class CreateCustomerLabelCommandValidator : AbstractValidator<CreateCustomerLabelCommand>
    {
        public CreateCustomerLabelCommandValidator()
        {
            //RuleFor(x => x.CustomerLabelCustomerLabelId).NotEqual(0).WithMessage("»—‰œ „Õ’Ê· „‘Œ’ ‰‘œÂ «”  !");
        }

    }
}