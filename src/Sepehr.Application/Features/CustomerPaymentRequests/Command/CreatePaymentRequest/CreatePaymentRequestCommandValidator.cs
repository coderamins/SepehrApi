using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sepehr.Application.Features.PaymentRequests.Command.CreatePaymentRequest
{
    public class CreatePaymentRequestCommandValidator : AbstractValidator<CreatePaymentRequestCommand>
    {
        public CreatePaymentRequestCommandValidator()
        {
        }

    }
}