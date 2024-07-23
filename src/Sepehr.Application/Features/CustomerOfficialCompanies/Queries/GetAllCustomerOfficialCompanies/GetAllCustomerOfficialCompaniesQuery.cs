using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Sepehr.Application.Interfaces.Repositories;
using Sepehr.Application.Wrappers;
using Sepehr.Domain.Entities;
using Sepehr.Domain.ViewModels;

namespace Sepehr.Application.Features.CustomerOfficialCompanys.Queries.GetAllCustomerOfficialCompanys
{
    public class GetAllCustomerOfficialCompanysQuery : IRequest<PagedResponse<IEnumerable<CustomerOfficialCompanyViewModel>>>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public bool? IsActive { get; set; } = true;
        public Guid? CustomerId { get; set; }
    }
    public class GetAllCustomerOfficialCompanysQueryHandler :
         IRequestHandler<GetAllCustomerOfficialCompanysQuery, PagedResponse<IEnumerable<CustomerOfficialCompanyViewModel>>>
    {
        private readonly ICustomerOfficialCompanyRepositoryAsync _customerOfficialCompanyRepository;
        private readonly IMapper _mapper;
        public GetAllCustomerOfficialCompanysQueryHandler(ICustomerOfficialCompanyRepositoryAsync customerOfficialCompanyRepository, IMapper mapper)
        {
            _customerOfficialCompanyRepository = customerOfficialCompanyRepository;
            _mapper = mapper;
        }

        public async Task<PagedResponse<IEnumerable<CustomerOfficialCompanyViewModel>>> Handle(
            GetAllCustomerOfficialCompanysQuery request,
            CancellationToken cancellationToken)
        {
            try
            {
                var validFilter = _mapper.Map<GetAllCustomerOfficialCompanysParameter>(request);
                var customerOfficialCompanys = await _customerOfficialCompanyRepository.GetAllCustomerOfficialCompanies(validFilter);

                var customerOfficialCompanyViewModel = _mapper.Map<IEnumerable<CustomerOfficialCompanyViewModel>>(customerOfficialCompanys);
                return new PagedResponse<IEnumerable<CustomerOfficialCompanyViewModel>>(
                    customerOfficialCompanyViewModel,
                    validFilter.PageNumber,
                    validFilter.PageSize);
            }
            catch (Exception e)
            {

                throw;
            }
        }
    }
}