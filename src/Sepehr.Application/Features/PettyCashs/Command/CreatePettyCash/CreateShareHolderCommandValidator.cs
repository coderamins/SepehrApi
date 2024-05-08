using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sepehr.Application.Features.PettyCashs.Command.CreatePettyCash
{
    public class CreatePettyCashCommandValidator : AbstractValidator<CreatePettyCashCommand>
    {
        public CreatePettyCashCommandValidator()
        {
        }

    }
}