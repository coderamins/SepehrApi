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

namespace Sepehr.Application.Features.ReceivePays.Command.AccApproveReceivePay
{
    public class AccApproveReceivePayCommand : IRequest<Response<bool>>
    {
        public IEnumerable<Guid> Ids { get; set; }

        public class AccApproveReceivePayCommandHandler : IRequestHandler<AccApproveReceivePayCommand, Response<bool>>
        {
            private readonly IMapper _mapper;
            private readonly IReceivePayRepositoryAsync _preceivePayRepository;
            public AccApproveReceivePayCommandHandler(IReceivePayRepositoryAsync preceivePayRepository, IMapper mapper)
            {
                _preceivePayRepository = preceivePayRepository;
                _mapper = mapper;
            }

            public async Task<Response<bool>> Handle(AccApproveReceivePayCommand command, CancellationToken cancellationToken)
            {
                var receivePays = await _preceivePayRepository.GetReceivePays(command.Ids);
                //preceivePay = _mapper.Map<AccApproveReceivePayCommand, ReceivePay>(command, preceivePay);
                if (receivePays == null)
                {
                    throw new ApiException($"دریافت/پرداخت یافت نشد !");
                }
                else if (receivePays.Any(r => r.ReceivePayStatusId == 2))
                {
                    throw new ApiException($"تایید حسابداری برای دریافت پرداخت های \"{string.Join(',', receivePays.Select(r => r.ReceivePayCode))}\" قبلا انجام شده است !");
                }
                else
                {
                    foreach (var item in receivePays)
                    {
                        item.ReceivePayStatusId = 2;
                    }

                    await _preceivePayRepository.UpdateAsync(receivePays.ToList());
                    return new Response<bool>(true, "تایید حسابداری با موفقیت انجام شد !");
                }
            }
        }
    }
}