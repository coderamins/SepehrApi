using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using AutoMapper;
using Azure.Core;
using MediatR;
using Sepehr.Application.DTOs;
using Sepehr.Application.Exceptions;
using Sepehr.Application.Interfaces.Repositories;
using Sepehr.Application.Wrappers;
using Sepehr.Domain.Common;
using Sepehr.Domain.Entities;
using Sepehr.Domain.Enums;

namespace Sepehr.Application.Features.PersonnelPaymentRequests.Command.ProceedPersonnelPaymentRequest
{
    public class ProceedToPersonnelPaymentRequestCommand : IRequest<Response<string>>
    {
        public Guid Id { get; set; }
        public List<AttachmentDto>? Attachments { get; set; }
        public class ProceedToPersonnelPaymentRequestCommandHandler : IRequestHandler<ProceedToPersonnelPaymentRequestCommand, Response<string>>
        {
            private readonly IMapper _mapper;
            private readonly IPersonnelPaymentRequestRepositoryAsync _personnelPaymentRequestRepository;
            public ProceedToPersonnelPaymentRequestCommandHandler(IPersonnelPaymentRequestRepositoryAsync personnelPaymentRequestRepository, IMapper mapper)
            {
                _personnelPaymentRequestRepository = personnelPaymentRequestRepository;
                _mapper = mapper;
            }
            public async Task<Response<string>> Handle(ProceedToPersonnelPaymentRequestCommand command, CancellationToken cancellationToken)
            {
                var personnelPaymentRequest = await _personnelPaymentRequestRepository.GetByIdAsync(command.Id);
                if (personnelPaymentRequest == null)
                    throw new ApiException("درخواست پرداخت یافت نشد !");

                personnelPaymentRequest = _mapper.Map<ProceedToPersonnelPaymentRequestCommand, PersonnelPaymentRequest>(command, personnelPaymentRequest);

                if (personnelPaymentRequest == null)
                    throw new ApiException(new ErrorMessageFactory().MakeError("درخواست پرداخت", ErrorType.NotFound));
                if (!new int[] { 1,2,4}.Contains(personnelPaymentRequest.PaymentRequestStatusId))
                    throw new ApiException("وضعیت درخواست نامعتبر می باشد !");
                else
                {
                    personnelPaymentRequest.PaymentRequestStatusId = (int)EPaymentRequestStatus.Payed;
                    await _personnelPaymentRequestRepository.UpdateAsync(personnelPaymentRequest);
                    return new Response<string>(personnelPaymentRequest.Id.ToString(),"درخواست پرداخت با موفقیت انجام شد .(پرداخت شد)");
                }
            }
        }
    }
}