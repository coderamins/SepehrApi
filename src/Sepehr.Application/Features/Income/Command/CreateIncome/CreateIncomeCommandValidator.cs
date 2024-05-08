using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sepehr.Application.Features.Incomes.Command.CreateIncome
{
    public class CreateIncomeCommandValidator : AbstractValidator<CreateIncomeCommand>
    {
        public CreateIncomeCommandValidator()
        {
            //RuleFor(x => x.IncomeIncomeId).NotEqual(0).WithMessage("»—‰œ „Õ’Ê· „‘Œ’ ‰‘œÂ «”  !");
        }

    }
}