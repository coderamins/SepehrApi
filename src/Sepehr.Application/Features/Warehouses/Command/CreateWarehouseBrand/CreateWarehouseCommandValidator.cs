using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sepehr.Application.Features.Warehouses.Command.CreateWarehouse
{
    public class CreateWarehouseCommandValidator : AbstractValidator<CreateWarehouseCommand>
    {
        public CreateWarehouseCommandValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage(" نام انبار نمی تواند خالی باشد !");
        }

    }
}