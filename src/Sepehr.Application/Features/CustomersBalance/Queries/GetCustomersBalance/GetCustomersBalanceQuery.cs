using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Sepehr.Application.Exceptions;
using Sepehr.Application.Interfaces.Repositories;
using Sepehr.Application.Wrappers;
using Sepehr.Domain.Entities;
using Sepehr.Domain.ViewModels;

namespace Sepehr.Application.Features.Customers.Queries.GetCustomerById
{
    public class GetCustomersBalanceQuery : IRequest<Response<CustomerViewModel>>
    {
        public Guid Id { get; set; }

        public class GetCustomersBalanceQueryHandler : IRequestHandler<GetCustomersBalanceQuery, Response<CustomerViewModel>>
        {
            private readonly ICustomerRepositoryAsync _customerRepository;
            private readonly IMapper _mapper;

            public GetCustomersBalanceQueryHandler(
                ICustomerRepositoryAsync customerRepository,
                IMapper mapper
            )
            {
                _customerRepository = customerRepository;
                _mapper = mapper;
            }

            public async Task<Response<CustomerViewModel>>
            Handle(
                GetCustomersBalanceQuery query,
                CancellationToken cancellationToken
            )
            {
                var customer = await _customerRepository.GetCustomerInfo(query.Id);
                var customerViewModel=_mapper.Map<CustomerViewModel>(customer);
                if (customer == null)
                    throw new ApiException($"مشتری یافت نشد !");

                return new Response<CustomerViewModel>(customerViewModel);
            }
        }
    }
}
