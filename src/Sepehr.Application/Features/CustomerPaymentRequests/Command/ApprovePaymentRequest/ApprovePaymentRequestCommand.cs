using AutoMapper;
using MediatR;
using Sepehr.Application.Exceptions;
using Sepehr.Application.Interfaces.Repositories;
using Sepehr.Application.Wrappers;
using Sepehr.Domain.Common;
using Sepehr.Domain.Entities;
using Sepehr.Domain.Enums;

namespace Sepehr.Application.Features.PaymentRequests.Command.ApprovePaymentRequest
{
    public class ApprovePaymentRequestCommand : IRequest<Response<string>>
    {
        public Guid Id { get; set; }
        public decimal Amount { get; set; }

        public class ApprovePaymentRequestCommandHandler : IRequestHandler<ApprovePaymentRequestCommand, Response<string>>
        {
            private readonly IMapper _mapper;
            private readonly IPaymentRequestRepositoryAsync _paymentRequestRepository;
            public ApprovePaymentRequestCommandHandler(IPaymentRequestRepositoryAsync paymentRequestRepository, IMapper mapper)
            {
                _paymentRequestRepository = paymentRequestRepository;
                _mapper = mapper;
            }
            public async Task<Response<string>> Handle(ApprovePaymentRequestCommand command, CancellationToken cancellationToken)
            {
                var paymentRequest = await _paymentRequestRepository.GetByIdAsync(command.Id);
                if(paymentRequest == null)
                    throw new ApiException("درخواست پرداخت یافت نشد !");

                paymentRequest = _mapper.Map(command, paymentRequest);

                if (paymentRequest == null)
                    throw new ApiException(new ErrorMessageFactory().MakeError("درخواست پرداخت", ErrorType.NotFound));
                if (!new int[] { 1,2,4}.Contains(paymentRequest.PaymentRequestStatusId))
                    throw new ApiException("وضعیت درخواست نامعتبر می باشد !");
                else
                {
                    await _paymentRequestRepository.ApproveAsync(paymentRequest);
                    return new Response<string>(paymentRequest.Id.ToString(), new ErrorMessageFactory().MakeError("درخواست پرداخت", ErrorType.UpdatedSuccess));
                }
            }
        }
    }
}