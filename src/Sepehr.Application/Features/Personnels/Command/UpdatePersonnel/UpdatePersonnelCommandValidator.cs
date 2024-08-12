using FluentValidation;
using Sepehr.Application.Features.Personnels.Command.CreatePersonnel;
using Sepehr.Application.Features.Personnels.Command.UpdatePersonnel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sepehr.Application.Features.Personnels.Command.UpdatePersonnel
{
    public class UpdatePersonnelCommandValidator: AbstractValidator<UpdatePersonnelCommand>
    {
        public UpdatePersonnelCommandValidator()
        {
                RuleFor(x=>x.NationalId).NotEmpty().WithMessage("کد ملی الزامی می باشد !");
                RuleFor(x=>x.NationalId).NotEmpty()
                        .When(x=> !ValidationHelper.IsValidNationalCode(x.NationalId)).WithMessage("کد ملی نامعتبر می باشد !");
        }

    }
}