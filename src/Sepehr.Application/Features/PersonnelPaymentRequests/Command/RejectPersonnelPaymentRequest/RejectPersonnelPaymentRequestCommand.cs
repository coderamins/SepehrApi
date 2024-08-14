using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using AutoMapper;
using Azure.Core;
using MediatR;
using Sepehr.Application.Exceptions;
using Sepehr.Application.Interfaces.Repositories;
using Sepehr.Application.Wrappers;
using Sepehr.Domain.Common;
using Sepehr.Domain.Entities;
using Sepehr.Domain.Enums;

namespace Sepehr.Application.Features.PersonnelPaymentRequests.Command.RejectPersonnelPaymentRequest
{
    public class RejectPersonnelPaymentRequestCommand : IRequest<Response<string>>
    {
        public Guid Id { get; set; }
        public string RejectReasonDesc { get; set; } = string.Empty;

        public class RejectPersonnelPaymentRequestCommandHandler : IRequestHandler<RejectPersonnelPaymentRequestCommand, Response<string>>
        {
            private readonly IMapper _mapper;
            private readonly IPersonnelPaymentRequestRepositoryAsync _personnelPaymentRequestRepository;
            public RejectPersonnelPaymentRequestCommandHandler(IPersonnelPaymentRequestRepositoryAsync personnelPaymentRequestRepository, IMapper mapper)
            {
                _personnelPaymentRequestRepository = personnelPaymentRequestRepository;
                _mapper = mapper;
            }
            public async Task<Response<string>> Handle(RejectPersonnelPaymentRequestCommand command, CancellationToken cancellationToken)
            {
                var personnelPaymentRequest = await _personnelPaymentRequestRepository.GetByIdAsync(command.Id);
                personnelPaymentRequest = _mapper.Map<RejectPersonnelPaymentRequestCommand, PersonnelPaymentRequest>(command, personnelPaymentRequest);

                if (personnelPaymentRequest == null)
                    throw new ApiException(new ErrorMessageFactory().MakeError("درخواست پرداخت", ErrorType.NotFound));
                if (!new int[] { 1, 2, 4 }.Contains(personnelPaymentRequest.PaymentRequestStatusId))
                    throw new ApiException("وضعیت درخواست نامعتبر می باشد !");
                else
                {
                    personnelPaymentRequest.PaymentRequestStatusId = (int)EPaymentRequestStatus.Rejected;
                    personnelPaymentRequest.RejectReasonDesc = command.RejectReasonDesc;
                    await _personnelPaymentRequestRepository.UpdateAsync(personnelPaymentRequest);
                    return new Response<string>(personnelPaymentRequest.Id.ToString(), new ErrorMessageFactory().MakeError("درخواست پرداخت", ErrorType.UpdatedSuccess));
                }
            }
        }
    }
}