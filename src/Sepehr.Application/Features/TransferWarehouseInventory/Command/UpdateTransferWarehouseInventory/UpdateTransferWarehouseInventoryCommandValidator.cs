using FluentValidation;
using Sepehr.Application.DTOs.Product;

namespace Sepehr.Application.Features.TransferWarehouseInventories.Command.UpdateTransferWarehouseInventory
{
    public class UpdateTransferWarehouseInventoryValidator : AbstractValidator<UpdateTransferWarehouseInventoryCommand>
    {
        public UpdateTransferWarehouseInventoryValidator()
        {
        }


    }
}