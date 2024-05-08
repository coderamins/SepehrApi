using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sepehr.Application.Features.ShareHolders.Command.CreateShareHolder
{
    public class CreateShareHolderCommandValidator : AbstractValidator<CreateShareHolderCommand>
    {
        public CreateShareHolderCommandValidator()
        {
        }

    }
}