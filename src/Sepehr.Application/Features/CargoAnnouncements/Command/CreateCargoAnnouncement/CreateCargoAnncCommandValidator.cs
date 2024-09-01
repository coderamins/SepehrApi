using FluentValidation;

namespace Sepehr.Application.Features.CargoAnnouncements.Command.CreateCargoAnnouncement
{
    public class CreateCargoAnncCommandValidator : AbstractValidator<CreateCargoAnncCommand>
    {
        public CreateCargoAnncCommandValidator()
        {
            RuleFor(m => m)
                .Must(m => !(string.IsNullOrEmpty(m.CarPlaque) && string.IsNullOrEmpty(m.DriverName) && string.IsNullOrEmpty(m.ShippingName)))
                .WithMessage("یکی از موارد نام راننده یا نام باربری یا پلاک خودرو الزامی می باشد !");

            RuleFor(m => m.DriverMobile).Matches(@"^[0۰][9۹][۰-۹0-9]{9}$").WithMessage("فرمت شماره موبایل نامعتبر می باشد ( فرمت قابل قبول 09XXXXXXXXX) !").When(x => !string.IsNullOrEmpty(x.DriverMobile));
            RuleFor(m => m.DeliveryDate).Matches(@"^$|^([1۱][۰-۹ 0-9]{3}[/\/]([0 ۰][۱-۶ 1-6])[/\/]([0 ۰][۱-۹ 1-9]|[۱۲12][۰-۹ 0-9]|[3۳][01۰۱])|[1۱][۰-۹ 0-9]{3}[/\/]([۰0][۷-۹ 7-9]|[1۱][۰۱۲012])[/\/]([۰0][1-9 ۱-۹]|[12۱۲][0-9 ۰-۹]|(30|۳۰)))$")
                .WithMessage("تاریخ تحویل نامعتبر می باشد !");
        }
    }
}