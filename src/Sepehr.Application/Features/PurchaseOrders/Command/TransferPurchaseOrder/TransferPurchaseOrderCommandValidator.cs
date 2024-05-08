using FluentValidation;
using Sepehr.Application.DTOs.PurchaseOrder;
using Sepehr.Application.Features.PurchaseOrders.Command.TransferPurchaseOrder;
using Sepehr.Domain.Enums;

namespace Sepehr.Application.Features.PurchaseOrders.Command.TransferPurchaseOrder
{
    public class TransferPurchaseOrderCommandValidator:AbstractValidator<TransferPurchaseOrderCommand>
    {
        public TransferPurchaseOrderCommandValidator()
        {
        }

     
    }
}