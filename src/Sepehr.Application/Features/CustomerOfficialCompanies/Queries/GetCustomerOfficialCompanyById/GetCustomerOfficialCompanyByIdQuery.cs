using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Sepehr.Application.Exceptions;
using Sepehr.Application.Interfaces.Repositories;
using Sepehr.Application.Wrappers;
using Sepehr.Domain.Entities;

namespace Sepehr.Application.Features.CustomerOfficialCompanys.Queries.GetCustomerOfficialCompanyById
{
    public class GetCustomerOfficialCompanyByIdQuery : IRequest<Response<CustomerOfficialCompany>>
    {
        public int Id { get; set; }

        public class GetCustomerOfficialCompanyByIdQueryHandler : IRequestHandler<GetCustomerOfficialCompanyByIdQuery, Response<CustomerOfficialCompany>>
        {
            private readonly ICustomerOfficialCompanyRepositoryAsync _customerOfficialCompanyRepository;

            public GetCustomerOfficialCompanyByIdQueryHandler(
                ICustomerOfficialCompanyRepositoryAsync customerOfficialCompanyRepository
            )
            {
                _customerOfficialCompanyRepository = customerOfficialCompanyRepository;
            }
                
            public async Task<Response<CustomerOfficialCompany>>
            Handle(
                GetCustomerOfficialCompanyByIdQuery query,
                CancellationToken cancellationToken
            )
            {
                var customerOfficialCompany = await _customerOfficialCompanyRepository.GetCustomerOfficialCompanyById(query.Id);
                if (customerOfficialCompany == null)
                    throw new ApiException($"شرکت رسمی یافت نشد !");
                return new Response<CustomerOfficialCompany>(customerOfficialCompany);
            }
        }
    }
}
