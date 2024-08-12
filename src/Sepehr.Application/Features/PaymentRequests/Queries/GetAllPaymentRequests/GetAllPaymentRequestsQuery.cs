using AutoMapper;
using MediatR;
using Sepehr.Application.Interfaces.Repositories;
using Sepehr.Application.Wrappers;
using Sepehr.Domain.ViewModels;

namespace Sepehr.Application.Features.PaymentRequests.Queries.GetAllPaymentRequests
{
    public class GetAllPaymentRequestsQuery : IRequest<PagedResponse<IEnumerable<PaymentRequestViewModel>>>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }

    }
    public class GetAllPaymentRequestsToPaymentQueryHandler :
         IRequestHandler<GetAllPaymentRequestsQuery, PagedResponse<IEnumerable<PaymentRequestViewModel>>>
    {
        private readonly IPaymentRequestRepositoryAsync _paymentRequestRepository;
        private readonly IMapper _mapper;
        public GetAllPaymentRequestsToPaymentQueryHandler(IPaymentRequestRepositoryAsync paymentRequestRepository, IMapper mapper)
        {
            _paymentRequestRepository = paymentRequestRepository;
            _mapper = mapper;
        }

        public async Task<PagedResponse<IEnumerable<PaymentRequestViewModel>>> Handle(GetAllPaymentRequestsQuery request, 
            CancellationToken cancellationToken)
        {
            try
            {
                var validFilter = _mapper.Map<GetAllPaymentRequestsParameter>(request);
                var payRequests = await _paymentRequestRepository.GetAllPaymentRequestsAsync(validFilter);

                var result = _mapper.Map<List<PaymentRequestViewModel>>(payRequests);

                var paymentRequests =
                    result.Skip((validFilter.PageNumber - 1) * validFilter.PageSize)
                    .Take(validFilter.PageSize).ToList();

                return new PagedResponse<IEnumerable<PaymentRequestViewModel>>(
                    paymentRequests,
                    validFilter.PageNumber,
                    validFilter.PageSize, result.Count());

            }
            catch (Exception e) 
            {
                throw;
            }
        }

    }
}