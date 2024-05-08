using FluentValidation;
using Sepehr.Application.Features.ProductSuppliers.Command.CreateProductSupplier;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sepehr.Application.Features.ProductSuppliers.Command.CreateProductSupplier
{
    public class CreateProductSupplierCommandValidator:AbstractValidator<CreateProductSupplierCommand>
    {
        public CreateProductSupplierCommandValidator()
        {
        }

    }
}