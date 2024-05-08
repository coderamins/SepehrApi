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
    public class AccRegisterReceivePayCommand : IRequest<Response<bool>>
    {
        public required IEnumerable<Guid> ReceivePays { get; set; }
        public int AccountDocNo { get; set; }

        public class AccRegisterReceivePayCommandHandler : IRequestHandler<AccRegisterReceivePayCommand, Response<bool>>
        {
            private readonly IMapper _mapper;
            private readonly IReceivePayRepositoryAsync _preceivePayRepository;
            public AccRegisterReceivePayCommandHandler(IReceivePayRepositoryAsync preceivePayRepository, IMapper mapper)
            {
                _preceivePayRepository = preceivePayRepository;
                _mapper = mapper;
            }
            public async Task<Response<bool>> Handle(AccRegisterReceivePayCommand command, CancellationToken cancellationToken)
            {
                var receivePay = await _preceivePayRepository.GetReceivePays(command.ReceivePays);
                //preceivePay = _mapper.Map(command, preceivePay);
                if (receivePay == null)
                {
                    throw new ApiException($"دریافت/پرداخت یافت نشد !");
                }
                else if(receivePay.Any(r=>r.ReceivePayStatusId==3))
                {
                    throw new ApiException($"ثبت حسابداری برای دریافت پرداخت های \"{string.Join(',', receivePay.Select(r=>r.ReceivePayCode))}\" قبلا انجام شده است !");
                }
                else
                {
                    foreach (var item in receivePay)
                    {
                        item.ReceivePayStatusId = 3;
                        item.IsAccountingApproval = true;
                        item.AccountingApprovalDate = DateTime.Now;
                        item.AccountingDocNo = command.AccountDocNo;
                    }

                    await _preceivePayRepository.UpdateAsync(receivePay.ToList());
                    return new Response<bool>(true, "ثبت حسابداری با موفقیت انجام شد !");
                }
            }
        }
    }
}