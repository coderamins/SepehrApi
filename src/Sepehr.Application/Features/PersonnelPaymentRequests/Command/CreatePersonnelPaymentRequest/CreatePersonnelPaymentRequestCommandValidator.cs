using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sepehr.Application.Features.PersonnelPaymentRequests.Command.CreatePersonnelPaymentRequest
{
    public class CreatePersonnelPaymentRequestCommandValidator : AbstractValidator<CreatePersonnelPaymentRequestCommand>
    {
        public CreatePersonnelPaymentRequestCommandValidator()
        {
        }

    }
}