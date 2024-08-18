using FluentValidation;
using Sepehr.Domain.Enums;

namespace Sepehr.Application.Features.EntrancePermits.Command.CreateEntrancePermit
{
    public class CreateEntrancePermitCommandValidator:AbstractValidator<CreateEntrancePermitCommand>
    {
        public CreateEntrancePermitCommandValidator()
        {

        }

    }
}