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

namespace Sepehr.Application.Features.Customers.Queries.GetAllCustomers
{
    public class GetAllCustomersQuery : IRequest<PagedResponse<IEnumerable<CustomerViewModel>>>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public int? CustomerCode { get; set; }
        public string CustomerName { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public string NationalCode { get; set; } = string.Empty;
        public int? CustomerLabelId { get; set; }
        public CustomerReportType ReportType { get; set; }
    }
    public class GetAllCustomerQueryHandler :
         IRequestHandler<GetAllCustomersQuery, PagedResponse<IEnumerable<CustomerViewModel>>>
    {
        private readonly ICustomerRepositoryAsync _customerRepository;
        private readonly IMapper _mapper;
        public GetAllCustomerQueryHandler(ICustomerRepositoryAsync customerRepository, IMapper mapper)
        {
            _customerRepository = customerRepository;
            _mapper = mapper;
        }

        public async Task<PagedResponse<IEnumerable<CustomerViewModel>>> Handle(
            GetAllCustomersQuery request, 
            CancellationToken cancellationToken)
        {
            var validFilter = _mapper.Map<GetAllCustomersParameter>(request);
            var customer = await _customerRepository.GetAllCustomers(validFilter);
              
            var customerViewModel = _mapper.Map<IEnumerable<CustomerViewModel>>(customer);
            return new PagedResponse<IEnumerable<CustomerViewModel>>(
                customerViewModel, 
                validFilter.PageNumber, 
                validFilter.PageSize);
        }
    }
}