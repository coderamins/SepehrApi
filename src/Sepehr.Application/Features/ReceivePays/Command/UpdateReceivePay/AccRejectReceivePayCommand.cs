using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Sepehr.Application.Exceptions;
using Sepehr.Application.Interfaces.Repositories;
using Sepehr.Application.Wrappers;
using Sepehr.Domain.Entities;
using Sepehr.Domain.Enums;

namespace Sepehr.Application.Features.ReceivePays.Command.AccRejectReceivePay
{
    public class AccRejectReceivePayCommand : IRequest<Response<bool>>
    {
        public Guid Id { get; set; }
        public required string AccountingDescription { get; set; }

        public class AccRejectReceivePayCommandHandler : IRequestHandler<AccRejectReceivePayCommand, Response<bool>>
        {
            private readonly IMapper _mapper;
            private readonly IReceivePayRepositoryAsync _preceivePayRepository;
            public AccRejectReceivePayCommandHandler(IReceivePayRepositoryAsync preceivePayRepository, IMapper mapper)
            {
                _preceivePayRepository = preceivePayRepository;
                _mapper = mapper;
            }

            public async Task<Response<bool>> Handle(AccRejectReceivePayCommand command, CancellationToken cancellationToken)
            {
                var receivePay = await _preceivePayRepository.GetReceivePayByIdAsync(command.Id);

                if (receivePay == null)
                    throw new ApiException($"دریافت/پرداخت یافت نشد !");
                else
                {
                    receivePay.ReceivePayStatusId = 1;
                    receivePay.AccountingDescription = command.AccountingDescription;

                    await _preceivePayRepository.UpdateAsync(receivePay);
                    return new Response<bool>(true, "برگشت تایید با موفقیت انجام شد !");
                }
            }
        }
    }
}