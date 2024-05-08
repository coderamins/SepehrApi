using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sepehr.Application.Features.Costs.Command.CreateCost
{
    public class CreateCostCommandValidator : AbstractValidator<CreateCostCommand>
    {
        public CreateCostCommandValidator()
        {
            //RuleFor(x => x.CostCostId).NotEqual(0).WithMessage("»—‰œ „Õ’Ê· „‘Œ’ ‰‘œÂ «”  !");
        }

    }
}