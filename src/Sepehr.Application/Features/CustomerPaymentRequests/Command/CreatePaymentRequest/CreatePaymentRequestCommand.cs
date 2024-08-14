using AutoMapper;
using MediatR;
using Sepehr.Application.Interfaces.Repositories;
using Sepehr.Application.Wrappers;
using Sepehr.Domain.Common;
using Sepehr.Domain.Entities;
using Sepehr.Domain.Enums;
using Serilog;

namespace Sepehr.Application.Features.PaymentRequests.Command.CreatePaymentRequest
{
    public partial class CreatePaymentRequestCommand : IRequest<Response<PaymentRequest>>
    {
        public Guid CustomerId { get; set; }
        public EPaymentRequestType PaymentRequestTypeId { get; set; }
        public decimal Amount { get; set; }
        public int PaymentRequestReasonId { get; set; } 
        public required string BankAccountOrShabaNo { get; set; }
        public string AccountOwnerName { get; set; } = string.Empty;
        public int BankId { get; set; }
        public string ApplicatorName { get; set; } = string.Empty;
        public string PaymentRequestDescription { get; set; } = string.Empty;
    }
    public class CreatePaymentRequestCommandHandler : IRequestHandler<CreatePaymentRequestCommand, Response<PaymentRequest>>
    {
        private readonly IPaymentRequestRepositoryAsync _paymentRequestRepository;
        private readonly ILadingExitPermitRepositoryAsync _ladingExitPermit;
        private readonly IUnloadingPermitRepositoryAsync _puOrderUnloadPermitRepository;
        private readonly IMapper _mapper;
        public CreatePaymentRequestCommandHandler(
            IPaymentRequestRepositoryAsync paymentRequestRepository,
            IMapper mapper,
            ILadingExitPermitRepositoryAsync ladingExitPermit,
            IUnloadingPermitRepositoryAsync puOrderUnloadPermitRepository)
        {
            _paymentRequestRepository = paymentRequestRepository;
            _mapper = mapper;
            _ladingExitPermit = ladingExitPermit;
            _puOrderUnloadPermitRepository = puOrderUnloadPermitRepository;
        }

        public async Task<Response<PaymentRequest>> Handle(CreatePaymentRequestCommand command, CancellationToken cancellationToken)
        {
            try
            {
                var paymentRequest = _mapper.Map<PaymentRequest>(command);
                await _paymentRequestRepository.AddAsync(paymentRequest);

                return new Response<PaymentRequest>(paymentRequest, 
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