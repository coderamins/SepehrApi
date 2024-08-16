using AutoMapper;
using MediatR;
using Sepehr.Application.Interfaces.Repositories;
using Sepehr.Application.Wrappers;
using Sepehr.Domain.Common;
using Sepehr.Domain.Entities;
using Serilog;

namespace Sepehr.Application.Features.PersonnelPaymentRequests.Command.CreatePersonnelPaymentRequest
{
    public partial class CreatePersonnelPaymentRequestCommand : IRequest<Response<PersonnelPaymentRequest>>
    {
        public Guid PersonnelId { get; set; }
        public decimal Amount { get; set; }
        public int PaymentRequestReasonId { get; set; } 
        public required string BankAccountOrShabaNo { get; set; }
        public string AccountOwnerName { get; set; } = string.Empty;
        public int BankId { get; set; }
        public string ApplicatorName { get; set; } = string.Empty;
        public string PaymentRequestDescription { get; set; } = string.Empty;
    }
    public class CreatePersonnelPaymentRequestCommandHandler : IRequestHandler<CreatePersonnelPaymentRequestCommand, Response<PersonnelPaymentRequest>>
    {
        private readonly IPersonnelPaymentRequestRepositoryAsync _personnelPaymentRequestRepository;
        private readonly ILadingExitPermitRepositoryAsync _ladingExitPermit;
        private readonly IUnloadingPermitRepositoryAsync _puOrderUnloadPermitRepository;
        private readonly IMapper _mapper;
        public CreatePersonnelPaymentRequestCommandHandler(
            IPersonnelPaymentRequestRepositoryAsync personnelPaymentRequestRepository,
            IMapper mapper,
            ILadingExitPermitRepositoryAsync ladingExitPermit,
            IUnloadingPermitRepositoryAsync puOrderUnloadPermitRepository)
        {
            _personnelPaymentRequestRepository = personnelPaymentRequestRepository;
            _mapper = mapper;
            _ladingExitPermit = ladingExitPermit;
            _puOrderUnloadPermitRepository = puOrderUnloadPermitRepository;
        }

        public async Task<Response<PersonnelPaymentRequest>> Handle(CreatePersonnelPaymentRequestCommand command, CancellationToken cancellationToken)
        {
            try
            {
                var personnelPaymentRequest = _mapper.Map<PersonnelPaymentRequest>(command);
                await _personnelPaymentRequestRepository.AddAsync(personnelPaymentRequest);

                return new Response<PersonnelPaymentRequest>(personnelPaymentRequest, 
                    new ErrorMessageFactory()
                    .MakeError("درخواست پرداخت", ErrorType.CreatedSuccess));
            }
            catch (Exception e)
            {
                Log.Write(Serilog.Events.LogEventLevel.Error, e.Message ?? e.InnerException.Message);
                throw;
            }
        }

    }
}