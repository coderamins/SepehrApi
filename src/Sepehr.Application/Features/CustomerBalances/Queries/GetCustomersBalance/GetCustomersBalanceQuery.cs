using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Sepehr.Application.Interfaces.Repositories;
using Sepehr.Application.Wrappers;
using Sepehr.Domain.Entities;
using Sepehr.Domain.Enums;
using Sepehr.Domain.ViewModels;
using Serilog;

namespace Sepehr.Application.Features.Customers.Queries.GetAllCustomers
{
    public class GetCustomersBalanceQuery : IRequest<PagedResponse<IEnumerable<CustomerBalanceViewModel>>>
    {
        public Guid? CustomerId { get; set; }
        public int? CustomerCode { get; set; }
        public string BalanceFromDate { get; set; } = string.Empty;
        public string BalanceToDate { get; set; } = string.Empty;
        public EBalanceReportType ReportType { get; set; }
    }
    public class GetCustomersBalanceQueryHandler :
         IRequestHandler<GetCustomersBalanceQuery, PagedResponse<IEnumerable<CustomerBalanceViewModel>>>
    {
        private readonly ICustomerRepositoryAsync _customerRepository;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;
        public GetCustomersBalanceQueryHandler(
            ICustomerRepositoryAsync customerRepository,
            ILogger logger,
            IMapper mapper)
        {
            _customerRepository = customerRepository;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<PagedResponse<IEnumerable<CustomerBalanceViewModel>>> Handle(
            GetCustomersBalanceQuery request, 
            CancellationToken cancellationToken)
        {
            var validFilter = _mapper.Map<GetCustomersBalanceParameter>(request);
            var customer = await _customerRepository.GetCustomerBalance(validFilter);
              
            var customerViewModel = _mapper.Map<IEnumerable<CustomerBalanceViewModel>>(customer);
            return new PagedResponse<IEnumerable<CustomerBalanceViewModel>>(
                customerViewModel, 
                validFilter.PageNumber, 
                validFilter.PageSize);
        }
    }
}