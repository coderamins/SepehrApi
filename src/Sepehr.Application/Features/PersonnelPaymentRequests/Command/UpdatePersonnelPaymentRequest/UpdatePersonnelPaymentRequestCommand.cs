using AutoMapper;
using Azure.Core;
using MediatR;
using Sepehr.Application.Exceptions;
using Sepehr.Application.Interfaces;
using Sepehr.Application.Interfaces.Repositories;
using Sepehr.Application.Wrappers;
using Sepehr.Domain.Common;
using Sepehr.Domain.Entities;
using Sepehr.Domain.Enums;

namespace Sepehr.Application.Features.PersonnelPaymentRequests.Command.UpdatePersonnelPaymentRequest
{
    public class UpdatePersonnelPaymentRequestCommand : IRequest<Response<string>>
    {
        public Guid Id { get; set; }
        public Guid PersonnelId { get; set; }
        public EPaymentRequestType PaymentRequestTypeId { get; set; }
        public decimal Amount { get; set; }
        public int PaymentRequestReasonId { get; set; } 
        public required string BankAccountOrShabaNo { get; set; }
        public string AccountOwnerName { get; set; } = string.Empty;
        public int BankId { get; set; }
        public string ApplicatorName { get; set; } = string.Empty;
        public string PaymentRequestDescription { get; set; } = string.Empty;

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