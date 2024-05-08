using FluentValidation;
using Sepehr.Application.DTOs.Order;
using Sepehr.Application.Features.Orders.Command.UpdateOrder;
using Sepehr.Domain.Enums;

namespace Sepehr.Application.Features.Orders.Command.UpdateOrder
{
    public class UpdateOrderCommandValidator:AbstractValidator<UpdateOrderCommand>
    {
        public UpdateOrderCommandValidator()
        {
            RuleForEach(x => x.Details).SetValidator(new OrderDetailValidator());
            //RuleFor(x => x.CarPlaque).NotEmpty().WithMessage("شماره پلاک راننده الزامی می باشد !");
            //RuleFor(x => x.OrderExitTypeId).NotEmpty().WithMessage("نوع خروج صحیح نمی باشد !").When(x=> Enum.IsDefined((ExitType)x.ExitType));
        }

        public class OrderDetailValidator:AbstractValidator<OrderDetailRequest>
        {
            public OrderDetailValidator()
            {
                //RuleFor(x => x.CargoSendDate).NotNull().Matches("^\\d{4}\\/\\d{1,2}\\/\\d{1,2}$").WithMessage("تاریخ ارسال بار نامعتبر می باشد !!");

                //RuleFor(x => x.PurchaseInvoiceTypeId).NotNull().NotEqual(0).WithMessage("فاکتور خرید مشخص نشده است !").When(x => x.WarehouseId != 2);
                //RuleFor(x => x.PurchaseSettlementDate).NotNull().Matches("^\\d{4}\\/\\d{1,2}\\/\\d{1,2}$")
                //    .WithMessage("تاریخ تسویه خرید نامعتبر می باشد !").When(x => x.WarehouseId != 2);
            }
        }
    }
}