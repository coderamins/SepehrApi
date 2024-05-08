using FluentValidation;
using Sepehr.Application.Features.CargoAnnouncements.Command.UpdateCargoAnnouncement;
using Sepehr.Domain.Enums;

namespace Sepehr.Application.Features.CargoAnncs.Command.UpdateCargoAnnouncement
{
    public class UpdateCargoAnncCommandValidator:AbstractValidator<UpdateCargoAnncCommand>
    {
        public UpdateCargoAnncCommandValidator()
        {
        }

    }
}