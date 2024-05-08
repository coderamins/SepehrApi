using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sepehr.Application.Features.LadingExitPermits.Command.CreateLadingExitPermit
{
    public class CreateLadingExitPermitCommandValidator : AbstractValidator<CreateLadingExitPermitCommand>
    {
        public CreateLadingExitPermitCommandValidator()
        {
        }

    }
}