using FluentValidation;
using Sepehr.Application.DTOs.Order;
using Sepehr.Application.DTOs.PurchaseOrder;
using Sepehr.Domain.Entities;
using Sepehr.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sepehr.Application.Features.PurchaseOrders.Command.CreatePurchaseOrder
{
    public class CreatePurchaseOrderCommandValidator:AbstractValidator<CreatePurchaseOrderCommand>
    {
        public CreatePurchaseOrderCommandValidator()
        {
            RuleForEach(x => x.OrderPayments).SetValidator(new PurchaseOrderPaymentsValidator());
            RuleFor(x => x.OrderPayments.Sum(p => p.Amount)).Equal(x => x.TotalAmount)
                .WithMessage("مبلغ سفارش باید با جمع مبالغ تسویه برابر باشد !");

            RuleForEach(x => x.Details).SetValidator(new PurchaseOrderDetailValidator());
            //RuleFor(x => x.CarPlaque).NotEmpty().WithMessage("شماره پلاک راننده الزامی می باشد !");
            RuleFor(x => x.ExitType).NotEmpty().WithMessage("نوع خروج صحیح نمی باشد !").When(x => Enum.IsDefined((ExitType)x.ExitType));
        }

        public class PurchaseOrderDetailValidator:AbstractValidator<CreatePurchaseOrderDetailRequest>
        {
            public PurchaseOrderDetailValidator()
            {
                //RuleFor(x => x.CargoSendDate).NotNull().Matches("^\\d{4}\\/\\d{1,2}\\/\\d{1,2}$").WithMessage("تاریخ ارسال بار نامعتبر می باشد !!");
                //RuleFor(x => x.PurchaseInvoiceTypeId).NotNull().NotEqual(0).WithMessage("فاکتور خرید مشخص نشده است !").When(x => x.WarehouseId != 2);
                //RuleFor(x => x.PurchaseSettlementDate).NotNull().Matches("^\\d{4}\\/\\d{1,2}\\/\\d{1,2}$")
                //    .WithMessage("تاریخ تسویه خرید نامعتبر می باشد !").When(x => x.WarehouseId != 2);
            }
        }


        public class PurchaseOrderPaymentsValidator : AbstractValidator<PurchaseOrderPaymentDto>
        {
            public PurchaseOrderPaymentsValidator()
            {
                //RuleFor(x => x.).NotNull().Matches("^\\d{4}\\/\\d{1,2}\\/\\d{1,2}$").WithMessage("تاریخ ارسال بار نامعتبر می باشد !!");
            }
        }

    }
}