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
    public class GetCustomerByIdQuery : IRequest<Response<CustomerViewModel>>
    {
        public Guid Id { get; set; }

        public class GetCustomerByIdQueryHandler : IRequestHandler<GetCustomerByIdQuery, Response<CustomerViewModel>>
        {
            private readonly ICustomerRepositoryAsync _customerRepository;
            private readonly IMapper _mapper;

            public GetCustomerByIdQueryHandler(
                ICustomerRepositoryAsync customerRepository,
                IMapper mapper
            )
            {
                _customerRepository = customerRepository;
                _mapper = mapper;
            }

            public async Task<Response<CustomerViewModel>>
            Handle(
                GetCustomerByIdQuery query,
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
