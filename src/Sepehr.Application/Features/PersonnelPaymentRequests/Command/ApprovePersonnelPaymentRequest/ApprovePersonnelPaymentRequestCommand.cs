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

namespace Sepehr.Application.Features.PersonnelPaymentRequests.Command.ApprovePersonnelPaymentRequest
{
    public class ApprovePersonnelPaymentRequestCommand : IRequest<Response<string>>
    {
        public Guid Id { get; set; }
        public decimal Amount { get; set; }

        public class ApprovePersonnelPaymentRequestCommandHandler : IRequestHandler<ApprovePersonnelPaymentRequestCommand, Response<string>>
        {
            private readonly IMapper _mapper;
            private readonly IPersonnelPaymentRequestRepositoryAsync _personnelPaymentRequestRepository;
            public ApprovePersonnelPaymentRequestCommandHandler(IPersonnelPaymentRequestRepositoryAsync personnelPaymentRequestRepository, IMapper mapper)
            {
                _personnelPaymentRequestRepository = personnelPaymentRequestRepository;
                _mapper = mapper;
            }
            public async Task<Response<string>> Handle(ApprovePersonnelPaymentRequestCommand command, CancellationToken cancellationToken)
            {
                try
                {
                    var personnelPaymentRequest = await _personnelPaymentRequestRepository.GetByIdAsync(command.Id);
                    personnelPaymentRequest = _mapper.Map<ApprovePersonnelPaymentRequestCommand, PersonnelPaymentRequest>(command, personnelPaymentRequest);

                    if (personnelPaymentRequest == null)
                        throw new ApiException(new ErrorMessageFactory().MakeError("درخواست پرداخت", ErrorType.NotFound));
                    if (!new int[] { (int)EPaymentRequestStatus.InProgress, (int)EPaymentRequestStatus.Rejected }
                        .Contains(personnelPaymentRequest.PaymentRequestStatusId))
                        throw new ApiException("وضعیت درخواست نامعتبر می باشد !");
                    else
                    {
                        await _personnelPaymentRequestRepository.ApproveAsync(personnelPaymentRequest);
                        return new Response<string>(personnelPaymentRequest.Id.ToString(), new ErrorMessageFactory().MakeError("درخواست پرداخت", ErrorType.UpdatedSuccess));
                    }
                }
                catch (Exception e)
                {

                    throw;
                }
            }
        }
    }
}