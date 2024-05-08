using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sepehr.Application.Features.RentPayments.Command.CreateRentPayment
{
    public class CreateRentPaymentCommandValidator : AbstractValidator<CreateRentPaymentCommand>
    {
        public CreateRentPaymentCommandValidator()
        {
        }

    }
}