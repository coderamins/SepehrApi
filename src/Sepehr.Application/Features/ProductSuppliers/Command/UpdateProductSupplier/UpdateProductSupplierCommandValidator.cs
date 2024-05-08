using FluentValidation;
using Sepehr.Application.Features.ProductSuppliers.Command.CreateProductSupplier;
using Sepehr.Application.Features.ProductSuppliers.Command.UpdateProductSupplier;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sepehr.Application.Features.ProductSuppliers.Command.UpdateProductSupplier
{
    public class UpdateProductSupplierCommandValidator : AbstractValidator<UpdateProductSupplierCommand>
    {
        public UpdateProductSupplierCommandValidator()
        {
        }

    }
}