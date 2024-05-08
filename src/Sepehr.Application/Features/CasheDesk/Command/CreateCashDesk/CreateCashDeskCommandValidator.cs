using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sepehr.Application.Features.CashDesks.Command.CreateCashDesk
{
    public class CreateCashDeskCommandValidator : AbstractValidator<CreateCashDeskCommand>
    {
        public CreateCashDeskCommandValidator()
        {
        }

    }
}