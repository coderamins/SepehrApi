using FluentValidation;
using Sepehr.Application.DTOs.PurchaseOrder;
using Sepehr.Application.Features.PurchaseOrders.Command.UpdatePurchaseOrder;
using Sepehr.Domain.Enums;

namespace Sepehr.Application.Features.PurchaseOrders.Command.UpdatePurchaseOrder
{
    public class UpdatePurchaseOrderCommandValidator:AbstractValidator<UpdatePurchaseOrderCommand>
    {
        public UpdatePurchaseOrderCommandValidator()
        {
            RuleForEach(x => x.Details).SetValidator(new PurchaseOrderDetailValidator());
            //RuleFor(x => x.CarPlaque).NotEmpty().WithMessage("شماره پلاک راننده الزامی می باشد !");
            RuleFor(x => x.ExitType).NotEmpty().WithMessage("نوع خروج صحیح نمی باشد !").When(x=> Enum.IsDefined((ExitType)x.ExitType));
        }

        public class PurchaseOrderDetailValidator:AbstractValidator<UpdatePurchaseOrderDetailRequest>
        {
            public PurchaseOrderDetailValidator()
            {
                //RuleFor(x => x.CargoSendDate).NotNull().Matches("^\\d{4}\\/\\d{1,2}\\/\\d{1,2}$").WithMessage("تاریخ ارسال بار نامعتبر می باشد !!");

                //RuleFor(x => x.PurchaseInvoiceTypeId).NotNull().NotEqual(0).WithMessage("فاکتور خرید مشخص نشده است !").When(x => x.WarehouseId != 2);
                //RuleFor(x => x.PurchaseSettlementDate).NotNull().Matches("^\\d{4}\\/\\d{1,2}\\/\\d{1,2}$")
                //    .WithMessage("تاریخ تسویه خرید نامعتبر می باشد !").When(x => x.WarehouseId != 2);
            }
        }
    }
}