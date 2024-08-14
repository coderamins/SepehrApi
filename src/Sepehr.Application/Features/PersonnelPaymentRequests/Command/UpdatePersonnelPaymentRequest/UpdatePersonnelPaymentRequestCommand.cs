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
using Sepehr.Application.Interfaces;
using Sepehr.Application.Interfaces.Repositories;
using Sepehr.Application.Wrappers;
using Sepehr.Domain.Common;
using Sepehr.Domain.Entities;

namespace Sepehr.Application.Features.PersonnelPaymentRequests.Command.UpdatePersonnelPaymentRequest
{
    public class UpdatePersonnelPaymentRequestCommand : IRequest<Response<string>>
    {
        public Guid Id { get; set; }
        public Guid CustomerId { get; set; }
        public decimal Amount { get; set; }
        public int PersonnelPaymentRequestReasonId { get; set; } 
        public required string BankAccountOrShabaNo { get; set; }
        public string AccountOwnerName { get; set; } = string.Empty;
        public int BankId { get; set; }
        public string ApplicatorName { get; set; } = string.Empty;
        public string PersonnelPaymentRequestDescription { get; set; } = string.Empty;

        public class UpdatePersonnelPaymentRequestCommandHandler : IRequestHandler<UpdatePersonnelPaymentRequestCommand, Response<string>>
        {
            private readonly IMapper _mapper;
            private readonly IPersonnelPaymentRequestRepositoryAsync _personnelPaymentRequestRepository;
            private readonly IAuthenticatedUserService _userService;
            public UpdatePersonnelPaymentRequestCommandHandler(
                IPersonnelPaymentRequestRepositoryAsync personnelPaymentRequestRepository,
                IAuthenticatedUserService userService,
                IMapper mapper)
            {
                _personnelPaymentRequestRepository = personnelPaymentRequestRepository;
                _mapper = mapper;
                _userService = userService; 
            }
            public async Task<Response<string>> Handle(UpdatePersonnelPaymentRequestCommand command, CancellationToken cancellationToken)
            {
                var personnelPaymentRequests = _personnelPaymentRequestRepository.GetAllAsQueryable();

                var personnelPaymentRequest = await _personnelPaymentRequestRepository.GetByIdAsync(command.Id);
                personnelPaymentRequest = _mapper.Map<UpdatePersonnelPaymentRequestCommand, PersonnelPaymentRequest>(command, personnelPaymentRequest);

                if (personnelPaymentRequest == null)
                    throw new ApiException(new ErrorMessageFactory().MakeError("درخواست پرداخت", ErrorType.NotFound));
                else
                {
                    personnelPaymentRequest.ApproverId =Guid.Parse(_userService.UserId);
                    await _personnelPaymentRequestRepository.UpdateAsync(personnelPaymentRequest);
                    return new Response<string>(personnelPaymentRequest.Id.ToString(), new ErrorMessageFactory().MakeError("درخواست پرداخت", ErrorType.UpdatedSuccess));
                }
            }
        }
    }
}