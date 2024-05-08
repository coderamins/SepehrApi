using FluentValidation;
using Sepehr.Application.Features.ReceivePays.Command.CreateReceivePay;
using Sepehr.Application.Features.ReceivePays.Command.UpdateReceivePay;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sepehr.Application.Features.ReceivePays.Command.UpdateReceivePay
{
    public class UpdateReceivePayCommandValidator : AbstractValidator<UpdateReceivePayCommand>
    {
        public UpdateReceivePayCommandValidator()
        {
        }

    }
}