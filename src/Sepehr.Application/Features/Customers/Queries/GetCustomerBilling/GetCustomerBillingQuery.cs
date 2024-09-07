using AutoMapper;
using MediatR;
using Sepehr.Application.Features.Customers.Queries.GetAllCustomers;
using Sepehr.Application.Interfaces.Repositories;
using Sepehr.Application.Wrappers;
using Sepehr.Domain.ViewModels;

namespace Sepehr.Application.Features.Customers.Queries.GetCustomerBilling
{
    public class GetCustomerBillingQuery : IRequest<Response<IEnumerable<GetCustomerBillingViewModel>>>
    {
        public Guid CustomerId { get; set; }
        public string FromDate { get; set; } = string.Empty;
        public string ToDate { get; set; } = string.Empty;
        public int BillingReportType { get; set; }
    }
    public class GetCustomerBillingQueryHandler :
         IRequestHandler<GetCustomerBillingQuery, Response<IEnumerable<GetCustomerBillingViewModel>>>
    {
        private readonly ICustomerRepositoryAsync _customerRepository;
        private readonly IMapper _mapper;
        public GetCustomerBillingQueryHandler(
            ICustomerRepositoryAsync customerRepository,
            IMapper mapper)
        {
            _customerRepository = customerRepository;
            _mapper = mapper;
        }

        public async Task<Response<IEnumerable<GetCustomerBillingViewModel>>> Handle(
            GetCustomerBillingQuery request,
            CancellationToken cancellationToken)
        {
            var validFilter = _mapper.Map<GetCustomerBillingParameter>(request);
            var customerBillingRep = await _customerRepository.GetCustomerBillingReport(validFilter);

            return new Response<IEnumerable<GetCustomerBillingViewModel>>(customerBillingRep);
        }
    }
}