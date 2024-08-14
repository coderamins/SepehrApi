using AutoMapper;
using MediatR;
using Sepehr.Application.Interfaces.Repositories;
using Sepehr.Application.Wrappers;
using Sepehr.Domain.ViewModels;

namespace Sepehr.Application.Features.PersonnelPaymentRequests.Queries.GetAllPersonnelPaymentRequests
{
    public class GetAllPersonnelPaymentRequestsQuery : IRequest<PagedResponse<IEnumerable<PersonnelPaymentRequestViewModel>>>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public int? PaymentRequestCoode { get; set; }

    }
    public class GetAllPersonnelPaymentRequestsToPaymentQueryHandler :
         IRequestHandler<GetAllPersonnelPaymentRequestsQuery, PagedResponse<IEnumerable<PersonnelPaymentRequestViewModel>>>
    {
        private readonly IPersonnelPaymentRequestRepositoryAsync _personnelPaymentRequestRepository;
        private readonly IMapper _mapper;
        public GetAllPersonnelPaymentRequestsToPaymentQueryHandler(IPersonnelPaymentRequestRepositoryAsync personnelPaymentRequestRepository, IMapper mapper)
        {
            _personnelPaymentRequestRepository = personnelPaymentRequestRepository;
            _mapper = mapper;
        }

        public async Task<PagedResponse<IEnumerable<PersonnelPaymentRequestViewModel>>> Handle(GetAllPersonnelPaymentRequestsQuery request, 
            CancellationToken cancellationToken)
        {
            try
            {
                var validFilter = _mapper.Map<GetAllPersonnelPaymentRequestsParameter>(request);
                var payRequests = await _personnelPaymentRequestRepository.GetAllPersonnelPaymentRequestsAsync(validFilter);

                var result = _mapper.Map<List<PersonnelPaymentRequestViewModel>>(payRequests);

                var personnelPaymentRequests =
                    result.Skip((validFilter.PageNumber - 1) * validFilter.PageSize)
                    .Take(validFilter.PageSize).ToList();

                return new PagedResponse<IEnumerable<PersonnelPaymentRequestViewModel>>(
                    personnelPaymentRequests,
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